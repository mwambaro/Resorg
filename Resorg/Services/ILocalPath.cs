using System;
using System.Collections.Generic;
using System.Text;

namespace Resorg.Services
{
    public interface ILocalPath
    {
        string DatabasePath(string file);
    }
}
