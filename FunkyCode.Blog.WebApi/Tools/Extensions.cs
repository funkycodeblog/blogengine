using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace FunkyCode.Blog
{
    public static class Extensions
    {

        public static string GetHeaderOrEmptyString(this HttpRequest request, string key)
        {

            if (request.Headers.TryGetValue(key, out StringValues values))
            {
                return values.FirstOrDefault();
            }

            return string.Empty;

        }

    }
}
