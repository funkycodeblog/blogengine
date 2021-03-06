using System;
using System.Collections.Generic;

namespace FunkyCode.Blog.App.Core.Infrastructure.Internals
{
    public class BlogPostMetadata
    {
        public enum PostTypeEnum
        {
            Article,
            Page
        }

        public string Id { get; set; }
        public string Title { get; set; }
        public string Header { get; set; }
        public DateTime? PublishedDate { get; set; }
        public List<string> Categories { get; set; }
        public PostTypeEnum PostType { get; set; }
    }

    
}
