// Copyright (c) GeekShoppingWeb. Todos os direitos reservados.
// .

using GeekShopping.MessageBuss;

namespace GeekShopping.CartApi.RabbitMQMessageSender
{
    public interface IRabbitMQMessageSender
    {
        void SendMessage(BaseMessage message, string queueName);
    }
}
