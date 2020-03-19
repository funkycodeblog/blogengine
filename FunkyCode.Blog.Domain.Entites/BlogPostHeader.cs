using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;

namespace FunkyCode.Blog.Domain.Entites
{
    public class BlogPostHeader
    {
        public string Id { get; set; }
        public DateTime PublishingDate { get; set; }
        public string Title { get; set; }
        public BlogStatusTypeEnum Status { get; set; }
        public string Header { get; set; }
    }
}
