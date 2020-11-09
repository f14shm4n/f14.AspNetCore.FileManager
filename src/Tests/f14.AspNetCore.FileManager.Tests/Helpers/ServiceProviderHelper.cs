using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace f14.AspNetCore.FileManager.Tests.Helpers
{
    internal static class ServiceProviderHelper
    {
        public static IServiceCollection GetServiceCollection()
        {
            var services = new ServiceCollection();
            services.AddSingleton(provaider => MockHelper.GetWebHostEnvironment());
            services.AddMediatR(typeof(FileManagerAssemblyBeacon).Assembly);
            return services;
        }

        public static IServiceProvider GetServiceProvider() => GetServiceCollection().BuildServiceProvider();
    }
}
