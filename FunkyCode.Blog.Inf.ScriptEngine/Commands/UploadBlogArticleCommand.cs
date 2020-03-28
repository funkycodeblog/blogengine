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
    public class GitPreCommitCommand : IConsoleCommand<GitPreCommitCommand.Options>
    {
        private readonly IBlogPostUploadService _blogPostUploadService;

        [Verb("upload", HelpText = "Upload blog post(s) to server")]
        public class Options : OptionsBase
        {
            [Option('p', "path", Required = true, HelpText = "Initial path")]
            public string Folder { get; set; }

            [Option('h', "host", Required = false, Default= "https://localhost:5001", HelpText = "Host name")]
            public string Host { get; set; }

            [Option('s', "subdirectories", Default = false, Required = false, HelpText = "Checks subdirectories for blog articles")]
            public bool IsSubdirectories { get; set; }

            [Option('o', "override", Default = false, Required = false, HelpText = "Overrides article when exists")]
            public bool IsOverrideWhenExists { get; set; }
        }

        public GitPreCommitCommand(IBlogPostUploadService blogPostUploadService)
        {
            _blogPostUploadService = blogPostUploadService;
        }

        public void Execute(Options options)
        {
          
        }
    }
}
