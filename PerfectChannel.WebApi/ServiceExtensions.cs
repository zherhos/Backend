using Microsoft.Extensions.DependencyInjection;
using PerfectChannel.WebApi.Interfaces;
using PerfectChannel.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerfectChannel.WebApi
{
    public static class ServiceExtensions
    {
        public static void ConfigureRepository(this IServiceCollection services)
        {
            services.AddScoped<IRepository<ITask>, TaskRepository>();
        }
    }
}
