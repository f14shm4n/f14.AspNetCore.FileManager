using f14.AspNetCore.FileManager.Commands;
using f14.IO;
using FluentAssertions;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace f14.AspNetCore.FileManager.Tests
{
    public class MoveCommandHandlerTest : BaseCommandHandlerTest
    {
        private const string _TestFolderName = "TestMoveFolder";
        private const string __SubTestSrcFolderName = "FolderSrc";
        private const string __SubTestDstFolderName = "FolderDst";
        private const string __TestFileName = "TestFile.txt";

        private const string _TestFolderPath = RootTestFolder + "\\" + _TestFolderName;
        private const string __SubTestSrcFolderPath = _TestFolderPath + "\\" + __SubTestSrcFolderName;
        private const string __SubTestDstFolderPath = _TestFolderPath + "\\" + __SubTestDstFolderName;
        private const string __TestSrcFilePath = __SubTestSrcFolderPath + "\\" + __TestFileName;
        private const string __TestDstFilePath = __SubTestDstFolderPath + "\\" + __TestFileName;
        private const string ___TestScrInDstFolderPath = __SubTestDstFolderPath + "\\" + __SubTestSrcFolderName;
        private const string ___TestScrInDstFilePath = ___TestScrInDstFolderPath + "\\" + __TestFileName;

        [Fact]
        public async Task Move_file()
        {
            Directory.CreateDirectory(__SubTestSrcFolderPath);
            Directory.CreateDirectory(__SubTestDstFolderPath);
            FileIO.WriteData(__TestSrcFilePath, "0");

            var cmd = new MoveCommand
            {
                CurrentFolderPath = $"/{_TestFolderName}/{__SubTestDstFolderName}",
                SourceDirectory = $"/{_TestFolderName}/{__SubTestSrcFolderName}",
                Name = __TestFileName,
                IsFile = true
            };

            await Mediator.Send(cmd);

            FileIO.ReadToEnd(__TestDstFilePath).Should().Be("0");

            File.Exists(__TestSrcFilePath).Should().BeFalse();

            Directory.Delete(_TestFolderPath, true);
        }

        [Fact]
        public async Task Move_file_with_overwrite()
        {
            Directory.CreateDirectory(__SubTestSrcFolderPath);
            Directory.CreateDirectory(__SubTestDstFolderPath);
            FileIO.WriteData(__TestSrcFilePath, "0");
            FileIO.WriteData(__TestDstFilePath, "1");

            var cmd = new MoveCommand
            {
                CurrentFolderPath = $"/{_TestFolderName}/{__SubTestDstFolderName}",
                SourceDirectory = $"/{_TestFolderName}/{__SubTestSrcFolderName}",
                Name = __TestFileName,
                IsFile = true,
                Overwrite = true
            };

            await Mediator.Send(cmd);

            FileIO.ReadToEnd(__TestDstFilePath).Should().Be("0");

            File.Exists(__TestSrcFilePath).Should().BeFalse();

            Directory.Delete(_TestFolderPath, true);
        }

        [Fact]
        public async Task Move_folder()
        {
            Directory.CreateDirectory(__SubTestSrcFolderPath);
            Directory.CreateDirectory(__SubTestDstFolderPath);
            FileIO.WriteData(__TestSrcFilePath, "0");

            var cmd = new MoveCommand
            {
                CurrentFolderPath = $"/{_TestFolderName}/{__SubTestDstFolderName}",
                SourceDirectory = $"/{_TestFolderName}",
                Name = __SubTestSrcFolderName
            };

            await Mediator.Send(cmd);

            FileIO.ReadToEnd(___TestScrInDstFilePath).Should().Be("0");

            File.Exists(__TestSrcFilePath).Should().BeFalse();

            Directory.Delete(_TestFolderPath, true);
        }

        [Fact]
        public async Task Move_folder_with_overwrite()
        {
            Directory.CreateDirectory(__SubTestSrcFolderPath);
            Directory.CreateDirectory(___TestScrInDstFolderPath);
            FileIO.WriteData(__TestSrcFilePath, "0");
            FileIO.WriteData(___TestScrInDstFilePath, "1");

            var cmd = new MoveCommand
            {
                CurrentFolderPath = $"/{_TestFolderName}/{__SubTestDstFolderName}",
                SourceDirectory = $"/{_TestFolderName}",
                Name = __SubTestSrcFolderName,
                Overwrite = true
            };

            await Mediator.Send(cmd);

            FileIO.ReadToEnd(___TestScrInDstFilePath).Should().Be("0");

            File.Exists(__TestSrcFilePath).Should().BeFalse();

            Directory.Delete(_TestFolderPath, true);
        }
    }
}
