using CarrierSelectorApi.Core.Interfaces;
using CarrierSelectorApi.Core.Utilities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarrierSelectorApi.Core.DependencyResolvers
{
    public static class ServiceRegistration
    {
        public static void AddCoreServices(this IServiceCollection services)
        {
            services.AddScoped<ILoggerService, LoggingService>();
            services.AddTransient<ExceptionMiddleware>();
        }
    }
}