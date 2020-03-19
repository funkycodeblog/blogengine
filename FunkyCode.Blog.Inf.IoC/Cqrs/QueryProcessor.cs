using System.Threading.Tasks;
using Autofac;


namespace FunkyCode.Blog
{
    public class QueryProcessor : IQueryProcessor
    {
        private readonly IComponentContext context;

        public QueryProcessor(IComponentContext context)
        {
            this.context = context;
        }

        public async Task<TResult> Process<TQuery, TResult>(TQuery query) where TQuery : IQuery
        {
            var handler = context.Resolve<IQueryHandler<TQuery, TResult>>();
            var result = await handler.Handle(query);
            return result;
        }
    }
}

