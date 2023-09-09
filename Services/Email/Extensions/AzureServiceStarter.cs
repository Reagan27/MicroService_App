using EmailService.Messaging;

namespace EmailService.Extensions
{
    public static class AzureServiceStarter
    {
        public static IAzureMessageBusConsumer ServiceBusConsumerInstance { get; set; }
        public static  IApplicationBuilder useAzure(this IApplicationBuilder app)
        {
            ServiceBusConsumerInstance = app.ApplicationServices.GetService<IAzureMessageBusConsumer>();

            var HostLifetime= app.ApplicationServices.GetService<IHostApplicationLifetime>();

            HostLifetime.ApplicationStarted.Register(OnStart);
            HostLifetime.ApplicationStopping.Register(OnStop);

            return app;
        }

        private static void OnStop()
        {
           //Stop our Email Processor
           ServiceBusConsumerInstance.Stop();
        }

        private static void OnStart()
        {
            //Call our Email Processor
            ServiceBusConsumerInstance.Start();
        }
    }
}