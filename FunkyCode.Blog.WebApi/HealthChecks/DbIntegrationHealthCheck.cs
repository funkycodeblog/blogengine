using FunkyCode.Blog.App.Core;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace FunkyCode.Blog.Inf.WebApi.HealthChecks
{

    public class Check : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }
    }


    public class DbIntegrationHealthCheck : IHealthCheck
    {
        private readonly IBlogRepository _blogRepository;

        public DbIntegrationHealthCheck(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var results = await _blogRepository.PerformHealthCheck();

            var isAnyError = results.Any(i => !i.IsOk);

            if (!isAnyError)
            {
                return HealthCheckResult.Healthy();
            }
            else
            {

                var unhealthyItems = results.Where(i => !i.IsOk).ToList();
                var description = string.Join(Environment.NewLine,
                    unhealthyItems.Select(i => $"[{i.Name}] {i.Exception?.Message ?? i.Description}"));

                return HealthCheckResult.Unhealthy(description);


            }
        }
    }
}
