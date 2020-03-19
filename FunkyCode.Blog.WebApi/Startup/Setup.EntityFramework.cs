using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FunkyCode.Blog.Inf.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace FunkyCode.Blog.WebApi
{
    public static class SetupEntityFramework
    {

        public static void AddEntityFramework(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("defaultConnection");

            services.AddDbContext<BlogContext>(options => {
                options.UseSqlServer(connectionString, sqlServerOptions => {
                    
                });

            });



        }

    }
}
