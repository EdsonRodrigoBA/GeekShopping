using System.ComponentModel.DataAnnotations.Schema;

using GeekShopping.OrderApi.Model.Base;

namespace GeekShopping.OrderApi.Model
{
    [Table("OrderDetails")]
    public class OrderDetail : BaseEntity
    {
        public long? orderHeaderId { get; set; }

        [ForeignKey("orderHeaderId")]
        public virtual OrderHeader? orderHeader { get; set; }


        [Column("produtcId")]
        public long? productId { get; set; }

        [Column("count")]
        public int? count { get; set; }

        public string productName { get; set; }

        public decimal price { get; set; }

    }
}
