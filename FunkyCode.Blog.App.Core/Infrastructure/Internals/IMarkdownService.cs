using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunkyCode.Blog.App
{
    public interface IMarkdownService
    {
        Task<string> ConvertToHtml(string markdownContent);
    }
}
