using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FunkyCode.Blog.App.Core.Infrastructure.Internals;
using FunkyCode.Blog.Domain.Entites;
using FunkyCode.Blog.Domain.Entites.Client;

namespace FunkyCode.Blog.App.Internals.Map
{
    public class BlogPostBlogPostHeaderDtoMapper : IMapper<BlogPostHeader, BlogPostHeaderDto>
    {
        private readonly ITagMapper _tagMapper;

        public BlogPostBlogPostHeaderDtoMapper(ITagMapper tagMapper)
        {
            _tagMapper = tagMapper;
        }

        public BlogPostHeaderDto Map(BlogPostHeader p)
        {
            return new BlogPostHeaderDto
            {
                Id = p.Id,
                Title = p.Title,
                Text = p.Header,
                Published = p.PublishingDate,
                Tags = _tagMapper.Map(p.Tags).ToList()
            };
        }

        public BlogPostHeader Map(BlogPostHeaderDto second)
        {
            throw new NotImplementedException();
        }

        public List<BlogPostHeaderDto> Map(List<BlogPostHeader> firstCollection) => firstCollection.Select(Map).ToList();

        public List<BlogPostHeader> Map(List<BlogPostHeaderDto> secondCollection)
        {
            throw new NotImplementedException();
        }
    }
}
