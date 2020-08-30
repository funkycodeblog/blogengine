namespace FunkyCode.Blog.App.Internals
{
    public interface IStringBuilder<T>
    {
        string Build(T input);
    }
}
