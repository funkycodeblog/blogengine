using FunkyCode.Blog.App.Core.Infrastructure.Internals;
using System.Collections.Generic;

namespace FunkyCode.Blog.App.Internals.Tools
{
    public interface IArticleItemInfoBuilder
    {
        string BuildItemDescription(ArticleItemInfo info);

        List<string> GetReferences(string line);

        string UpdateReferenceInLine(string line, ArticleItemInfo info, string reference);
    }
}