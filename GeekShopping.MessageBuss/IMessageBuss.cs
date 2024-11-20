// Copyright (c) GeekShoppingWeb. Todos os direitos reservados.
// .

namespace GeekShopping.MessageBuss
{
    public interface IMessageBuss
    {
        Task PublicMessage(BaseMessage message, string  queueName);
    }
}
