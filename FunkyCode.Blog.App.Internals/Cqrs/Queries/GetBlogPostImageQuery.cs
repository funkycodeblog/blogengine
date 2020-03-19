namespace FunkyCode.Blog.App
{
    public class GetBlogPostImageQuery : IQuery
    {
        public string PostId { get; set; }
        public string ImageId { get; set; }

    }
}
