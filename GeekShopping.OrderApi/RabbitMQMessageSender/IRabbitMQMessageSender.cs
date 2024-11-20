// Copyright (c) GeekShoppingWeb. Todos os direitos reservados.
// .

using GeekShopping.MessageBuss;

namespace GeekShopping.OrderApi.RabbitMQMessageSender
{
    public interface IRabbitMQMessageSender
    {
        void SendMessage(BaseMessage message, string queueName);
    }
}
