using FunkyCode.Blog.App.Core;
using RestSharp;

namespace FunkyCode.Blog.Inf.AuthService
{
    public class AuthService : IAuthService
    {
        public UserAuthInfo GetUserAuthInfo()
        {
            var client = new RestClient("");

            var request = new RestRequest(".auth/me", Method.GET);

            var result = client.Execute(request);

            return new UserAuthInfo
            {
                RawResponse = result.Content
            };

        }

        public UserAuthInfo GetUserAuthInfo(string path)
        {
            var client = new RestClient(path);

            var request = new RestRequest(".auth/me", Method.GET);

            var result = client.Execute(request);

            return new UserAuthInfo
            {
                RawResponse = result.Content
            };
        }
    }
}
