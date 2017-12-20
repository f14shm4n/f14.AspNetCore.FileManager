using System;
using System.Collections.Generic;
using System.Text;

namespace f14.AspNetCore.FileManager.Abstractions
{
    public interface IParam
    {
        string CurrentFolderPath { get; }
    }
}
