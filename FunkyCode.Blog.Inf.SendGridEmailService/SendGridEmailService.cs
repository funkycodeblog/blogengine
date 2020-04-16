using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FunkyCode.Blog.App.Core.Infrastructure.Email;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace FunkyCode.Blog.Inf.SendGridEmailService
{
    public class SendGridEmailService : IEmailService
    {
        private readonly IBlogEngineConfiguration _configuration;
        private readonly SendGridClient sendGridClient;

        public SendGridEmailService(IBlogEngineConfiguration configuration)
        {
            _configuration = configuration;
            sendGridClient = new SendGridClient(configuration.SendGridApiKey);
        }

        public async Task SendContactEmail(string name, string email, string subject, string message)
        {
            try
            {
                var msg = new SendGridMessage();

                msg.SetFrom(email);
                var recipients = new List<EmailAddress>
                {
                    new EmailAddress("maciek.szczudlo@gmail.com", "Maciej Szczudlo")
                };
                msg.AddTos(recipients);
                msg.SetSubject($"[funkycode.pl] {subject}");
                msg.AddContent(MimeType.Text, message);

                var response = await sendGridClient.SendEmailAsync(msg);
            }
            catch (Exception e)
            {
                // Must be Ignored. 
            }
        }
    }

}
