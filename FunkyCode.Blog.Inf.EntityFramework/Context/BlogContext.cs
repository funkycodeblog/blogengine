using System.Reflection;
using FunkyCode.Blog.Domain.Entites;
using Microsoft.EntityFrameworkCore;

namespace FunkyCode.Blog.Inf.EntityFramework.Context
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {
            base.Database.SetCommandTimeout(120);
            base.Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(BlogContext))); ;
        }

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<BlogPostImage> BlogPostImages { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }

    }
}
