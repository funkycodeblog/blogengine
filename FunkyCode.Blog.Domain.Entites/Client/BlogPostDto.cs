using System;
using System.Collections.Generic;
using System.Text;

namespace FunkyCode.Blog.Domain.Entites.Client
{
    public class BlogPostDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Content { get; set; }
    }
}
