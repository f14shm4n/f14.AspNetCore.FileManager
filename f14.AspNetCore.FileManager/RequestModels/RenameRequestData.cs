using f14.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace f14.AspNetCore.FileManager.RequestModels
{
    public class RenameRequestData : RequestData
    {
        public class RenameFileInfo
        {
            public string OldName { get; set; }
            public string NewName { get; set; }
            public bool IsFile { get; set; }
        }

        public List<RenameFileInfo> Renames { get; set; } = new List<RenameFileInfo>();
    }
}
