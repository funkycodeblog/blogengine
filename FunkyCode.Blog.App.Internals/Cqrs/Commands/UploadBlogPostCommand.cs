using System;
using System.Collections.Generic;
using System.Text;
using FunkyCode.Blog.Domain.Entites.Client;

namespace FunkyCode.Blog.App.Core.Commands
{
    public class UploadBlogPostCommand : ICommand
    {

        public class File
        {
            public string FileName { get; set; }
            public byte[] Data { get; set; }
        }

        public List<File> Files { get; set; } = new List<File>();

    }


}
