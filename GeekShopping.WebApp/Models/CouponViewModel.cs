// Copyright (c) GeekShoppingWeb. Todos os direitos reservados.
// .

namespace GeekShopping.WebApp.Models
{
    public class CouponViewModel
    {
        public long Id { get; set; }
        public string name { get; set; }
        public string couponCode { get; set; }
        public decimal discountAmount { get; set; }
    }
}
