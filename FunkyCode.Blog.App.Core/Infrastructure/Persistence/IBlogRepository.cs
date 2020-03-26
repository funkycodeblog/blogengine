using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FunkyCode.Blog.Domain.Entites;

namespace FunkyCode.Blog.App.Core
{
    public interface IBlogRepository
    {
        Task<List<BlogPostHeader>> GetHeaders();
        Task<List<BlogPostHeader>> GetHeaders(string tag);
        Task<List<BlogPostHeader>> SearchHeaders(string searchItem);
        Task<bool> Add(BlogPost post);
        Task<byte[]> GetImage(string blogPostId, string imageId);
        Task<BlogPost> GetBlogPostWithNoImages(string blogPostId);
        Task<bool> DeleteBlogPost(string blogPostId);
        Task<string[]> GetAllTags();
    }
}
