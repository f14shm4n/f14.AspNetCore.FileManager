using System;
using System.Collections.Generic;
using System.Text;

namespace f14.AspNetCore.FileManager.Abstractions
{
    public interface IResult
    {
        List<string> Errors { get; }
    }
}
