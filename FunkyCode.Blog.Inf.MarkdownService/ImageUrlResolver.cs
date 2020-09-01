using FunkyCode.Blog.App.Core.Infrastructure.Internals;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunkyCode.Blog.Inf.MarkdownService
{
    public class ImageUrlResolver : IImageUrlResolver
    {
        public string GetWithImageUrlResolved(string content, string articleId)
        { 

            for (var i = 0; i < 20; i++)
            {
                var number = $"{i:00}";
                var src = $"{number}.png";
                var dest = $"/api/blog/{articleId}/{number}";

                content = content.Replace(src, dest);
            }

            return content;
        }
    }
}
