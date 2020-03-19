using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FunkyCode.Blog.ScriptEngine.Contract;
using FunkyCode.Blog.Scripts;
using FunkyCode.Blog.App.Core.Infrastructure.Blog;
using CommandLine;

namespace FunkyCode.Blog.Scripts
{
    public class UploadBlogArticleCommand : IConsoleCommand<UploadBlogArticleCommand.Options>
    {
        private readonly IBlogPostUploadService _blogPostUploadService;

        [Verb("upload", HelpText = "Upload blog post to server")]
        public class Options : OptionsBase
        {
            [Option('p', "path", Required = true, HelpText = "Initial path")]
            public string Folder { get; set; }

            [Option('s', "subdirectories", Default = false, Required = false, HelpText = "Checks subdirectories for blog articles")]
            public bool IsSubdirectories { get; set; }

            [Option('o', "override", Default = false, Required = false, HelpText = "Overrides article when exists")]
            public bool IsOverrideWhenExists { get; set; }
        }

        public UploadBlogArticleCommand(IBlogPostUploadService blogPostUploadService)
        {
            _blogPostUploadService = blogPostUploadService;
        }

        public void Execute(Options options)
        {
            if (!options.IsSubdirectories)
            {
                _blogPostUploadService.Upload(options.Folder, options.IsOverrideWhenExists);
                return;
            }

            var subdirectories = Directory.GetDirectories(options.Folder);
            foreach (var subdir in subdirectories)
            {
                _blogPostUploadService.Upload(subdir, options.IsOverrideWhenExists);
            }
        }
    }
}
