using f14.AspNetCore.FileManager;
using f14.AspNetCore.FileManager.RequestHandlers;
using f14.AspNetCore.FileManager.RequestModels;
using f14.AspNetCore.FileManager.ResponseModels;
using f14.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
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

        public RequestHandlerManagerTest(ITestOutputHelper console)
        {
            _handlerManager = new RequestHandlerManager();
            _log = console;
        }

        private DeleteRequestData MakeSampleDeleteRequestData() => new DeleteRequestData
        {
            Type = RequestHandlerManager.RequestHandlerKeys.Delete,
            Path = "Temp",
            ObjectNames = new System.Collections.Generic.List<string>()
            {
                "fm_test_123.txt",
                "fm_test_backlog.txt",
                "fm_test_wnd.dat"
            }
        };

        private void MakeTestFilesForDelete()
        {
            Directory.CreateDirectory(@"C:\Temp");

            foreach(var n in MakeSampleDeleteRequestData().ObjectNames)
            {
                FileIO.WriteData($@"C:\Temp\{n}", "Sample data.");
            }
        }

        [Fact]
        public void ParseRequestData_Test()
        {
            DeleteRequestData sampleRData = MakeSampleDeleteRequestData();

            var jsonSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            var jsonStr = JsonConvert.SerializeObject(sampleRData, jsonSettings);

            var resultRData = _handlerManager.ParseRequestData<DeleteRequestData>(jsonStr);

            Assert.Equal(RequestHandlerManager.RequestHandlerKeys.Delete, resultRData.Type);
            Assert.Equal("Temp", resultRData.Path);
            Assert.Equal(3, resultRData.ObjectNames.Count);
        }

        [Fact]
        public void GetRequestHandler_Test()
        {
            DeleteRequestData sampleRData = MakeSampleDeleteRequestData();

            var jsonSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            var jsonStr = JsonConvert.SerializeObject(sampleRData, jsonSettings);

            var requestHandler = _handlerManager.GetRequestHandler(jsonStr);

            Assert.True(requestHandler is DeleteHandler);
        }

        [Fact]
        public void HandleDeleteRequest_Test()
        {
            MakeTestFilesForDelete();

            DeleteRequestData sampleRData = MakeSampleDeleteRequestData();

            var jsonSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            var jsonStr = JsonConvert.SerializeObject(sampleRData, jsonSettings);

            var responseData = _handlerManager.HandleRequest<DeleteResponseData>(jsonStr, new ServiceVars(@"C:\"));

            Assert.Equal(3, responseData.Affected);
            Assert.True(responseData.Errors.Count() == 0);
        }
    }
}
