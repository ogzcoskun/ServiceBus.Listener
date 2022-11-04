﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceBus.Listener.App.Models
{
    public class OrderModel
    {
        public string OrderId { get; set; }
        public string ProductId { get; set; }
        public double Price { get; set; }
        public string Date { get; set; }
        public string UserMail { get; set; }
        public string Phone { get; set; }
    }
}
