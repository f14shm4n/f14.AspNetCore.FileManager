using f14.AspNetCore.FileManager;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace FMTests
{
    public class PathHelperTest
    {
        const string ROOT_PATH = "C:\\Temp";
        const string PATH_RELATIVE = "folder";

        private ITestOutputHelper Log;

        public PathHelperTest(ITestOutputHelper log)
        {
            Log = log;
        }

        [Theory]
        [InlineData("")]
        [InlineData("/")]
        [InlineData("\\")]
        [InlineData(PATH_RELATIVE)]
        [InlineData("/" + PATH_RELATIVE)]
        [InlineData(ROOT_PATH + "\\" + PATH_RELATIVE)]
        public void RelativePath(string path)
        {
            var finalPath = PathHelper.GetFullPath(ROOT_PATH, path);

            Log.WriteLine("Input: {0} Result: {1}", path, finalPath);

            if (string.IsNullOrWhiteSpace(path) || path == "/" || path == "\\")
            {
                Assert.Equal(ROOT_PATH, finalPath);
            }
            else
            {
                Assert.Equal(ROOT_PATH + "\\" + PATH_RELATIVE, finalPath);
            }
        }
    }
}
