using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Autofac;
using Module = Autofac.Module;

namespace FunkyCode.Blog.Inf.IoC.Modules
{
    public class AllAssemblyTypesModule<TTypeFromAssemblyToRegister> : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = Assembly.GetAssembly(typeof(TTypeFromAssemblyToRegister));
            builder.RegisterAssemblyTypes(assembly)
                .AsImplementedInterfaces();
        }

    }
}
