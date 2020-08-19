using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Runtime.InteropServices;

namespace FunkyCode.Blog.Domain.Entites
{
    public class Subscriber
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public DateTime SubscriptionStart { get; set; }
        public bool IsTester { get; set; }
        public SubscriptionStatusTypeEnum Status { get; set; }

    }
}
