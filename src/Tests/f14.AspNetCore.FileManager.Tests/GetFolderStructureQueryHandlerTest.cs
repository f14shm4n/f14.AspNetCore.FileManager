using f14.AspNetCore.FileManager.Queries;
using f14.IO;
using FluentAssertions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace f14.AspNetCore.FileManager.Tests
{
    public class GetFolderStructureQueryHandlerTest : BaseCommandHandlerTest
    {
        private const string _TestFolderName = "TestFolderToStructObserve";
        private const string __SubTestFolderName = "TestSampleFolder";
        private const string __TestTextFileName = "TestFile.txt";
        private const string __TestJpgFileName = "TestFile.jpg";

        private const string _TestFolderPath = RootTestFolder + "\\" + _TestFolderName;
        private const string __SubTestFolderPath = _TestFolderPath + "\\" + __SubTestFolderName;
        private const string __TestTextFilePath = _TestFolderPath + "\\" + __TestTextFileName;
        private const string __TestJpgFilePath = _TestFolderPath + "\\" + __TestJpgFileName;

        [Fact]
        public async Task GetStruct_without_extensions_filter()
        {
            Directory.CreateDirectory(__SubTestFolderPath);
            FileIO.WriteData(__TestTextFilePath, "0");
            FileIO.WriteData(__TestJpgFilePath, "0");

            var query = new GetFolderStructureQuery
            {
                CurrentFolderPath = _TestFolderName
            };

            var r = await Mediator.Send(query);

            r.FolderCount.Should().Be(1);
            r.FileCount.Should().Be(2);

            Directory.Delete(_TestFolderPath, true);
        }

        [Fact]
        public async Task GetStruct_with_extensions_filter()
        {
            Directory.CreateDirectory(__SubTestFolderPath);
            FileIO.WriteData(__TestTextFilePath, "0");
            FileIO.WriteData(__TestJpgFilePath, "0");

            var query = new GetFolderStructureQuery
            {
                CurrentFolderPath = _TestFolderName,
                FileExtensions = new List<string> { ".jpg" }
            };

            var r = await Mediator.Send(query);

            r.FolderCount.Should().Be(1);
            r.FileCount.Should().Be(1);

            Directory.Delete(_TestFolderPath, true);
        }
    }
}
