using CommandLine;
using FunkyCode.Blog.App.Core;
using FunkyCode.Blog.ScriptEngine.Contract;
using LibGit2Sharp;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FunkyCode.Blog.Scripts
{
    public class GitPreCommitCommand : IConsoleCommand<GitPreCommitCommand.Options>
    {
        private readonly IBlogEngineLogger<GitPreCommitCommand> _logger;
        
        private static readonly string[] FileExtensions = new [] {".cs", ".tsx", ".ts"};
        private static readonly string TODO = "TODO:";

        [Verb("git-pre-commit", HelpText = "Handles git pre-commit hook")]
        public class Options : OptionsBase
        {
            [Option('d', "directory", Required = true, HelpText = "Working git directory")]
            public string WorkingDirectory { get; set; }
        }

        public GitPreCommitCommand(IBlogEngineLogger<GitPreCommitCommand> logger)
        {
            _logger = logger;
        }

        public int Execute(Options options)
        {
            var workingDirectory = options.WorkingDirectory;

            var gitPath = Path.Combine(options.WorkingDirectory, ".git");

            if (!Directory.Exists(gitPath))
            {
                _logger.Warning($"Directory {gitPath} doesn't exist!");
                return 1;
            }

            var repo = new Repository(gitPath);
            
            var currentBranch = repo.Branches.First(b => b.IsCurrentRepositoryHead);
            if (currentBranch.FriendlyName != "master" && currentBranch.FriendlyName != "test-react-hooks")
                return 0;

            var fileStatus = repo.RetrieveStatus();

            _logger.Info($"Checking '${TODO}' in files...");

            var filesToCheck = fileStatus
                .Where(f => f.State != FileStatus.Ignored)
                .Where(f => FileExtensions.Contains(Path.GetExtension(f.FilePath)))
                .Where(f => Path.GetFileNameWithoutExtension(f.FilePath) != nameof(GitPreCommitCommand))
                .ToList();
            
            var filesWithTodo = new List<string>();
            foreach (var file in filesToCheck)
            {
                var path = Path.Combine(workingDirectory, file.FilePath);
                _logger.Info($"Checking {path}...");
                var isOk = CheckIsNoToDoInFile(path);

                if (!isOk)
                    filesWithTodo.Add(path);
            }

            if (filesWithTodo.Any())
            {
                _logger.Error("There are files containing '${TODO}'");
                filesWithTodo.ForEach(_logger.Error);
                _logger.Error("Commit interrupted!");
                return 1;
            }

            _logger.Success("Ok!");
            return 0;
        }

        private static bool CheckIsNoToDoInFile(string filePath)
        {
            var extension = Path.GetExtension(filePath);
            if (!FileExtensions.Contains(extension)) return true;

            var lines = File.ReadAllLines(filePath);
            return lines.All(line => !line.Contains(TODO));
        }




    }
}
