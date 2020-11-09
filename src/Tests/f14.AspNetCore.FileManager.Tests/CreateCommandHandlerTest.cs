using f14.AspNetCore.FileManager.Commands;
using FluentAssertions;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace f14.AspNetCore.FileManager.Tests
{
    public class CreateCommandHandlerTest : BaseCommandHandlerTest
    {
        private const string _TestFolderName = "TestNewFolder";
        private const string _TestFolderPath = RootTestFolder + "\\" + _TestFolderName;

        [Fact]
        public async Task Result_should_be_created()
        {
            var cmd = new CreateCommand
            {
                CurrentFolderPath = "/",
                Name = _TestFolderName
            };

            var r = await Mediator.Send(cmd);

            r.Should().Be(CreateCommandResult.Created);

            Directory.Delete(_TestFolderPath);
        }

        [Fact]
        public async Task Result_should_be_exists()
        {
            var cmd = new CreateCommand
            {
                CurrentFolderPath = "/",
                Name = _TestFolderName
            };

            var r = await Mediator.Send(cmd);

            r.Should().Be(CreateCommandResult.Created);

            r = await Mediator.Send(cmd);

            r.Should().Be(CreateCommandResult.Exists);

            Directory.Delete(_TestFolderPath);
        }
    }
}
