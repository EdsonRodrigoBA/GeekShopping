// Copyright (c) GeekShoppingWeb. Todos os direitos reservados.
// .

namespace GeekShopping.MessageBuss
{
    public class BaseMessage
    {
        public long Id { get; set; }
        public DateTime messageCreated { get; set; }
    }
}
