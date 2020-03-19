using System;
using FunkyCode.Blog.App;
using FunkyCode.Blog.Inf.IoC.Modules;

namespace FunkyCode.Blog.Inf.IoC
{
    public class InternalsModule : AllAssemblyTypesModule<GetBlogPostQuery>
    {
    }
}
