using f14.AspNetCore.FileManager.Commands;
using f14.IO;
using FluentAssertions;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace f14.AspNetCore.FileManager.Tests
{
    public class CopyCommandHandlerTest : BaseCommandHandlerTest
    {
        private const string _TestFolderName = "TestCopyFolder";
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
        public async Task Copy_file()
        {
            Directory.CreateDirectory(__SubTestSrcFolderPath);
            Directory.CreateDirectory(__SubTestDstFolderPath);
            FileIO.WriteData(__TestSrcFilePath, "0");

            var cmd = new CopyCommand
            {
                CurrentFolderPath = $"/{_TestFolderName}/{__SubTestDstFolderName}",
                SourceDirectory = $"/{_TestFolderName}/{__SubTestSrcFolderName}",
                Name = __TestFileName,
                IsFile = true
            };

            await Mediator.Send(cmd);

            FileIO.ReadToEnd(__TestDstFilePath).Should().Be("0");

            Directory.Delete(_TestFolderPath, true);
        }

        [Fact]
        public async Task Copy_file_with_overwrite()
        {
            Directory.CreateDirectory(__SubTestSrcFolderPath);
            Directory.CreateDirectory(__SubTestDstFolderPath);
            FileIO.WriteData(__TestSrcFilePath, "0");
            FileIO.WriteData(__TestDstFilePath, "1");

            var cmd = new CopyCommand
            {
                CurrentFolderPath = $"/{_TestFolderName}/{__SubTestDstFolderName}",
                SourceDirectory = $"/{_TestFolderName}/{__SubTestSrcFolderName}",
                Name = __TestFileName,
                IsFile = true,
                Overwrite = true
            };

            await Mediator.Send(cmd);

            FileIO.ReadToEnd(__TestDstFilePath).Should().Be("0");

            Directory.Delete(_TestFolderPath, true);
        }

        [Fact]
        public async Task Copy_folder()
        {
            Directory.CreateDirectory(__SubTestSrcFolderPath);
            Directory.CreateDirectory(__SubTestDstFolderPath);
            FileIO.WriteData(__TestSrcFilePath, "0");

            var cmd = new CopyCommand
            {
                CurrentFolderPath = $"/{_TestFolderName}/{__SubTestDstFolderName}",
                SourceDirectory = $"/{_TestFolderName}",
                Name = __SubTestSrcFolderName
            };

            await Mediator.Send(cmd);

            FileIO.ReadToEnd(___TestScrInDstFilePath).Should().Be("0");

            Directory.Delete(_TestFolderPath, true);
        }
    }
}
