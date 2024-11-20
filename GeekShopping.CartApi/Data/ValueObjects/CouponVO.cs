
using System.ComponentModel.DataAnnotations;

namespace GeekShopping.CartApi.Data.ValueObjects
{
    public class CouponVO
    {
        public long Id { get; set; }
        public string name { get; set; }
        public string couponCode { get; set; }
        public decimal discountAmount { get; set; }

    }
}
