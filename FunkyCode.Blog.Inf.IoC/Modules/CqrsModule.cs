using System;
using System.Collections.Generic;
using System.Text;
using Autofac;

namespace FunkyCode.Blog.Inf.IoC.Modules
{
    public class CqrsModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<QueryProcessor>().AsImplementedInterfaces();
            builder.RegisterType<CommandDispatcher>().AsImplementedInterfaces();
        }
    }
}
