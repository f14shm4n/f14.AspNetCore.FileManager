using f14.AspNetCore.FileManager;
using f14.AspNetCore.FileManager.RequestHandlers;
using f14.AspNetCore.FileManager.RequestModels;
using f14.AspNetCore.FileManager.ResponseModels;
using f14.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace FMTests
{
    public class RequestHandlerManagerTest
    {
        private RequestHandlerManager _handlerManager;
        private ITestOutputHelper _log;

        private const string rootPath = @"C:\Temp";
        private const string rootName = "Temp";
        private const string subFolder_1 = "fm_test_sub1";
        private const string subFolder_2 = "fm_test_sub2";
        private const string subFolder_1_1 = "fm_test_sub1_1";

        private const string newFolderName = "fm_new_folder";

        private List<string> _testFileList_1 = new List<string>()
        {
            "fm_test_123.txt",
            "fm_test_backlog.txt",
            "fm_test_wnd.dat"
        };
        private List<string> _testFileList_2 = new List<string>()
        {
            "fm_test_trash.txt",
            "fm_test_toDo.txt",
            "fm_test_rework.bak",
            "fm_test_hide.txt",
            "fm_test_show.txt",
        };
        private List<string> _testFileList_3 = new List<string>()
        {
            "fm_test_hide.txt",
        };

        public RequestHandlerManagerTest(ITestOutputHelper console)
        {
            _handlerManager = new RequestHandlerManager();
            _log = console;
        }

        // Create files for the test.
        private void CreateTestFileSystemObjects()
        {
            Action<string, IEnumerable<string>> fileMaker = (rPath, names) =>
             {
                 foreach (var n in names)
                 {
                     string fpath = Path.Combine(rPath, n);
                     if (!File.Exists(fpath))
                     {
                         FileIO.WriteData(fpath, "Sample data. " + fpath);
                     }
                 }
             };

            Directory.CreateDirectory(rootPath);
            Directory.CreateDirectory(Path.Combine(rootPath, subFolder_1));
            Directory.CreateDirectory(Path.Combine(rootPath, subFolder_1, subFolder_1_1));
            Directory.CreateDirectory(Path.Combine(rootPath, subFolder_2));

            fileMaker(rootPath, _testFileList_1);
            fileMaker(Path.Combine(rootPath, subFolder_1), _testFileList_2);
            fileMaker(Path.Combine(rootPath, subFolder_1, subFolder_1_1), _testFileList_3);
        }

        private void PrintErrors(IEnumerable<string> errors)
        {
            foreach (var err in errors)
            {
                _log.WriteLine(err);
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

        #region Check supports

        [Fact]
        public void ParseRequestData_Test()
        {
            DeleteRequestData sampleRData = new DeleteRequestData
            {
                Type = RequestHandlerManager.RequestHandlerKeys.Delete,
                Path = rootName,
                ObjectNames = _testFileList_1
            };

            var jsonSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            var jsonStr = JsonConvert.SerializeObject(sampleRData, jsonSettings);

            var resultRData = _handlerManager.ParseRequestData<DeleteRequestData>(jsonStr);

            Assert.Equal(RequestHandlerManager.RequestHandlerKeys.Delete, resultRData.Type);
            Assert.Equal("Temp", resultRData.Path);
        }

        [Fact]
        public void GetRequestHandler_Test()
        {
            var sampleRData = new RequestData { Type = RequestHandlerManager.RequestHandlerKeys.Delete };

            var jsonSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            var jsonStr = JsonConvert.SerializeObject(sampleRData, jsonSettings);

            var requestHandler = _handlerManager.GetRequestHandler(jsonStr);

            Assert.True(requestHandler is DeleteHandler);
        }

        #endregion

        #region Test handler requests

        [Fact]
        public void Handle_DeleteRequest()
        {
            CreateTestFileSystemObjects();

            var sampleRData = new DeleteRequestData
            {
                Type = RequestHandlerManager.RequestHandlerKeys.Delete,
                Path = rootName,
                ObjectNames = _testFileList_1
            };

            var responseData = _handlerManager.HandleRequest<DeleteResponseData>(SerializeToJson(sampleRData), new ServiceVars(@"C:\"));

            PrintErrors(responseData.Errors);

            Assert.Equal(3, responseData.Affected);
            Assert.True(responseData.Errors.Count() == 0);
        }

        [Fact]
        public void Handle_RenameRequest()
        {
            CreateTestFileSystemObjects();

            var sampleRData = new RenameRequestData
            {
                Type = RequestHandlerManager.RequestHandlerKeys.Rename,
                Path = rootName,
                Renames = new List<RenameRequestData.RenameFileInfo>
                {
                    new RenameRequestData.RenameFileInfo{ IsFile = true, OldName = _testFileList_1[0], NewName = "renamed_" + _testFileList_1[0] },
                    new RenameRequestData.RenameFileInfo{ IsFile = true, OldName = _testFileList_1[1], NewName = "renamed_" + _testFileList_1[1] }
                }
            };

            var responseData = _handlerManager.HandleRequest<RenameResponseData>(SerializeToJson(sampleRData), new ServiceVars(@"C:\"));

            PrintErrors(responseData.Errors);

            Assert.Equal(2, responseData.Affected);
            Assert.True(responseData.Errors.Count() == 0);
        }

        [Theory]
        [InlineData(RequestHandlerManager.RequestHandlerKeys.Move)]
        [InlineData(RequestHandlerManager.RequestHandlerKeys.Copy)]
        public void Handle_MoveRequest_Files(string requestType)
        {
            CreateTestFileSystemObjects();

            var sampleRData = new MoveRequestData
            {
                Type = requestType,
                SourceDirectory = Path.Combine(rootPath, subFolder_1),
                DestinationDirectory = Path.Combine(rootPath, subFolder_1, subFolder_1_1),
                Overwrite = true,
                Targets = new List<MoveRequestData.MoveTarget>
                {
                    new MoveRequestData.MoveTarget{ IsFile = true, Name = _testFileList_3[0] }
                }
            };

            var responseData = _handlerManager.HandleRequest<ResponseData>(SerializeToJson(sampleRData), new ServiceVars(@"C:\"));

            PrintErrors(responseData.Errors);

            var fileData = FileIO.ReadToEnd(Path.Combine(rootPath, subFolder_1, subFolder_1_1, _testFileList_3[0]));

            Assert.Equal($@"Sample data. {Path.Combine(rootPath, subFolder_1, _testFileList_3[0])}", fileData);

            if (requestType == RequestHandlerManager.RequestHandlerKeys.Move)
            {
                Assert.True(!File.Exists(Path.Combine(rootPath, subFolder_1, _testFileList_3[0])));
            }

            Assert.True(responseData.Errors.Count() == 0);
        }

        [Theory]
        [InlineData(RequestHandlerManager.RequestHandlerKeys.Move)]
        [InlineData(RequestHandlerManager.RequestHandlerKeys.Copy)]
        public void Handle_MoveRequest_Folders(string requestType)
        {
            CreateTestFileSystemObjects();

            var sampleRData = new MoveRequestData
            {
                Type = requestType,
                SourceDirectory = Path.Combine(rootPath, subFolder_1),
                DestinationDirectory = Path.Combine(rootPath, subFolder_2),
                Overwrite = true,
                Targets = new List<MoveRequestData.MoveTarget>
                {
                    new MoveRequestData.MoveTarget{ Name = subFolder_1_1 }
                }
            };

            var responseData = _handlerManager.HandleRequest<ResponseData>(SerializeToJson(sampleRData), new ServiceVars(@"C:\"));

            PrintErrors(responseData.Errors);

            Assert.True(Directory.Exists(Path.Combine(rootPath, subFolder_2, subFolder_1_1)));

            if (requestType == RequestHandlerManager.RequestHandlerKeys.Move)
            {
                Assert.True(!Directory.Exists(Path.Combine(rootPath, subFolder_1, subFolder_1_1)));
            }
        }

        [Theory]
        [InlineData(rootName)]
        [InlineData(rootName + "/" + subFolder_1)]
        public void Handle_StructRequest(string pathToFolder)
        {
            CreateTestFileSystemObjects();

            var sampleRData = new StructRequestData
            {
                Type = RequestHandlerManager.RequestHandlerKeys.Struct,
                Path = pathToFolder,
            };

            var responseData = _handlerManager.HandleRequest<StructResponseData>(SerializeToJson(sampleRData), new ServiceVars(@"C:\"));

            PrintErrors(responseData.Errors);

            _log.WriteLine("Folders:");
            foreach (var f in responseData.Folders)
            {
                _log.WriteLine($"{f.Name}");
            }
            _log.WriteLine("Files:");
            foreach (var f in responseData.Files)
            {
                _log.WriteLine($"{f.Name}");
            }

            if (pathToFolder == rootName)
            {
                Assert.Equal(3, responseData.FileCount);
                Assert.Equal(2, responseData.FolderCount);
            }
            if (pathToFolder.EndsWith(subFolder_1))
            {
                Assert.Equal(5, responseData.FileCount);
                Assert.Equal(1, responseData.FolderCount);
            }

            Assert.True(responseData.Errors.Count() == 0);
        }

        [Theory]
        [InlineData(rootName, newFolderName)]
        [InlineData(rootName + "/" + subFolder_1, newFolderName)]
        public void Handle_CreateFolderRequest(string path, string name)
        {
            CreateTestFileSystemObjects();

            var sampleRData = new CreateRequestData
            {
                Type = RequestHandlerManager.RequestHandlerKeys.CreateFolder,
                Path = path,
                Name = name
            };

            var responseData = _handlerManager.HandleRequest<ResponseData>(SerializeToJson(sampleRData), new ServiceVars(@"C:\"));

            PrintErrors(responseData.Errors);

            if (path == rootName)
            {
                Assert.True(Directory.Exists(Path.Combine(rootPath, newFolderName)));
            }
            if (path.EndsWith(subFolder_1))
            {
                Assert.True(Directory.Exists(Path.Combine(rootPath, subFolder_1, newFolderName)));
            }

            Assert.True(responseData.Errors.Count() == 0);
        }

        #endregion
    }
}
