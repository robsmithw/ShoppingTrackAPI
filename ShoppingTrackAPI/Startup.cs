using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ShoppingTrackAPI.Models;
using ShoppingTrackAPI.HelperFunctions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ShoppingTrackAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment HostingEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            if (HostingEnvironment.IsDevelopment())
            {
                services.AddCors(options => 
                {
                    options.AddDefaultPolicy(
                        builder => 
                        {
                            builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                        }
                    );
                });
            }
            services.AddMemoryCache();
            services.AddControllers().AddNewtonsoftJson();
            services.AddSingleton<IHelper, Helper>();
            services.AddSingleton<ICognitoUserManagement, CognitoUserManagement>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ShoppingTrackApi", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                                Enter 'Bearer' [space] and then your token in the text input below.
                                \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = GetCognitoTokenValidationParams();
                });
            //  services.AddDbContext<ShoppingTrackContext>(options =>
            //      options.UseMySql(populatedConnString));

            services.AddDbContextPool<ShoppingTrackContext>((srv, builder) =>
            {
                if (HostingEnvironment.IsDevelopment())
                {
                    builder.EnableDetailedErrors();
                    builder.EnableSensitiveDataLogging();
                }
                var conn = Configuration.GetConnectionString("DefaultConnection");

                Console.WriteLine("Connection string: {0}", conn);

                builder.UseMySql(conn, options =>
                {
                    options.ServerVersion(new Version(5, 7, 32), ServerType.MySql);
                });
            });

        }

        private TokenValidationParameters GetCognitoTokenValidationParams()
        {
            var cognitoIssuer = $"https://cognito-idp.{Configuration["AWS:Region"]}.amazonaws.com/{Configuration["AWS:UserPoolId"]}";
            var jwtKeySetUrl = $"{cognitoIssuer}/.well-known/jwks.json";
            var cognitoAudience = Configuration["AWS:AppClientId"];
            
            return new TokenValidationParameters
            {
                IssuerSigningKeyResolver = (s, securityToken, identifier, parameters) =>
                {
                    // get JsonWebKeySet from AWS
                    var json = new WebClient().DownloadString(jwtKeySetUrl);
                    
                    // serialize the result
                    var keys = JsonConvert.DeserializeObject<JsonWebKeySet>(json).Keys;
                    
                    // cast the result to be the type expected by IssuerSigningKeyResolver
                    return (IEnumerable<SecurityKey>)keys;
                },
                ValidIssuer = cognitoIssuer,
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidAudience = cognitoAudience,
                ValidateAudience = false
            };
        }

        public string GetConnectionString()
        {
            var baseConnString = Configuration.GetConnectionString("DefaultConnection");
            var userName = Configuration.GetValue<string>("Keys:UserId");
            var passwd = Configuration.GetValue<string>("Keys:Pass");
            var populatedConnString = !String.IsNullOrEmpty(baseConnString) && !String.IsNullOrEmpty(userName) && !String.IsNullOrEmpty(passwd) 
                ? String.Format(baseConnString, userName, passwd)
                : String.Empty;
            return populatedConnString;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseCors();
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ShoppingTrackApi v1"));
            }

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<ShoppingTrackContext>();
                context.Database.Migrate();
            }

            app.UseAuthentication();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
