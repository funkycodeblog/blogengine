using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using FunkyCode.Blog.App.Core;
using FunkyCode.Blog.App.Core.Infrastructure.Internals;
using FunkyCode.Blog.App.Internals.Map;
using FunkyCode.Blog.Domain.Entites;
using FunkyCode.Blog.Domain.Entites.Client;

namespace FunkyCode.Blog.App
{
    public class GetBlogPostQueryHandler :  
        IQueryHandler<GetBlogPostQuery, BlogPostDto>,
        IQueryHandler<GetBlogPostImageQuery, byte[]>,
        IQueryHandler<GetBlogPostHeadersQuery, List<BlogPostHeaderDto>>,
        IQueryHandler<CheckIfExistsQuery, bool>

    {
        private readonly IMarkdownService _markdownService;
        private readonly IBlogRepository _blogRepository;
        private readonly ITagMapper _tagMapper;

        public GetBlogPostQueryHandler(IMarkdownService markdownService, IBlogRepository blogRepository, ITagMapper tagMapper)
        {
            _markdownService = markdownService;
            _blogRepository = blogRepository;
            _tagMapper = tagMapper;
        }

        public async Task<BlogPostDto> Handle(GetBlogPostQuery query)
        {
            var postId = query.Id;

            var blogPost = await _blogRepository.GetBlogPostWithNoImages(postId);

            var postAsHtml = await _markdownService.ConvertToHtml(blogPost.Content);
            
            for (var i = 0; i < 10; i++)
            {
                var number = $"{i:00}";
                var src = $"{number}.png";
                var dest = $"https://localhost:5001/api/blog/{postId}/{number}";

                postAsHtml = postAsHtml.Replace(src, dest);
            }

            return new BlogPostDto
            {
                Id = postId,
                Date = DateTime.Now,
                Title = "Test title",
                Content = postAsHtml,
                Tags = _tagMapper.Map(blogPost.Tags).ToList()
            };

        }
        
        public async Task<byte[]> Handle(GetBlogPostImageQuery query)
        {
            var postId = query.PostId;
            var imageId = query.ImageId;

            var imageAsBytes = await _blogRepository.GetImage(postId, imageId);
            return imageAsBytes;
        }

        public async Task<List<BlogPostHeaderDto>> Handle(GetBlogPostHeadersQuery query)
        {
            var blockPosts = await _blogRepository.GetHeaders();

            var dtos = blockPosts.Select(p => new BlogPostHeaderDto
            {
                Id = p.Id,
                Title = p.Title,
                Text = p.Header,
                Published = p.PublishingDate,
                Tags = _tagMapper.Map(p.Tags).ToList()
            }).ToList();

            return dtos;
        }

        public async Task<bool> Handle(CheckIfExistsQuery query)
        {
            var blogPost = await _blogRepository.GetBlogPostWithNoImages(query.Id);
            return blogPost != null;
        }
    }
}
