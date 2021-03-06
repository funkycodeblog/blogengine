using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FunkyCode.Blog.App.Core.Infrastructure.Email;
using FunkyCode.Blog.App.Core.Infrastructure.Internals;
using FunkyCode.Blog.App.Core.Infrastructure.Persistence;
using FunkyCode.Blog.App.Internals.Map;
using FunkyCode.Blog.Domain.Entites;
using FunkyCode.Blog.Domain.Entites.Client;


namespace FunkyCode.Blog.App.Core.Commands
{
    public class CommandsHandler : 
        ICommandHandler<UploadBlogPostCommand>,
        ICommandHandler<DeleteBlogPostCommand>,
        ICommandHandler<SendContactMessageCommand>,
        ICommandHandler<ProcessSubscriptionCommand>


    {
        private readonly IBlogRepository _blogRepository;
        private readonly IBlogPostMetadataResolver _postMetadataResolver;
        private readonly ITagMapper _tagMapper;
        private readonly IEmailService _emailService;
        private readonly IUserRepository _userRepository;


        public CommandsHandler(IBlogRepository blogRepository, IBlogPostMetadataResolver postMetadataResolver, ITagMapper tagMapper, IEmailService emailService, IUserRepository userRepository)
        {
            _blogRepository = blogRepository;
            _postMetadataResolver = postMetadataResolver;
            _tagMapper = tagMapper;
            _emailService = emailService;
            _userRepository = userRepository;
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
            var tags = _tagMapper.Map(metadata.Categories?.ToArray());

            var blogPost = new BlogPost
            {
                Id = postId,
                Title = metadata.Title,
                Content = postFileContent,
                Header = metadata.Header,
                PublishingDate = date,
                Status = (metadata.PostType == BlogPostMetadata.PostTypeEnum.Article) ? 
                    BlogStatusTypeEnum.Active :
                    BlogStatusTypeEnum.Page, 
                Images = new List<BlogPostImage>(),
                Tags = tags
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

        public async Task Execute(SendContactMessageCommand command)
        {
            var msg = command.ContactMessage;
            await _emailService.SendContactEmail(msg.Username, msg.Email, msg.Subject, msg.Message);
        }

        public async Task Execute(ProcessSubscriptionCommand command)
        {
            if (command.SubscriptionData.Action == SubscribeDataActionTypeEnum.Subscribe)
            {
                await _userRepository.Subscribe(command.SubscriptionData);
            }
            else
            {
                await _userRepository.Unsubscribe(command.SubscriptionData);
            }
        }
    }
}
