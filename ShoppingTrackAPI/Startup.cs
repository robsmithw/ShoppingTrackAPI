using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
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
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var baseConnString = Configuration.GetConnectionString("DefaultConnection");
            var userName = Configuration.GetValue<string>("Keys:UserId");
            var passwd = Configuration.GetValue<string>("Keys:Pass");
            var populatedConnString = String.Format(baseConnString, userName, passwd);
            services.AddMemoryCache();
            services.AddControllers().AddNewtonsoftJson();
            services.AddSingleton<IHelper, Helper>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ShoppingTrackApi", Version = "v1" });
            });
            services.AddDbContext<ShoppingTrackContext>(options =>
                options.UseMySql(populatedConnString));
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
