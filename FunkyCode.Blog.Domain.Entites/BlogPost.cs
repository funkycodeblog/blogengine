using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;

namespace FunkyCode.Blog.Domain.Entites
{
    public class BlogPost
    {
        public string Id { get; set; }
        public DateTime PublishingDate { get; set; }
        public BlogStatusTypeEnum Status { get; set; }

        public string Title { get; set; }
        public string Header { get; set; }
        public string Content { get; set; }

        public virtual IList<BlogPostImage> Images { get; set; }

        public string Tags { get; set; }
    }

    public enum BlogStatusTypeEnum
    {
        Active,
        New,
        Page
    }
}
