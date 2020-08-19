namespace FunkyCode.Blog.Domain.Entites.Client
{
    public class SubscribeDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public SubscribeDataActionTypeEnum Action { get; set; }
    }

}