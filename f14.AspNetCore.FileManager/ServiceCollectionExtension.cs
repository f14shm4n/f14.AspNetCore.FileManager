using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace f14.AspNetCore.FileManager
{
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// Adds all f14 file manager services with base configuration.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddF14FileManager(this IServiceCollection services)
        {            
            services.AddSingleton<RequestHandlerManager>();
            return services;
        }
        /// <summary>
        /// Adds all f14 file manager services with user configuration.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configure">User configuration.</param>
        /// <returns></returns>
        public static IServiceCollection AddF14FileManager(this IServiceCollection services, Action<RequestHandlerManager> configure)
        {
            services.AddSingleton((provider) =>
            {
                RequestHandlerManager manager = new RequestHandlerManager();
                configure(manager);
                return manager;
            });
            return services;
        }
    }
}
