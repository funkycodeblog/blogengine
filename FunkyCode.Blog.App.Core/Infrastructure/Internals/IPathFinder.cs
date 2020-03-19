using System;
using System.Collections.Generic;
using System.Text;

namespace FunkyCode.Blog
{
    public interface IPathFinder
    {
        string FindPath(string root, string name);
    }
}
