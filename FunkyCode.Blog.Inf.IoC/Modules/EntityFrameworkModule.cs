using FunkyCode.Blog.Inf.EntityFramework.Context;
using FunkyCode.Blog.Inf.IoC.Modules;

namespace FunkyCode.Blog.Inf.IoC
{
    public class EntityFrameworkModule : AllAssemblyTypesModule<BlogContext>
    {
    }
}
