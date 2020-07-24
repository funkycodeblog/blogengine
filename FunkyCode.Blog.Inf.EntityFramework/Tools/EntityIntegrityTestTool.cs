using System;
using FunkyCode.Blog.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunkyCode.Blog.Inf.EntityFramework.Tools
{
    public static class EntityIntegrityTestTool
    {
        public static async Task<List<HealthCheckItem>> Test(DbContext context)
        {
            var dbSetPropInfos = context.GetType()
                .GetProperties()
                .Where(p => p.PropertyType.IsGenericType &&
                            p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>));

            var entityRepoType = typeof(TestExecutor<>);

            var result = new List<HealthCheckItem>();

            foreach (var dbSetPropInfo in dbSetPropInfos)
            {
                    var genericArguments = dbSetPropInfo.PropertyType.GetGenericArguments();
                 
                    var testExecutorGenericType = entityRepoType.MakeGenericType(genericArguments);

                    var testExecutor = (ITestExecutor)Activator.CreateInstance(testExecutorGenericType);

                    var dbSet = dbSetPropInfo.GetValue(context);

                    var healthCheckResult = await testExecutor.Test(dbSetPropInfo.Name, dbSet);

                    result.Add(healthCheckResult);
            }

            return result;

        }

        private interface ITestExecutor
        {
            Task<HealthCheckItem> Test(string dbSetName, object dbSetRef) ;
        }

        private class TestExecutor<T> : ITestExecutor where T : class
        {
            public async Task<HealthCheckItem> Test(string dbSetName, object dbSetRef)
            {
                try
                {
                    var set = (DbSet<T>)dbSetRef;
                    var testItem = await set.FirstOrDefaultAsync();
                    return HealthCheckItem.Ok(dbSetName);
                }
                catch (Exception e)
                {
                    return HealthCheckItem.WithException(dbSetName, e);
                }
            }
        }


       

    }
}
