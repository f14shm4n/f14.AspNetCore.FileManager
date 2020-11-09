using f14.AspNetCore.FileManager.Commands;
using f14.IO;
using FluentAssertions;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace f14.AspNetCore.FileManager.Tests
{
    public class DeleteCommandHandlerTest : BaseCommandHandlerTest
    {
        private const string _TestFolderName = "TestFolderToDelete";
        private const string _TestFileName = "TestFileToDelete.txt";
        private const string _TestFolderPath = RootTestFolder + "\\" + _TestFolderName;
        private const string _TestFilePath = RootTestFolder + "\\" + _TestFileName;

        [Fact]
        public async Task Delete_folder()
        {
            Directory.CreateDirectory(_TestFolderPath);

            var cmd = new DeleteCommand
            {
                CurrentFolderPath = "/",
                TargetName = _TestFolderName
            };

            Directory.Exists(_TestFolderPath).Should().BeTrue();

            await Mediator.Send(cmd);

            Directory.Exists(_TestFolderPath).Should().BeFalse();
        }

        [Fact]
        public async Task Delete_file()
        {
            FileIO.WriteData(_TestFilePath, "sample text data.");

            var cmd = new DeleteCommand
            {
                CurrentFolderPath = "/",
                TargetName = _TestFileName,
                IsFile = true
            };

            File.Exists(_TestFilePath).Should().BeTrue();

            await Mediator.Send(cmd);

            File.Exists(_TestFilePath).Should().BeFalse();
        }
    }
}
