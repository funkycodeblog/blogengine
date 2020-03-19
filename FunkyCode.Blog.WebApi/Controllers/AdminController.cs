using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
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

        public AdminController(IBlogEngineConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpGet("throw-test-exception")]
        public string ThrowTestException()
        {
            throw new Exception("This is test exception. Nothing to worry about!");
        }

        [HttpGet("get-configuration")]
        public Dictionary<string, string> Config()
        {
            return _configuration.GetConfig();
        }

    }
}
