using System;
using System.Collections.Generic;
using System.Text;

namespace FunkyCode.Blog.Domain.Entites.Client
{
    public class BlogPostHeaderDto
    {
        public string Id { get; set; }
        public DateTime Published { get; set; }
        public string Title { get; set; }
        public List<string> Tags { get; set; } = new List<string>();
        public string Text { get; set; }
    }

}
