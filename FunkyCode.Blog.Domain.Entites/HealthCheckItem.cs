using System;
using System.Collections.Generic;
using System.Text;

namespace FunkyCode.Blog.Domain.Entites
{
    public class HealthCheckItem
    {
        public bool IsOk { get; set; }
        public Exception Exception { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public static HealthCheckItem Ok(string name) => new HealthCheckItem { IsOk = true, Name = name};

        public static HealthCheckItem WithException(string name, Exception exc) => new HealthCheckItem { Exception = exc, Name = name};

    }

}
