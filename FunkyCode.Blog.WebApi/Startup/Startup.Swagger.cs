using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace FunkyCode.Blog.Inf.WebApi
{
    public static class SwaggerConfig
    {
        public const string BlogEngineApi = "blogEngine";
        public const string BlogEngineApiTitle = "Blog Engine Api";

        public const string UtilsApi = "utils";
        public const string UtilsApiTitle = "Utilities";
        
        public static void AddSwaggerConfig(this IServiceCollection services)
        {
            services.AddSwaggerGen(setup => {

                setup.SwaggerDoc(
                    BlogEngineApi,
                    new Info
                    {
                        Title = SwaggerConfig.BlogEngineApiTitle,
                        Version = "1",
                        Description = "Blog Engine Api exposes methods for client application.",

                    });

                setup.SwaggerDoc(
                    UtilsApi,
                    new Info
                    {
                        Title = SwaggerConfig.UtilsApiTitle,
                        Version = "1",
                        Description = "Utills Api for diagnostic operations.",

                    });

                setup.IncludeXmlComments(GetXmlDocumentationFileLocation(Assembly.GetExecutingAssembly()));
                setup.DescribeAllEnumsAsStrings();

                

            });

        }

        public static void UseSwaggerConfig(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(setup =>
            {
                setup.SwaggerEndpoint($"/swagger/{SwaggerConfig.BlogEngineApi}/swagger.json", SwaggerConfig.BlogEngineApiTitle);
                setup.SwaggerEndpoint($"/swagger/{SwaggerConfig.UtilsApi}/swagger.json", SwaggerConfig.UtilsApiTitle);
                setup.RoutePrefix = "api";
                // setup.InjectStylesheet();
            });



        }

        static string GetXmlDocumentationFileLocation(Assembly assembly)
        {
            var xmlCommentsFile = $"{assembly.GetName().Name}.xml";
            var xmlCommentsPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);
            return xmlCommentsPath;
        }

    }
}
