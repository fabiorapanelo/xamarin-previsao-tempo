using System;
using System.Collections.Generic;
using System.Text;

namespace AppPrevisaoDoTempo
{
    public interface IFileHelper
    {
        string GetLocalFilePath(string filename);
    }
}
