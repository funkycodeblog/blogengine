using System;
using System.Collections.Generic;
using System.Text;

namespace FunkyCode.Blog.App.Core.Infrastructure.Internals
{
    public class ArticleItemInfo
    {

        public string RawCode { get; set; }
        public string Id { get; set; }
        public string Description { get; set; }
        public ArticleItemTypeEnum ItemType { get; set; }
        public int Position { get; set; }
    }
}
