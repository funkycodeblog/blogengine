using System;
using System.Collections.Generic;
using System.Text;
using FunkyCode.Blog.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FunkyCode.Blog.Inf.EntityFramework
{
    public class EntityBaseConfiguration<T> where T : Entity
    {
        public void ConfigureEntity(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(e => e.Id);
        }
    }
}
