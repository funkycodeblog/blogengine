using System;
using System.Collections.Generic;
using System.Text;

namespace FunkyCode.Blog.App.Core.Infrastructure.Internals
{
    public class ProcessingResult<T>
    {
        public List<string> Messages { get; set; } = new List<string>();
        public ProcessingStatus Status { get; set; }
        public T Result { get; set; }

        public void AddMessage(string msg)
        {
            Messages.Add(msg);
        }

        public void AddRequiredMessageIfNullOrEmpty(string propName, string value)
        {
            if (string.IsNullOrEmpty(value))
                Messages.Add($"{propName} is required!");
        }



    }
}
