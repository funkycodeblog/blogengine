using FunkyCode.Blog.ScriptEngine.Contract;

namespace FunkyCode.Blog.Scripts
{
    public interface IConsoleCommand<in T> where T : OptionsBase
    {
        int Execute(T options);
    }
}
