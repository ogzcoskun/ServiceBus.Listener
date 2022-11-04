using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceBus.Listener.App.Models
{
    public class SmsModel
    {
        public string Phone { get; set; }
        public string Message { get; set; }
    }
}
