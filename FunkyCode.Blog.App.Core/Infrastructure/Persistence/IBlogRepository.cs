using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FunkyCode.Blog.Domain.Entites;

namespace FunkyCode.Blog.App.Core
{
    public interface IBlogRepository
    {
        Task<List<BlogPostHeader>> GetHeaders();
        Task<bool> Add(BlogPost post);
        Task<byte[]> GetImage(string blogPostId, string imageId);
        Task<BlogPost> GetBlogPostWithNoImages(string blogPostId);
        Task<bool> DeleteBlogPost(string blogPostId);
    }
}
