
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PerfectChannel.WebApi.Models;
using System;

namespace PerfectChannel.WebApi
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
            ConfigureCors(services);
            ConfigureDBContext(services);
            services.ConfigureRepository();
            services.AddControllers();
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<TaskDBContext>();
                context.Database.Migrate();
            }
        }

        private void ConfigureCors(IServiceCollection services)
        {
            var frontendServerUrl = Configuration.GetValue<string>("FrontendServerUrl");
            services.AddCors(options =>
                options.AddPolicy("AllowOrigin", builder =>
                    builder.WithOrigins(frontendServerUrl)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                )
            );
        }

        private void ConfigureDBContext(IServiceCollection services)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<TaskDBContext>(options => options.UseSqlServer(connectionString));
        }
    }
}
