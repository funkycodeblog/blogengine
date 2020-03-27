using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using FunkyCode.Blog.App.Core;
using FunkyCode.Blog.App.Core.Infrastructure.Blog;
using FunkyCode.Blog.App.Core.Infrastructure.Internals;

namespace FunkyCode.Blog.Inf
{
    public class BlogPostUploadService : IBlogPostUploadService
    {
        private readonly IBlogEngineLogger<BlogPostUploadService> _logger;
        private readonly IBlogPostMetadataResolver _blockBlogPostMetadataResolver;
        //private string _url = $@"https://localhost:5001/api/blog";

        public BlogPostUploadService(IBlogEngineLogger<BlogPostUploadService> logger, IBlogPostMetadataResolver blockBlogPostMetadataResolver)
        {
            _logger = logger;
            _blockBlogPostMetadataResolver = blockBlogPostMetadataResolver;
        }

        public void Upload(string host, string folderPath, bool isOverrideWhenExists)
        {
            _logger.Info($"Processing folder: {folderPath} ...");

            var url = $"{host}/api/blog";

            var markdownFiles = Directory.GetFiles(folderPath, "*.md")
                .ToList();

            if (markdownFiles.Count != 1)
            {
                _logger.Warning($"Folder should contain single markdown file!");
                _logger.Info($"Posting article cancelled.");
                return;
            }

            var imageFiles = Directory.GetFiles(folderPath, "*.png")
                .ToList();

            var files = markdownFiles.Union(imageFiles).ToList();

            using (var client = new HttpClient())
            {
                var markdownFile = markdownFiles.Single();
                var isMarkdownOk = CheckMarkdownFile(client, markdownFile, url, isOverrideWhenExists);
                if (!isMarkdownOk)
                {
                    _logger.Info($"Posting article cancelled.");
                    return;
                }

                using (var formData = new MultipartFormDataContent())
                {
                    foreach (var filePath in files)
                    {
                        var iFileName = Path.GetFileName(filePath);
                        _logger.Info($"   File: {iFileName}");

                        var iFileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                        var iStreamContent = CreateFileContent(iFileStream, iFileName);
                        formData.Add(iStreamContent);
                    }

                   

                    _logger.Info($"Posting blog to: {url} ...");
                    var response = client.PostAsync(url, formData).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        _logger.Success($"Posted!");
                        _logger.Info($"Status code: {response.StatusCode}");
                        _logger.Info(response.ReasonPhrase);

                    }
                    else
                    {
                        _logger.Error($"Status code: {response.StatusCode}");
                        _logger.Error(response.ReasonPhrase);
                    }
                 
                }
            }
        }
    

        private bool CheckMarkdownFile(HttpClient client, string filePath, string url, bool isOverrideWhenExists)
        {
            
            var file = File.ReadAllText(filePath);
            
            _logger.Info("Obtaining metadata...");
            var metadataProcessingResult = _blockBlogPostMetadataResolver.Resolve(file);
            var metadata = metadataProcessingResult.Result;
            
            _logger.Object("Article metadata", metadata);

            if (metadataProcessingResult.Status != ProcessingStatus.Ok)
            {
                _logger.Error("There are problems with blog article metadata");
                metadataProcessingResult.Messages.ForEach(m => _logger.Error(m));
                return false;
            }

            var id = metadata.Id;
            
            var checkUrl = $"{url}/CheckIfExists/{id}";
            _logger.Info(checkUrl);


            _logger.Info($"Checking article with Id {id} if exists on server...");
            
            var checkIfExistsResponse = client.GetAsync(checkUrl).Result;

            if (checkIfExistsResponse.StatusCode == HttpStatusCode.OK)
            {
                _logger.Warning($"Article with Id {id} already exists!");
                if (!isOverrideWhenExists)
                {
                    return false;
                }
            }

            return true;

        }

        private StreamContent CreateFileContent(Stream image, string fileName)
        {
            var fileStreamContent = new StreamContent(image);
            fileStreamContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data") { Name = "file", FileName = fileName };
            fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            return fileStreamContent;
        }


    }
}
