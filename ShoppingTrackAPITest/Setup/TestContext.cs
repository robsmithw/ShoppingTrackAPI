using Docker.DotNet;
using Docker.DotNet.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Version = System.Version;
using ShoppingTrackAPI.Models;
using ShoppingTrackAPI;

namespace ShoppingTrackAPITest.Setup
{
    public class TestContext : IAsyncLifetime
    {
        private readonly DockerClient _dockerClient;
        private string _containerId;
        private TestServer _server;
        public HttpClient Client { get; set; }
        public ShoppingTrackContext DbContext { get; set; }
        private const string ContainerImageUri = "mysql";
        private const string ContainerImageTag = "5.7";
        private const string ConnectionString
            = "Server=localhost;Port=3306;Database=ShoppingTrack;Uid=root;Pwd=Password1;SSLMode=None";

        public TestContext()
        {
            var dockerClientUri = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                ? new Uri("npipe://./pipe/docker_engine")
                : new Uri("unix:///var/run/docker.sock");
            using var dockerClientConfiguration = new DockerClientConfiguration(dockerClientUri);
            _dockerClient = dockerClientConfiguration.CreateClient();
        }
        public async Task InitializeAsync()
        {
            await PullImageAsync();
            await CleanContainersAsync();
            await StartContainerAsync();
            await MigrateDatabaseAsync();
            SetupClientAsync();
        }

        public async Task DisposeAsync()
        {
            if (_containerId != null)
            {
                await _dockerClient.Containers.KillContainerAsync(_containerId, new ContainerKillParameters());
                await _dockerClient.Containers.RemoveContainerAsync(_containerId, new ContainerRemoveParameters
                {
                    RemoveVolumes = true,
                    Force = true
                });
            }
        }

        private void SetupClientAsync()
        {
            var appSettingOverrides = new Dictionary<string, string>()
            {
                { "ConnectionStrings:DefaultConnection", ConnectionString },
                { "IntegrationTesting", "true" }
            };

            _server = new TestServer(new WebHostBuilder()
                .UseEnvironment("Development")
                .ConfigureAppConfiguration((hostingContext, config) => {
                    config.AddInMemoryCollection(appSettingOverrides);
                })
                .UseStartup<Startup>())
            {
                BaseAddress = new Uri("http://localhost:8000")
            };
            Client = _server.CreateClient();
        }

        private async Task CleanContainersAsync()
        {
            var containerListParams = new ContainersListParameters
            {
                All = true
            };

            var containersList = await _dockerClient.Containers.ListContainersAsync(containerListParams);

            foreach (var container in containersList)
            {
                if (container.Labels.ContainsKey("UnitTest") && container.Labels["UnitTest"] == "ShoppingTrackAPI")
                {
                    try
                    {
                        Console.WriteLine("The following container will be removed:" + container.ID + ":" + string.Join(',', container.Names));
                        await _dockerClient.Containers.RemoveContainerAsync(container.ID, new ContainerRemoveParameters
                        {
                            RemoveVolumes = true,
                            Force = true
                        });
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Failed to remove - perhaps its already been removed :" + ex.ToString());
                    }
                }
            }
        }
        /// <summary>
        /// Migrates the database.
        /// Tries to migrate the database multiple times becuase
        /// mysql contianer takes a while to inititalize
        /// </summary>
        /// <returns></returns>
        private async Task MigrateDatabaseAsync()
        {
            var isMigrationSuccessful = false;
            var migrationAttempt = 0;
            do
            {
                try
                {
                    await Task.Delay(5000);
                    migrationAttempt++;
                    var optionsBuilder = new DbContextOptionsBuilder<ShoppingTrackContext>();
                    // optionsBuilder.UseMySql(ConnectionString);
                    optionsBuilder.UseMySql(ConnectionString, options =>
                    {
                        options
                            .ServerVersion(new Version(5, 7, 32), ServerType.MySql)
                            .EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                    });
                    DbContext = new ShoppingTrackContext(optionsBuilder.Options);
                    await DbContext.Database.MigrateAsync();
                    isMigrationSuccessful = true;
                }
                catch (Exception)
                {
                    if (migrationAttempt > 6)
                        throw;
                    Console.WriteLine("Error migrating the database");
                }
#pragma warning disable S2583 // Conditionally executed blocks should be reachable
            } while (!isMigrationSuccessful);
#pragma warning restore S2583 // Conditionally executed blocks should be reachable
        }

        #region Docker
        private async Task PullImageAsync()
        {
            if(!(await DoesImageExistAsync(ContainerImageUri, ContainerImageTag)))
                await _dockerClient.Images.CreateImageAsync(new ImagesCreateParameters
                {
                    FromImage = ContainerImageUri,
                    Tag = ContainerImageTag
                }, new AuthConfig(), new Progress<JSONMessage>());
        }

        private async Task<bool> DoesImageExistAsync(string imageUri, string tag)
        {
            var images = await _dockerClient.Images.ListImagesAsync(new ImagesListParameters()
            {
                MatchName = $"{imageUri}:{tag}"
            });
            return images.Any();
        }

        private async Task StartContainerAsync()
        {
            const string port = "3306";
            var response = await _dockerClient.Containers.CreateContainerAsync(new CreateContainerParameters
            {
                Labels = new Dictionary<string, string> { { "UnitTest", "ShoppingTrackAPI" } },
                Image = $"{ContainerImageUri}:{ContainerImageTag}",
                Env = new List<string>
                {
                    "MYSQL_ROOT_PASSWORD=Password1",
                    "MYSQL_DATABASE=ShoppingTrack"
                },
                ExposedPorts = new Dictionary<string, EmptyStruct>
                {
                    { port, default }
                },
                HostConfig = new HostConfig
                {
                    PortBindings = new Dictionary<string, IList<PortBinding>>
                    {
                        { port, new List<PortBinding> { new PortBinding { HostPort = port } } }
                    },
                    PublishAllPorts = true
                }
            });

            _containerId = response.ID;

            var isStarted = await _dockerClient.Containers
                .StartContainerAsync(_containerId, null);
            if (!isStarted)
                throw new Exception("Unable to start docker container");
            Console.WriteLine("Container started successfully: {0}", _containerId);
        }
        #endregion
    }
}
