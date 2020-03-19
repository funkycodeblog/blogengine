using System;
using System.Collections.Generic;
using System.Text;
using FunkyCode.Blog.Domain.Entites.Client;

namespace FunkyCode.Blog.App.Core.Commands
{
    public class DeleteBlogPostCommand : ICommand
    {

        public string BlogPostId { get; set; }
        

    }


}
