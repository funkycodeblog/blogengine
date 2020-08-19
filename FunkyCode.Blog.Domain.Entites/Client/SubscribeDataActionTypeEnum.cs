namespace FunkyCode.Blog.Domain.Entites.Client
{
    public enum SubscribeDataActionTypeEnum
    {
        Unknown,
        Subscribe,
        Unsubscribe
    }

    public enum SubscriptionResult
    {
        Unknown,
        Subscribed,
        AlreadySubscribed,
        Unsubscribed,
        NotInDatabase
    }
}