using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace f14.AspNetCore.FileManager.RequestModels
{
    public class DeleteRequestData : RequestData
    {
        public List<string> ObjectNames { get; set; } = new List<string>();        
    }
}
