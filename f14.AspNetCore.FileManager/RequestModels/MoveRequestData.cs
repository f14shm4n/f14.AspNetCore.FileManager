using f14.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace f14.AspNetCore.FileManager.RequestModels
{
    public class MoveRequestData : RequestData
    {
        public class MoveTarget
        {
            public string Name { get; set; }
            public bool IsFile { get; set; }
        }

        public string SourceDirectory { get; set; }
        public string DestinationDirectory { get; set; }
        public bool Overwrite { get; set; } = false;
        public List<MoveTarget> Targets { get; set; } = new List<MoveTarget>();
    }
}
