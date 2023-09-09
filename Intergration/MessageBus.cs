using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheJituMessageBus{
    public class MessageBus:IMessageBus{
        public string ConnectionString = "Endpoint=sb://thejituservicebus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=t8nzEfJXay8fMcnUKqQyTZoTgW5HwImgI+ASbI/PvDA=";
        public async Task PublishMessage(object message, string queue_topic_name){
            var ServiceBus = new ServiceBusClient(this.ConnectionString);
            var sender = ServiceBus.CreateSender(queue_topic_name);

            var jsonMessage=JsonConvert.SerializeObject(message);
            var theMessage = new ServiceBusMessage(Encoding.UTF8.GetBytes(jsonMessage)){
                // Give a unique Id
                CorrelationId = Guid.NewGuid().ToString(),
            };
            // send
            await sender.SendMessageAsync(theMessage);
            // clean up resources
            await ServiceBus.DisposeAsync();
        }
    }
}