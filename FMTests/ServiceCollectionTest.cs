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
using f14.AspNetCore.FileManager.Data.Results;
using System.IO;
using System.Linq;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace FMTests
{
    public class ServiceCollectionTest
    {
        [Fact]
        public void GetService()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton(provaider => new Mock<IHostingEnvironment>().Object);
            serviceCollection.AddFileManagerHandlers();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var fmHandler = serviceProvider.GetService<IFolderStructHandler>();

            Assert.NotNull(fmHandler);
        }

        [Fact]
        public void OverwriteHandler()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton(provaider => new Mock<IHostingEnvironment>().Object);
            serviceCollection.AddFileManagerHandlers();

            serviceCollection.Replace(new ServiceDescriptor(typeof(ICreateFolderHandler), typeof(CustomCreateFolderHandler), ServiceLifetime.Transient));            
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var fmHandler = serviceProvider.GetService<ICreateFolderHandler>();

            Assert.NotNull(fmHandler);
            Assert.IsType<CustomCreateFolderHandler>(fmHandler);
        }


        public class CustomCreateFolderHandler : BaseOperationHandler<CreateFolderParam, CreateFolderResult>, ICreateFolderHandler
        {
            public CustomCreateFolderHandler(IHostingEnvironment env) : base(new CreateFolderResult(), env.WebRootPath)
            {
            }

            public override CreateFolderResult Run(CreateFolderParam param)
            {
                string fullPath = PathHelper.GetFullPath(RootPath, param.CurrentFolderPath);

                if (Directory.Exists(fullPath))
                {
                    if (!param.Name.Contains('\\') && !param.Name.Contains('/'))
                    {
                        string fullPathToNewFolder = Path.Combine(fullPath, param.Name + "_customHandler");

                        try
                        {
                            Directory.CreateDirectory(fullPathToNewFolder);
                        }
                        catch (Exception ex)
                        {
                            OnErrorReport($"Unable to create folder. Ex: {ex.Message}");
                        }
                    }
                    else
                    {
                        OnErrorReport("The new folder name contains invalid chars. New folder name cannot contains slashes.");
                    }
                }
                else
                {
                    OnErrorReport("The work folder does not exists on the real file system. Check work folder path: " + param.CurrentFolderPath);
                }

                return Result;
            }
        }
    }
}
