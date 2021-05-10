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
            services.AddMemoryCache();
            services.AddControllers().AddNewtonsoftJson();
            services.AddSingleton<IHelper, Helper>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ShoppingTrackApi", Version = "v1" });
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
                var conn = HostingEnvironment.EnvironmentName == "UnitTesting"
                    ? Configuration["ConnectionString"]
                    : GetConnectionString();

                builder.UseMySql(conn, options =>
                {
                    options.ServerVersion(new Version(5, 7, 32), ServerType.MySql);
                });
            });

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
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ShoppingTrackApi v1"));
            }

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<ShoppingTrackContext>();
                context.Database.Migrate();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
