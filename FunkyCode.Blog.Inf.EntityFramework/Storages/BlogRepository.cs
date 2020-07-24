using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FunkyCode.Blog.App.Core;
using FunkyCode.Blog.Domain.Entites;
using FunkyCode.Blog.Inf.EntityFramework.Context;
using FunkyCode.Blog.Inf.EntityFramework.Tools;
using Microsoft.EntityFrameworkCore;

namespace FunkyCode.Blog.Inf.EntityFramework.Storages
{
    public class BlogRepository : IBlogRepository
    {

        private readonly DbContextOptions<BlogContext> _options;

      

        public BlogRepository(DbContextOptions<BlogContext> options)
        {
            _options = options;
        }

        public async Task<List<BlogPostHeader>> GetHeaders()
        {
            using (var context = new BlogContext(_options))
            {
                var headers = await context.BlogPosts
                    .Where(p => p.Status == BlogStatusTypeEnum.Active)
                    .Select(p => new BlogPostHeader
                {
                    Id = p.Id,
                    Header = p.Header,
                    PublishingDate = p.PublishingDate,
                    Status = p.Status,
                    Title = p.Title,
                    Tags = p.Tags
                }).ToListAsync();

                return headers;
            }
        }

        public async Task<List<BlogPostHeader>> GetHeaders(string tag)
        {
            using (var context = new BlogContext(_options))
            {
                var tag1 = $"{tag};";
                var tag2 = $";{tag};";
                var tag3 = $";{tag}";

                var headers = await context.BlogPosts
                    .Where(p => p.Status == BlogStatusTypeEnum.Active)
                    .Where(p => p.Tags.StartsWith(tag1) || p.Tags.Contains(tag2) || p.Tags.EndsWith(tag3) || p.Tags == tag)
                    .Select(p => new BlogPostHeader
                {
                    Id = p.Id,
                    Header = p.Header,
                    PublishingDate = p.PublishingDate,
                    Status = p.Status,
                    Title = p.Title,
                    Tags = p.Tags
                }).ToListAsync();

                return headers;
            }
        }

        public async Task<List<BlogPostHeader>> SearchHeaders(string searchItem)
        {

            var tag1 = $"{searchItem};";
            var tag2 = $";{searchItem};";
            var tag3 = $";{searchItem}";

            using (var context = new BlogContext(_options))
            {
                var headers = await context.BlogPosts
                    .Where(p => p.Status == BlogStatusTypeEnum.Active)
                    .Where(p => p.Tags.StartsWith(tag1) || p.Tags.Contains(tag2) || p.Tags.EndsWith(tag3) || p.Tags == searchItem  || p.Title.Contains(searchItem) || p.Content.Contains(searchItem))
                    .Select(p => new BlogPostHeader
                    {
                        Id = p.Id,
                        Header = p.Header,
                        PublishingDate = p.PublishingDate,
                        Status = p.Status,
                        Title = p.Title,
                        Tags = p.Tags
                    }).ToListAsync();

                return headers;
            }
        }

        public async Task<bool> Add(BlogPost post)
        {
            using (var context = new BlogContext(_options))
            {
                await context.BlogPosts.AddAsync(post);
                await context.SaveChangesAsync();
            }

            return true;
        }

        public async Task<byte[]> GetImage(string blogPostId, string imageId)
        {
            using (var context = new BlogContext(_options))
            {
                var image = await
                    context.BlogPostImages
                        .Where(i => i.BlogPostId == blogPostId && i.Id == imageId)
                        .Select(i => i.Data)
                        .FirstOrDefaultAsync();

                return image;
            }
        }

        public async Task<BlogPost> GetBlogPostWithNoImages(string blogPostId)
        {
            using (var context = new BlogContext(_options))
            {
                var blogPost = await
                    context.BlogPosts.FindAsync(blogPostId);

                return blogPost;
            }
        }

        public async Task<bool> DeleteBlogPost(string blogPostId)
        {
            using (var context = new BlogContext(_options))
            {
                var blogPost = await
                    context.BlogPosts.FindAsync(blogPostId);

                context.BlogPosts.Remove(blogPost);

                await context.SaveChangesAsync();

                return true;
            }
        }

        public async Task<string[]> GetAllTags()
        {
            using (var context = new BlogContext(_options))
            {
                var tags = await
                    context.BlogPosts.Select(b => b.Tags).ToArrayAsync();

                return tags;
            }
        }

        public async Task<List<HealthCheckItem>> PerformHealthCheck()
        {
            using (var db = new BlogContext(_options))
            {
                var result = await EntityIntegrityTestTool.Test(db);
                return result;
            }
        }

    }
}
