using System.Collections.Generic;



namespace FunkyCode.Blog
{
    public interface IBlogEngineConfiguration
    {
        string DbConnectionString { get; }
        string Environment { get; }
        string UserPhotoPath { get; }
        

        string UsersAdGroup { get;  }

        EnvironmentTypeEnum EnvironmentType { get; }
        Dictionary<string, string> GetConfig();
    }
}
