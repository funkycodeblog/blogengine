using System.Threading.Tasks;
using Autofac;


namespace FunkyCode.Blog
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IComponentContext context;

        public CommandDispatcher(IComponentContext context)
        {
            this.context = context;
        }

        public async Task Execute<TCommand>(TCommand command) where TCommand : ICommand
        {
            var handler = context.Resolve<ICommandHandler<TCommand>>();
            await handler.Execute(command);
        }
    }
}
