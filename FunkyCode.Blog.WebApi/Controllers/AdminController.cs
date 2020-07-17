using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FunkyCode.Blog.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FunkyCode.Blog.Inf.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = SwaggerConfig.UtilsApi)]
    public class AdminController : ControllerBase
    {
        private readonly IBlogEngineConfiguration _configuration;
        private readonly IQueryProcessor _queryProcessor;

        public AdminController(IBlogEngineConfiguration configuration, IQueryProcessor queryProcessor)
        {
            _configuration = configuration;
            _queryProcessor = queryProcessor;
        }


        [HttpGet("exception")]
        public string Exception()
        {
            throw new Exception("This is test exception. Nothing to worry about!");
        }

        [HttpGet("config")]
        public Dictionary<string, string> Config()
        {
            return _configuration.GetConfig();
        }
        
        [HttpGet("dbhealth")]
        public async Task<Dictionary<string, string>> PerfromHealthCheck()
        {
            var headers =
                await _queryProcessor.Process<GetChealthchecksResultQuery, Dictionary<string, string>>(
                    new GetChealthchecksResultQuery());
            return headers;
        }

    }
}
