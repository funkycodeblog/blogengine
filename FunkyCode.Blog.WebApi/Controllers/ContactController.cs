using System.Threading.Tasks;
using FunkyCode.Blog.App;
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
        private readonly IQueryProcessor _queryProcessor;

        public ContactController(ICommandDispatcher commandDispatcher, IQueryProcessor queryProcessor)
        {
            _commandDispatcher = commandDispatcher;
            _queryProcessor = queryProcessor;
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

        [HttpPost("Subscribe")]
        public async Task<ActionResult<SubscriptionResult>> Subscribe(SubscribeDto subscribeDto)
        {
            var action = subscribeDto.Action;

            var checkIfSubscriberExistQuery = new CheckIfUserSubscribedQuery
            {
                SubscriberEmail = subscribeDto.Email
            };

            var isSubscriber =
                await _queryProcessor.Process<CheckIfUserSubscribedQuery, bool>(checkIfSubscriberExistQuery);

            if (isSubscriber && action == SubscribeDataActionTypeEnum.Subscribe)
                return Ok(SubscriptionResult.AlreadySubscribed);

            if (!isSubscriber && action == SubscribeDataActionTypeEnum.Unsubscribe)
                return Ok(SubscriptionResult.NotInDatabase);

            var command = new SendContactMessageCommand { ContactMessage = null };
            await _commandDispatcher.Execute(command);

            if (action == SubscribeDataActionTypeEnum.Subscribe)
                return Ok(SubscriptionResult.Subscribed);

            if (action == SubscribeDataActionTypeEnum.Unsubscribe)
                return Ok(SubscriptionResult.Unsubscribed);

            return BadRequest();
        }
    }
}