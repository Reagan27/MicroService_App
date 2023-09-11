using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using System.Text;

using EmailService.Models;
using EmailService.Service;


namespace EmailService.Messaging
{
    public class AzureMessageBusConsumer:IAzureMessageBusConsumer
    {
        private readonly IConfiguration _configuration;
        private readonly string Connectionstring;
        private readonly string QueueName;
        private readonly ServiceBusProcessor _registrationProcessor;
        private readonly EmailSendService _emailService;
        public readonly Emails _saveToDb;
        public AzureMessageBusConsumer(IConfiguration configuration, Emails service )
        {

            _configuration = configuration;
            Connectionstring= _configuration.GetSection("ServiceBus:ConnectionString").Get<string>();

            QueueName= _configuration.GetSection("QueuesandTopics:RegisterUser").Get<string>();

            var serviceBusClient = new ServiceBusClient(Connectionstring);
            _registrationProcessor = serviceBusClient.CreateProcessor(QueueName);
            _emailService = new EmailSendService();
            _saveToDb = service;
          

        }

        public async Task Start()
        {
            //start Processing
            _registrationProcessor.ProcessMessageAsync += OnRegistration;
            _registrationProcessor.ProcessErrorAsync += ErrorHandler;
            await _registrationProcessor.StartProcessingAsync();
        }

        public async Task Stop()
        {
            //Stop Processing
           await  _registrationProcessor.StopProcessingAsync();
           await _registrationProcessor.DisposeAsync();
        }
        private Task ErrorHandler(ProcessErrorEventArgs arg)
        {   

            //Todo send an email to Admin

           throw new NotImplementedException();
        }

        private async Task OnRegistration(ProcessMessageEventArgs arg)
        {
            var message= arg.Message;

            var body = Encoding.UTF8.GetString(message.Body);

            var userMessage= JsonConvert.DeserializeObject<UserMessage>(body);

            //sending An Email
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                // stringBuilder.Append("<img src=\"https://cdn.pixabay.com/photo/2016/02/22/20/22/bmw-1216469_640.jpg\" width=\"1000\" height=\"600\">");
                stringBuilder.Append("<h1> Hello " + userMessage.Name + "</h1>");
                stringBuilder.AppendLine("<br/>Welcome to this Social App ");

                stringBuilder.Append("<br/>");
                stringBuilder.Append('\n');
                stringBuilder.Append("<p> You have registered successfully</p>");
                var emailLoggers = new EmailLoggers(){
                    Email = userMessage.Email,
                    message = stringBuilder.ToString()
                };
                await _saveToDb.SaveData(emailLoggers);
                await _emailService.sendEmail(userMessage, stringBuilder.ToString());
                //delete the message from the queue
                 await arg.CompleteMessageAsync(message);
            }catch (Exception ex) { }
        }

       
    }
}