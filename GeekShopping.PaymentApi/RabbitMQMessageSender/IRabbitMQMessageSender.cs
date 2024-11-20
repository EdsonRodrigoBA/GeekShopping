// Copyright (c) GeekShoppingWeb. Todos os direitos reservados.
// .

using GeekShopping.MessageBuss;

namespace GeekShopping.PaymentApi.RabbitMQMessageSender
{
    public interface IRabbitMQMessageSender
    {
        void SendMessage(BaseMessage message);
    }
}
