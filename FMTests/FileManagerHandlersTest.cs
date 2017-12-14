using f14.AspNetCore.FileManager;
using f14.AspNetCore.FileManager.Handlers;
using f14.AspNetCore.FileManager.Data.Params;
using f14.IO;
using Microsoft.AspNetCore.Hosting;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;
using Xunit.Abstractions;
using f14.AspNetCore.FileManager.Data.Results;
using f14.AspNetCore.FileManager.Data;

namespace FMTests
{
    public class FileManagerHandlersTest
    {
        #region vars

        private ITestOutputHelper Log;

        private const string ROOT_TEST_PATH = @"C:\Temp";
        private const string ROOT_NAME = "Temp";

        #endregion

        public FileManagerHandlersTest(ITestOutputHelper console)
        {
            Log = console;
        }

        #region Base tests

        [Fact]
        public void CreateFolderHandler()
        {
            const string TEST_FOLDER_NAME = "TestNewFolder";
            const string TEST_FOLDER_PATH = ROOT_TEST_PATH + "\\" + TEST_FOLDER_NAME;

            var param = new CreateFolderParam
            {
                CurrentFolderPath = "/",
                Name = TEST_FOLDER_NAME
            };

            var result = new CreateFolderHandler(CreateHostingEnv()).Run(param);

            PrintErrors(result.Errors);

            Assert.True(Directory.Exists(TEST_FOLDER_PATH));
            Assert.True(result.Errors.Count == 0);
        }

        [Fact]
        public void DeleteHandler()
        {
            const string TEST_FOLDER_NAME = "TestFolderToDelete";
            const string TEST_FOLDER_PATH = ROOT_TEST_PATH + "\\" + TEST_FOLDER_NAME;

            Directory.CreateDirectory(TEST_FOLDER_PATH);

            Assert.True(Directory.Exists(TEST_FOLDER_PATH));

            var param = new DeleteParam
            {
                CurrentFolderPath = "/",
                Targets = new List<string> { TEST_FOLDER_NAME }
            };

            var result = new DeleteHandler(CreateHostingEnv()).Run(param) as DeleteResult;

            PrintErrors(result.Errors);

            Assert.Equal(1, result.Affected);
            Assert.False(Directory.Exists(TEST_FOLDER_PATH));
            Assert.True(result.Errors.Count == 0);
        }

        [Fact]
        public void RenameHandler()
        {
            const string TEST_FOLDER_NAME = "TestFolderToRename";
            const string TEST_FOLDER_NEW_NAME = "TestFolderRenamed";
            const string TEST_FOLDER_PATH = ROOT_TEST_PATH + "\\" + TEST_FOLDER_NAME;
            const string TEST_FOLDER_NEW_PATH = ROOT_TEST_PATH + "\\" + TEST_FOLDER_NEW_NAME;

            Directory.CreateDirectory(TEST_FOLDER_PATH);

            Assert.True(Directory.Exists(TEST_FOLDER_PATH));

            if (Directory.Exists(TEST_FOLDER_NEW_PATH))
            {
                Directory.Delete(TEST_FOLDER_NEW_PATH);
            }

            var targetToRename = new RenameActionTarget
            {
                OldName = TEST_FOLDER_NAME,
                Name = TEST_FOLDER_NEW_NAME
            };

            var param = new RenameParam
            {
                CurrentFolderPath = "/",
                Targets = new List<RenameActionTarget> { targetToRename }
            };

            var result = new RenameHandler(CreateHostingEnv()).Run(param) as RenameResult;

            PrintErrors(result.Errors);

            Assert.Equal(1, result.Affected);
            Assert.True(Directory.Exists(TEST_FOLDER_NEW_PATH));
            Assert.False(Directory.Exists(TEST_FOLDER_PATH));
            Assert.True(result.Errors.Count == 0);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void FolderStructHandler(bool useExtensionsFilter)
        {
            const string ROOT_ACTION_FOLDER_NAME = "TestFolderToStructObserve";
            const string ROOT_ACTION_FOLDER_PATH = ROOT_TEST_PATH + "\\" + ROOT_ACTION_FOLDER_NAME;
            const string TEST_FOLDER_NAME = "TestSampleFolder";
            const string TEST_FILE_TXT_NAME = "TestFile.txt";
            const string TEST_FILE_JPG_NAME = "TestFile.jpg";
            const string TEST_FOLDER_PATH = ROOT_ACTION_FOLDER_PATH + "\\" + TEST_FOLDER_NAME;
            const string TEST_FILE_TXT_PATH = ROOT_ACTION_FOLDER_PATH + "\\" + TEST_FILE_TXT_NAME;
            const string TEST_FILE_JPG_PATH = ROOT_ACTION_FOLDER_PATH + "\\" + TEST_FILE_JPG_NAME;


            Directory.CreateDirectory(TEST_FOLDER_PATH);
            FileIO.WriteData(TEST_FILE_TXT_PATH, "0");
            FileIO.WriteData(TEST_FILE_JPG_PATH, "0");

            var param = new FolderStructParam
            {
                CurrentFolderPath = ROOT_ACTION_FOLDER_NAME
            };

            if (useExtensionsFilter)
            {
                param.FileExtensions = new string[]
                {
                    ".jpg"
                };
            }

            var result = new FolderStructHandler(CreateHostingEnv()).Run(param) as FolderStructResult;

            PrintErrors(result.Errors);

            Log.WriteLine("Folders:");
            foreach (var f in result.Folders)
            {
                Log.WriteLine($"{f.Name}");
            }
            Log.WriteLine("");
            Log.WriteLine("Files:");
            foreach (var f in result.Files)
            {
                Log.WriteLine($"{f.Name}");
            }

            if (useExtensionsFilter)
            {
                Assert.Equal(1, result.FolderCount);
                Assert.Equal(1, result.FileCount);
            }
            else
            {
                Assert.Equal(1, result.FolderCount);
                Assert.Equal(2, result.FileCount);
            }

            Assert.True(result.Errors.Count == 0);
        }

        [Fact]
        public void MoveHandler_Files()
        {
            const string CURRENT_ROOT = "TestMoveFolder";
            const string CURRENT_ROOT_PATH = ROOT_TEST_PATH + "\\" + CURRENT_ROOT;

            const string TEST_FOLDER_SRC_NAME = "FolderSrc";
            const string TEST_FOLDER_SRC_PATH = CURRENT_ROOT_PATH + "\\" + TEST_FOLDER_SRC_NAME;

            const string TEST_FOLDER_DST_NAME = "FolderDst";
            const string TEST_FOLDER_DST_PATH = CURRENT_ROOT_PATH + "\\" + TEST_FOLDER_DST_NAME;

            const string TEST_FILE_TXT_NAME = "TestFile.txt";
            const string TEST_SRC_FILE_TXT_PATH = TEST_FOLDER_SRC_PATH + "\\" + TEST_FILE_TXT_NAME;
            const string TEST_DST_FILE_TXT_PATH = TEST_FOLDER_DST_PATH + "\\" + TEST_FILE_TXT_NAME;

            Directory.CreateDirectory(TEST_FOLDER_SRC_PATH);
            Directory.CreateDirectory(TEST_FOLDER_DST_PATH);
            FileIO.WriteData(TEST_SRC_FILE_TXT_PATH, "0");
            FileIO.WriteData(TEST_DST_FILE_TXT_PATH, "1");

            var targetToMove = new BaseActionTarget
            {
                IsFile = true,
                Name = TEST_FILE_TXT_NAME
            };

            var param = new MoveParam
            {
                CurrentFolderPath = $"/{CURRENT_ROOT}/{TEST_FOLDER_DST_NAME}",
                SourceDirectory = $"/{CURRENT_ROOT}/{TEST_FOLDER_SRC_NAME}",
                DestinationDirectory = $"/{CURRENT_ROOT}/{TEST_FOLDER_DST_NAME}",
                Targets = new List<BaseActionTarget> { targetToMove }
            };

            var result = new MoveHandler(CreateHostingEnv()).Run(param) as MoveResult;

            PrintErrors(result.Errors);

            string value = FileIO.ReadToEnd(TEST_DST_FILE_TXT_PATH);
            Assert.Equal("0", value);
            Assert.False(File.Exists(TEST_SRC_FILE_TXT_PATH));
            Assert.True(result.Errors.Count == 0);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void MoveHandler_Folders(bool overwrite)
        {
            const string CURRENT_ROOT = "TestMoveFolder";
            const string CURRENT_ROOT_PATH = ROOT_TEST_PATH + "\\" + CURRENT_ROOT;

            const string TEST_FOLDER_SRC_NAME = "FolderSrc";
            const string TEST_FOLDER_SRC_PATH = CURRENT_ROOT_PATH + "\\" + TEST_FOLDER_SRC_NAME;

            const string TEST_FOLDER_DST_NAME = "FolderDst";
            const string TEST_FOLDER_DST_PATH = CURRENT_ROOT_PATH + "\\" + TEST_FOLDER_DST_NAME;

            const string TEST_SUB_FOLDER_NAME = "SubFolder";
            const string TEST_SUB_FOLDER_SRC_PATH = TEST_FOLDER_SRC_PATH + "\\" + TEST_SUB_FOLDER_NAME;
            const string TEST_SUB_FOLDER_DST_PATH = TEST_FOLDER_DST_PATH + "\\" + TEST_SUB_FOLDER_NAME;

            const string TEST_FILE_TXT_NAME = "TestFile.txt";
            const string TEST_SUB_SRC_FILE_TXT_PATH = TEST_SUB_FOLDER_SRC_PATH + "\\" + TEST_FILE_TXT_NAME;
            const string TEST_SUB_DST_FILE_TXT_PATH = TEST_SUB_FOLDER_DST_PATH + "\\" + TEST_FILE_TXT_NAME;

            Directory.CreateDirectory(TEST_SUB_FOLDER_SRC_PATH);
            Directory.CreateDirectory(TEST_SUB_FOLDER_DST_PATH);
            FileIO.WriteData(TEST_SUB_SRC_FILE_TXT_PATH, "0");
            FileIO.WriteData(TEST_SUB_DST_FILE_TXT_PATH, "1");

            var targetToMove = new BaseActionTarget
            {
                Name = TEST_SUB_FOLDER_NAME
            };

            var param = new MoveParam
            {
                SourceDirectory = $"/{CURRENT_ROOT}/{TEST_FOLDER_SRC_NAME}",
                DestinationDirectory = $"/{CURRENT_ROOT}/{TEST_FOLDER_DST_NAME}",
                Targets = new List<BaseActionTarget> { targetToMove },
                Overwrite = overwrite
            };

            var result = new MoveHandler(CreateHostingEnv()).Run(param) as MoveResult;

            PrintErrors(result.Errors);

            string value = FileIO.ReadToEnd(TEST_SUB_DST_FILE_TXT_PATH);
            if (overwrite)
            {
                Assert.Equal("0", value);
            }
            else
            {
                Assert.Equal("1", value);
            }
            Assert.False(Directory.Exists(TEST_SUB_FOLDER_SRC_PATH));
            Assert.True(result.Errors.Count == 0);
        }

        [Fact]
        public void CopyHandler()
        {
            const string CURRENT_ROOT = "TestCopyFolder";
            const string CURRENT_ROOT_PATH = ROOT_TEST_PATH + "\\" + CURRENT_ROOT;

            const string TEST_FOLDER_SRC_NAME = "FolderSrc";
            const string TEST_FOLDER_SRC_PATH = CURRENT_ROOT_PATH + "\\" + TEST_FOLDER_SRC_NAME;

            const string TEST_FOLDER_DST_NAME = "FolderDst";
            const string TEST_FOLDER_DST_PATH = CURRENT_ROOT_PATH + "\\" + TEST_FOLDER_DST_NAME;

            const string TEST_FILE_TXT_NAME = "TestFile.txt";
            const string TEST_SRC_FILE_TXT_PATH = TEST_FOLDER_SRC_PATH + "\\" + TEST_FILE_TXT_NAME;
            const string TEST_DST_FILE_TXT_PATH = TEST_FOLDER_DST_PATH + "\\" + TEST_FILE_TXT_NAME;

            Directory.CreateDirectory(TEST_FOLDER_SRC_PATH);
            Directory.CreateDirectory(TEST_FOLDER_DST_PATH);
            FileIO.WriteData(TEST_SRC_FILE_TXT_PATH, "0");
            FileIO.WriteData(TEST_DST_FILE_TXT_PATH, "1");

            var targetToCopy = new BaseActionTarget
            {
                IsFile = true,
                Name = TEST_FILE_TXT_NAME
            };

            var param = new CopyParam
            {
                SourceDirectory = $"/{CURRENT_ROOT}/{TEST_FOLDER_SRC_NAME}",
                DestinationDirectory = $"/{CURRENT_ROOT}/{TEST_FOLDER_DST_NAME}",
                Targets = new List<BaseActionTarget> { targetToCopy },
                Overwrite = true
            };

            var result = new CopyHandler(CreateHostingEnv()).Run(param) as CopyResult;

            PrintErrors(result.Errors);

            string value = FileIO.ReadToEnd(TEST_DST_FILE_TXT_PATH);
            Assert.Equal("0", value);
            Assert.True(File.Exists(TEST_SRC_FILE_TXT_PATH));
            Assert.True(result.Errors.Count == 0);
        }

        #endregion

        #region Json tests

        [Fact]
        public void CreateFolderHandlerJsonUsing()
        {
            const string TEST_FOLDER_NAME = "TestNewFolder";
            const string TEST_FOLDER_PATH = ROOT_TEST_PATH + "\\" + TEST_FOLDER_NAME;

            var json = JsonConvert.SerializeObject(new CreateFolderParam
            {
                CurrentFolderPath = "/",
                Name = TEST_FOLDER_NAME
            });

            var result = new CreateFolderHandler(CreateHostingEnv()).Run(json);

            PrintErrors(result.Errors);

            Assert.True(Directory.Exists(TEST_FOLDER_PATH));
            Assert.True(result.Errors.Count == 0);
        }

        #endregion

        #region Helpers

        private IHostingEnvironment CreateHostingEnv()
        {
            var moqEnv = new Mock<IHostingEnvironment>();
            moqEnv.Setup(x => x.WebRootPath).Returns(ROOT_TEST_PATH);
            return moqEnv.Object;
        }

        private void PrintErrors(IEnumerable<string> errors)
        {
            foreach (var err in errors)
            {
                Log.WriteLine(err);
            }
        }

        private string SerializeToJson(object obj)
        {
            var jsonSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            return JsonConvert.SerializeObject(obj, jsonSettings);
        }

        #endregion
    }
}
