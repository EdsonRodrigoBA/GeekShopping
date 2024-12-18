using GeekShopping.OrderApi.Model.Base;

using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.OrderApi.Model
{
    [Table("OrderHeader")]

    public class OrderHeader : BaseEntity
    {
        public string? UserId { get; set; }
        public string? CouponCode { get; set; }
        public decimal? PurchaseAmount { get; set; }
        public decimal? DiscountAmount { get; set; }
        public string? FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateTime { get; set; }

        public DateTime? OrderDateTime { get; set; }

        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? CardNumber { get; set; }
        public string? CVV { get; set; }
        public string? ExpiryMonthYear { get; set; }
        public int? OrderTotalItens { get; set; }
        public List<OrderDetail>? OrderDetails { get; set; }
        public bool ?  PaymentStatus { get; set; }

    }
}
