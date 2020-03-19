using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FunkyCode.Blog.Inf.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace FunkyCode.Blog.Inf.WebApi.ContextFactory
{
    public class BlogContextFactory :  IDesignTimeDbContextFactory<BlogContext>
    {
        public BlogContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<BlogContext>();

            var connectionString = configuration.GetConnectionString("defaultConnection");

            builder.UseSqlServer(connectionString);
                
            return new BlogContext(builder.Options);
        }
    }
}
