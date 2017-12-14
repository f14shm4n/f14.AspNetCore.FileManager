using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using f14.AspNetCore.FileManager;
using f14.AspNetCore.FileManager.Handlers;
using f14.AspNetCore.FileManager.Data.Params;
using Microsoft.AspNetCore.Hosting;

namespace FMTests
{
    public class ServiceCollectionTest
    {
        [Fact]
        public void GetService()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton(provaider => new Mock<IHostingEnvironment>().Object);
            serviceCollection.AddF14FileManager();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var fmHandler = serviceProvider.GetService<IOperationHandler<FolderStructParam>>();

            Assert.NotNull(fmHandler);
        }
    }
}
