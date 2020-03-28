using System;
using System.Collections.Generic;
using System.Text;
using FunkyCode.Blog.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FunkyCode.Blog.Inf.EntityFramework.Configurations
{
    public class BlogPostConfiguration : IEntityTypeConfiguration<BlogPost>
    {
        public void Configure(EntityTypeBuilder<BlogPost> builder)
        {
            builder.HasKey(e => e.Id);
            // builder.Property(e => e.Avatar) <--- nvarchar (max)

            var converter = new EnumToStringConverter<BlogStatusTypeEnum>();
            builder.Property(e => e.Status)
                .HasConversion(converter);

            builder
                .HasMany(b => b.Images)
                .WithOne(i => i.BlogPost)
                .HasForeignKey(p => p.BlogPostId);
        }
    }
}
