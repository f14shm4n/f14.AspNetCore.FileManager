using Xunit;

namespace f14.AspNetCore.FileManager.Tests
{
    public class PathHelperTest
    {
        const string ROOT_PATH = "C:\\Temp";
        const string PATH_RELATIVE = "folder";

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
