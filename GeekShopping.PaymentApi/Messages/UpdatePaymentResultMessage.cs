// Copyright (c) GeekShoppingWeb. Todos os direitos reservados.
// .

using GeekShopping.MessageBuss;

namespace GeekShopping.OrderApi.Messages
{
    public class UpdatePaymentResultMessage : BaseMessage
    {
        public long OrderId { get; set; }
        public string Email { get; set; }

        public bool Status { get; set; }
    }
}
