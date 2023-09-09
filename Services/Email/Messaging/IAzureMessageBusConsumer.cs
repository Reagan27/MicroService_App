namespace EmailService.Messaging
{
    public interface IAzureMessageBusConsumer
    {
        Task Start();


        Task Stop();
    }
}