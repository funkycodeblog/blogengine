using System;
using System.Collections.Generic;
using System.Text;

namespace FunkyCode.Blog.Domain.Entites.Client
{
    public class ContactMessageDto
    {
        public string Username { get; set; }
        public string Email    { get; set; }
        public string Subject  { get; set; }
        public string Message  { get; set; }
    }
}
