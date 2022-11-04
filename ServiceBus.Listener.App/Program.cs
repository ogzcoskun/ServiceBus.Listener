using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using RestSharp;
using ServiceBus.Listener.App.Models;
using System;
using System.Text;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace ServiceBus.Listener.App
{
    internal class Program
    {

        private static string serviceBusConnection = "Endpoint=sb://oguzdevtestservicebusdemo.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=/SlHbQXonfpKPfwmFI8XUvn5SINlTsQ1rsou/Uk2NI4=";
        private static string queueName = "ordercreatedqueue";
        private static IQueueClient client;

        static  void Main(string[] args)
        {
            var listenSwitch = true;

            client = new QueueClient(serviceBusConnection, queueName);


            var options = new MessageHandlerOptions(ExceptionMethod)
               {
                  
                  AutoComplete = false
               };

            client.RegisterMessageHandler(ExecuteMessageProcessing, options);

            Console.ReadLine(); 

            
        }


        private static async Task ExecuteMessageProcessing(Message message, CancellationToken arg2)
        {
            var result = JsonConvert.DeserializeObject<OrderModel>(Encoding.UTF8.GetString(message.Body));

            var mailModel = new MailModel()
            {
                Email = result.UserMail,
                Message = ".",
                OrderId = result.OrderId
            };

            var mailToSend = JsonConvert.SerializeObject(mailModel);

            var restClient = new RestClient
            {
                BaseUrl = new Uri($"https://localhost:44382/Email/SendEmail")
            };

            var request = new RestRequest();
            request.Method = Method.POST;
            request.AddJsonBody(mailToSend);
            var response = restClient.Execute(request);


            Console.WriteLine($"Order Id is {result.UserMail}, Order name is {result.Price} and quantity is {result.UserMail}");

            client.CompleteAsync(message.SystemProperties.LockToken);
        }
        private static async Task ExceptionMethod(ExceptionReceivedEventArgs arg)
        {
            await Task.Run(() =>
           Console.WriteLine($"Error occured. Error is {arg.Exception.Message}")
           );
        }

    }
}
