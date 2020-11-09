using f14.AspNetCore.FileManager.Commands;
using f14.AspNetCore.FileManager.Tests.Helpers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace f14.AspNetCore.FileManager.Tests
{
    public class ServiceCollectionTest
    {
        [Fact]
        public async Task OverwriteHandler()
        {
            var services = ServiceProviderHelper.GetServiceCollection();
            services.Replace(new ServiceDescriptor(typeof(IRequestHandler<CreateCommand, CreateCommandResult>), typeof(CustomCreateHandler), ServiceLifetime.Transient));

            var serviceProvider = services.BuildServiceProvider();
            var mediator = serviceProvider.GetService<IMediator>();

            await Assert.ThrowsAsync<NotImplementedException>(() => mediator.Send(new CreateCommand()));
        }


        public class CustomCreateHandler : IRequestHandler<CreateCommand, CreateCommandResult>
        {
            public Task<CreateCommandResult> Handle(CreateCommand request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
