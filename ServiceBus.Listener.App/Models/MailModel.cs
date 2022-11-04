using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceBus.Listener.App.Models
{
    public class MailModel
    {
        public string Email { get; set; }
        public string Message { get; set; }
        public string OrderId { get; set; }
    }
}
