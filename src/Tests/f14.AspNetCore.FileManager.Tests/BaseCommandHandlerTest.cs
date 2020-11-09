using f14.AspNetCore.FileManager.Tests.Helpers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace f14.AspNetCore.FileManager.Tests
{
    public abstract class BaseCommandHandlerTest
    {
        public const string RootTestFolder = "C:\\Temp";

        private IMediator _mediator;

        public IMediator Mediator
        {
            get
            {
                if (_mediator == null)
                {
                    _mediator = ServiceProviderHelper.GetService().GetService<IMediator>();
                }
                return _mediator;
            }
        }
    }
}
