using System;
using System.Collections.Generic;
using System.Text;

namespace f14.AspNetCore.FileManager.ResponseModels
{
    public class ResponseData
    {
        public int Affected { get; set; }
        public IEnumerable<string> Errors { get; set; } = new List<string>();
    }
}
