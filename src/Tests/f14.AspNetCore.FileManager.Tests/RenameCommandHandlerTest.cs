using f14.AspNetCore.FileManager.Commands;
using FluentAssertions;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace f14.AspNetCore.FileManager.Tests
{
    public class RenameCommandHandlerTest : BaseCommandHandlerTest
    {
        private const string _TestFolderOriginName = "TestFolderToRename";
        private const string _TestFolderNewName = "TestFolderRenamed";
        private const string _TestFolderOriginPath = RootTestFolder + "\\" + _TestFolderOriginName;
        private const string _TestFolderNewPath = RootTestFolder + "\\" + _TestFolderNewName;

        [Fact]
        public async Task Rename_folder()
        {
            Directory.CreateDirectory(_TestFolderOriginPath);

            if (Directory.Exists(_TestFolderNewPath))
            {
                Directory.Delete(_TestFolderNewPath);
            }

            var cmd = new RenameCommand
            {
                OriginName = _TestFolderOriginName,
                NewName = _TestFolderNewName
            };

            await Mediator.Send(cmd);

            Directory.Exists(_TestFolderNewPath).Should().BeTrue();
            Directory.Exists(_TestFolderOriginPath).Should().BeFalse();

            Directory.Delete(_TestFolderNewPath);
        }
    }
}
