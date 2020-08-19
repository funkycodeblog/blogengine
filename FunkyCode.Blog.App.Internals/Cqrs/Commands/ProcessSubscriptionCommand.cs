using FunkyCode.Blog.Domain.Entites.Client;

namespace FunkyCode.Blog.App.Core.Commands
{
    public class ProcessSubscriptionCommand : ICommand
    {
        public SubscribeDto SubscriptionData{ get; set; }
    }
}