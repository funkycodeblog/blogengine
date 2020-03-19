namespace FunkyCode.Blog.Domain.Entites
{
    public class BlogPostImage
    {
        public string Id { get; set; }
        public byte[] Data { get; set; }

        public string BlogPostId { get; set; }
        public virtual BlogPost BlogPost { get; set; }
    }
}
