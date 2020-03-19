using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FunkyCode.Blog.App.Core;
using FunkyCode.Blog.Domain.Entites;
using FunkyCode.Blog.Inf.EntityFramework.Context;
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
                var headers = await context.BlogPosts.Select(p => new BlogPostHeader
                {
                    Id = p.Id,
                    Header = p.Header,
                    PublishingDate = p.PublishingDate,
                    Status = p.Status,
                    Title = p.Title
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
    }
}
