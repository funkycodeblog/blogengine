using System.Threading.Tasks;
using FunkyCode.Blog.App.Core.Commands;
using FunkyCode.Blog.Domain.Entites.Client;
using Microsoft.AspNetCore.Mvc;

namespace FunkyCode.Blog.Inf.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public ContactController(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        /// <summary>
        ///     Upload blog article
        /// </summary>
        [HttpPost]
        public async Task Post(ContactMessageDto contactMessage)
        {
            var command = new SendContactMessageCommand {ContactMessage = contactMessage};
            await _commandDispatcher.Execute(command);
        }
    }
}