using System;
using System.Collections.Generic;
using System.Text;

namespace FunkyCode.Blog.App.Core.Infrastructure.Internals
{
    public interface IImageUrlResolver
    {
        string GetWithImageUrlResolved(string content, string articleId);
    }
}
