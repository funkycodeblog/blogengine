using System.Collections.Generic;

namespace FunkyCode.Blog.Domain
{
    public class ArchiveArticleDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public List<string> Tags { get; set; }
    }
}