using System;
using System.Collections.Generic;
using System.Text;
using FunkyCode.Blog.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FunkyCode.Blog.Inf.EntityFramework.Configurations
{
    public class BlogPostImageConfiguration : IEntityTypeConfiguration<BlogPostImage>
    {
        public void Configure(EntityTypeBuilder<BlogPostImage> builder)
        {
            builder.HasKey(e => new {e.BlogPostId, e.Id});
        }
    }
}
