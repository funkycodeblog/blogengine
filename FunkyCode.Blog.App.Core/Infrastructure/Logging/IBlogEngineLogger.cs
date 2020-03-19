using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Text;

namespace FunkyCode.Blog.App.Core
{
    public interface IBlogEngineLogger<T>
    {
        void Info(string info);
        void Exception(Exception exc);
        void Warning(string warning);
        void Error(string error);
        void Success(string message);
        void Object(string name, object obj);

    }
}
