using Microsoft.AspNetCore.Hosting;
using Moq;

namespace f14.AspNetCore.FileManager.Tests.Helpers
{
    internal static class MockHelper
    {
        public static IWebHostEnvironment GetWebHostEnvironment()
        {
            var moqEnv = new Mock<IWebHostEnvironment>();
            moqEnv.Setup(x => x.WebRootPath).Returns(BaseCommandHandlerTest.RootTestFolder);
            return moqEnv.Object;
        }
    }
}
