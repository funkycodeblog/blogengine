using FunkyCode.Blog.Inf.IoC;
using FunkyCode.Blog.Inf.IoC.Modules;

using Autofac;

namespace FunkyCode.Blog.Inf.WebApi.IoC
{
    public class Module : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new InternalsModule());
            builder.RegisterModule(new EntityFrameworkModule());
            builder.RegisterModule(new EntitiesModule());
            builder.RegisterModule(new CqrsModule());
         
            builder.RegisterModule(new MarkdownServiceModule());
            builder.RegisterModule(new SendGridEmailServiceModule());

            builder.RegisterType<BlogEngineConfiguration>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}