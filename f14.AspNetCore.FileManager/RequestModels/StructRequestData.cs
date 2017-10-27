using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace f14.AspNetCore.FileManager.RequestModels
{
    public class StructRequestData : RequestData
    {        
        public string[] FileExtensions { get; set; } = new string[0];        
    }
}
