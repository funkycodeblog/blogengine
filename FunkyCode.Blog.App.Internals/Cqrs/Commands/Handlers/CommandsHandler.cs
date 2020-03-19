using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FunkyCode.Blog.App.Core.Infrastructure.Internals;

using FunkyCode.Blog.Domain.Entites;


namespace FunkyCode.Blog.App.Core.Commands
{
    public class CommandsHandler : 
        ICommandHandler<UploadBlogPostCommand>,
        ICommandHandler<DeleteBlogPostCommand>

    {
        private readonly IBlogRepository _blogRepository;
        private readonly IBlogPostMetadataResolver _postMetadataResolver;

        public CommandsHandler(IBlogRepository blogRepository, IBlogPostMetadataResolver postMetadataResolver)
        {
            _blogRepository = blogRepository;
            _postMetadataResolver = postMetadataResolver;
        }


        public async Task Execute(UploadBlogPostCommand command)
        {

            var postFile = command.Files.FirstOrDefault(f => f.FileName.EndsWith(".md"));
            var imageFiles = command.Files.Where(f => !f.FileName.EndsWith(".md"));

            var postFileContent = Encoding.UTF8.GetString(postFile.Data, 0, postFile.Data.Length);
            var processingMetadataResult = _postMetadataResolver.Resolve(postFileContent);
            var metadata = processingMetadataResult.Result;

            var date = metadata.PublishedDate ?? DateTime.Now;
            var postId = metadata.Id;

            var blogPost = new BlogPost
            {
                Id = postId,
                Title = metadata.Title,
                Content = postFileContent,
                Header = metadata.Header,
                PublishingDate = date,
                Status = BlogStatusTypeEnum.Active,
                Images = new List<BlogPostImage>()
            };

            foreach (var imageFile in imageFiles)
            {
                var fileName = Path.GetFileNameWithoutExtension(imageFile.FileName);
                var blogPostImage = new BlogPostImage
                {
                    Id = fileName,
                    Data = imageFile.Data
                };

                blogPost.Images.Add(blogPostImage);
            }

            var existingPost = await _blogRepository.GetBlogPostWithNoImages(postId);
            var isPostExists = existingPost != null;
            if (isPostExists)
            {
                await _blogRepository.DeleteBlogPost(postId);
            }

            await _blogRepository.Add(blogPost);

        }

        public async Task Execute(DeleteBlogPostCommand command)
        {
            await _blogRepository.DeleteBlogPost(command.BlogPostId);
        }
    }
}
