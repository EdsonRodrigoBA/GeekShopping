// Copyright (c) GeekShoppingWeb. Todos os direitos reservados.
// .

namespace GeekShopping.OrderApi.Messages
{
    public class UpdatePaymentResultVO
    {
        public long OrderId { get; set; }
        public string Email { get; set; }

        public bool Status { get; set; }
    }
}
