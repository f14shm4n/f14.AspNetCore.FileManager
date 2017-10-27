using System;
using System.Collections.Generic;
using System.Text;

namespace f14.AspNetCore.FileManager.ResponseModels
{
    public class StructResponseData : ResponseData
    {
        public int FolderCount { get; set; }
        public int FileCount { get; set; }
        public IEnumerable<FileInfoProxy> Folders { get; set; }
        public IEnumerable<FileInfoProxy> Files { get; set; }
    }
}
