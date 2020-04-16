using System;
using System.Collections.Generic;
using System.Text;
using FunkyCode.Blog.Domain.Entites.Client;

namespace FunkyCode.Blog.App.Core.Commands
{
    public class SendContactMessageCommand : ICommand
    {
        public ContactMessageDto ContactMessage { get; set; }
    }
}
