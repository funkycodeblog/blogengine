using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using FunkyCode.Blog.App;
using FunkyCode.Blog.App.Core.Commands;
using FunkyCode.Blog.Domain;
using FunkyCode.Blog.Domain.Entites.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FunkyCode.Blog.Inf.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IQueryProcessor _queryProcessor;
        private readonly ICommandDispatcher _commandDispatcher;

        public BlogController(
            IQueryProcessor queryProcessor,
            ICommandDispatcher commandDispatcher
        )
        {
            _queryProcessor = queryProcessor;
            _commandDispatcher = commandDispatcher;
        }

        /// <summary>
        ///     Returns all blog article headers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<BlogPostHeaderDto>>> GetAllHaders()
        {
            var headers =
                await _queryProcessor.Process<GetBlogPostHeadersQuery, List<BlogPostHeaderDto>>(
                    new GetBlogPostHeadersQuery());
            return Ok(headers);
        }

        /// <summary>
        ///     Returns blog article headers by tag
        /// </summary>
        /// <returns></returns>
        [HttpGet("Tag/{tag}")]
        public async Task<ActionResult<List<BlogPostHeaderDto>>> GetHeadersByTag(string tag)
        {
            var headers =
                await _queryProcessor.Process<GetBlogPostHeadersByTagQuery, List<BlogPostHeaderDto>>(
                    new GetBlogPostHeadersByTagQuery{Tag = tag});
            return Ok(headers);
        }



        /// <summary>
        ///     Returns blog article by id.
        /// </summary>
        /// <returns></returns>
        [HttpGet("{blogPostId}")]
        public async Task<ActionResult<BlogPostDto>> Get(string blogPostId)
        {
            var post = await _queryProcessor.Process<GetBlogPostQuery, BlogPostDto>(new GetBlogPostQuery
                {Id = blogPostId});
            return Ok(post);
        }

        /// <summary>
        ///     Check if blog article exists.
        /// </summary>
        /// <returns></returns>
        [HttpGet("CheckIfExists/{blogPostId}")]
        public async Task<ActionResult<BlogPostDto>> CheckIfExists(string blogPostId)
        {
            var isExists =
                await _queryProcessor.Process<CheckIfExistsQuery, bool>(new CheckIfExistsQuery {Id = blogPostId});
            if (!isExists) return NoContent();
            return Ok();
        }

        /// <summary>
        ///     Get all tags.
        /// </summary>
        /// <returns></returns>
        [HttpGet("Tags")]
        public async Task<ActionResult<BlogPostDto>> GetTags()
        {
            var tags = await _queryProcessor.Process<GetAllTagsQuery, string[]>(new GetAllTagsQuery());
            return Ok(tags);
        }

        /// <summary>
        ///     Get archive.
        /// </summary>
        /// <returns></returns>
        [HttpGet("Archives")]
        public async Task<ActionResult<List<ArchiveYearDto>>> GetArchives()
        {
            var tags = await _queryProcessor.Process<GetArchiveQuery, List<ArchiveYearDto>>(new GetArchiveQuery());
            return Ok(tags);
        }



        /// <summary>
        ///     Get image from blog article.
        /// </summary>
        /// <returns></returns>
        [HttpGet("{blogPostId}/{imageId}")]
        public async Task<IActionResult> Get(string blogPostId, string imageId)
        {
            var imageAsBytes = await _queryProcessor.Process<GetBlogPostImageQuery, byte[]>(new GetBlogPostImageQuery
                {PostId = blogPostId, ImageId = imageId});
            return File(imageAsBytes, "image/jpg");
        }

        /// <summary>
        ///     Upload blog article
        /// </summary>
        [HttpPost]
        public async Task Post(IFormFileCollection file)
        {
            var command = new UploadBlogPostCommand();

            foreach (var ifile in file)
                using (var br = new BinaryReader(ifile.OpenReadStream()))
                {
                    var bytes = br.ReadBytes((int) ifile.Length);
                    command.Files.Add(new UploadBlogPostCommand.File {FileName = ifile.FileName, Data = bytes});
                }

            await _commandDispatcher.Execute(command);
        }

        /// <summary>
        ///     Deletes blog article
        /// </summary>
        /// <param name="blogPostId"></param>
        /// <returns></returns>
        [HttpDelete("{blogPostId}")]
        public async Task<IActionResult> Delete(string blogPostId)
        {
            await _commandDispatcher.Execute(new DeleteBlogPostCommand {BlogPostId = blogPostId});
            return Ok();
        }
    }
}