using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace FunkyCode.Blog.Inf.WebApi
{
    public class BlogEngineConfiguration : IBlogEngineConfiguration
    {
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _hostingEnvironment;

      

        public BlogEngineConfiguration(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        private string _dbConnectionString;
        public string DbConnectionString
        {
            get
            {
                if (null != _dbConnectionString)
                    return _dbConnectionString;

                _dbConnectionString = _configuration.GetConnectionString("defaultConnection");
                return _dbConnectionString;
            }
        }

        private string _environment;
        public string Environment => _environment ?? (_environment = _hostingEnvironment.EnvironmentName);
        
        public string UserPhotoPath  => "./Photos";


        private string _userAdGroup;
        public string UsersAdGroup
        {
            get
            {
                if (null != _userAdGroup)
                    return _userAdGroup;

                _userAdGroup = _configuration["UsersAdGroup"];
                return _userAdGroup;
            }
        }


        public EnvironmentTypeEnum EnvironmentType
        {
            get
            {
                switch (Environment)
                {
                    case "Local":
                        return EnvironmentTypeEnum.Local;
                    case "Development":
                        return EnvironmentTypeEnum.Development;
                    case "Production":
                        return EnvironmentTypeEnum.Production;

                    default:
                        return EnvironmentTypeEnum.Unknown;
                }
            }
        }

        public Dictionary<string, string> GetConfig()
        {
            var dict = new Dictionary<string, string>();
            var props = this.GetType().GetProperties();
            foreach (var propInfo in props)
            {
                var name = propInfo.Name;
                var value = $"{propInfo.GetValue(this)}";
                dict[name] = value;
            }

            return dict;
        }


    }
}
