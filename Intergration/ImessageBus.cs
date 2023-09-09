﻿
namespace TheJituMessageBus{
    public interface IMessageBus
{
    Task PublishMessage(object message, string queue_topic_name);
}

}
