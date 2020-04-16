using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunkyCode.Blog.App.Core.Infrastructure.Email
{
    public interface IEmailService
    {
        Task SendContactEmail(string name, string email, string subject, string message);
    }
}
