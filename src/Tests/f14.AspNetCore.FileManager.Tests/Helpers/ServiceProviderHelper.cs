using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace f14.AspNetCore.FileManager.Tests.Helpers
{
    internal static class ServiceProviderHelper
    {
        public static IServiceProvider GetService()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton(provaider => MockHelper.GetWebHostEnvironment());
            serviceCollection.AddMediatR(typeof(FileManagerAssemblyBeacon).Assembly);

            return serviceCollection.BuildServiceProvider();
        }
    }
}
