using System;
using System.Collections.Generic;
using System.Text;

namespace FunkyCode.Blog.App.Core.Infrastructure.Internals
{
    public interface IFileSystemService
    {
        string[] GetDirectories(string path);
        string[] GetFiles(string path, string searchPattern);

    }
}
