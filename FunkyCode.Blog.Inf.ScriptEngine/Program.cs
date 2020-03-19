using System;
using System.Data;
using System.Net.Sockets;
using System.Reflection;
using FunkyCode.Blog.ScriptEngine.Contract;
using FunkyCode.Blog.ScriptEngine.Tools;
using FunkyCode.Blog.Scripts;
using FunkyCode.Blog.App.Core;
using FunkyCode.Blog.App.Core.Infrastructure.Blog;
using FunkyCode.Blog.App.Core.Infrastructure.Internals;
using FunkyCode.Blog.App.Internals.Tools;
using FunkyCode.Blog.Inf;
using Autofac;
using CommandLine;

namespace FunkyCode.Blog.Scripts
{
    internal class Program
    {

        //public static int Main(string[] args)
        //{
        //    using (var container = ConfigureContainer())
        //    {
        //        using (var scope = container.BeginLifetimeScope())
        //        {

        //            var command = scope.Resolve<IConsoleCommand<UploadBlogArticleCommand.Options>>();

        //            var options = new UploadBlogArticleCommand.Options
        //            {
        //                Folder = @"c:\Projects\Tools\react-blog-engine\FunkyCode.Blog.ScriptEngine\_Articles",
        //                IsSubdirectories = true
        //            };

        //            command.Execute(options);
        //        }
        //    }

        //    return 0;
        //}

        public static int Main(string[] args)
        {
            return Parser.Default.ParseArguments<UploadBlogArticleCommand.Options>(args)
                .MapResult(
                    (UploadBlogArticleCommand.Options opts) => DoExecute(opts),
                    errs => 1);
        }

        static int DoExecute<TOptions>(TOptions options) where TOptions : OptionsBase
        {

            try
            {
                using (var container = ConfigureContainer())
                {
                    using (var scope = container.BeginLifetimeScope())
                    {
                        var command = scope.Resolve<IConsoleCommand<TOptions>>();
                        command.Execute(options);
                    }
                }

            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }

            return 0;
        }

        static IContainer ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            var assembly = Assembly.GetAssembly(typeof(BlogPostUploadService));
            builder.RegisterAssemblyTypes(assembly)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            var internalsAssembly = Assembly.GetAssembly(typeof(BlockPostMetadataResolver));
            builder.RegisterAssemblyTypes(internalsAssembly)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(ConsoleLogger<>))
                .As(typeof(IBlogEngineLogger<>))
                .InstancePerDependency();

            var container = builder.Build();

            return container;
        }
    }

    
}
