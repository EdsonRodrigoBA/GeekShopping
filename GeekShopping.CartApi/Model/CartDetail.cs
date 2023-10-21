using GeekShopping.CartApi.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.CartApi.Model
{
    [Table("CartDetails")]
    public class CartDetail : BaseEntity
    {
        public long? cartHeaderId { get; set; }

        [ForeignKey("cartHeaderId")]
        public virtual CartHeader? cartHeader { get; set; }

        public long? productId { get; set; }

        [ForeignKey("productId")]
        public virtual Product product { get; set; }

        [Column("count")]
        public int? count { get; set; }
    }
}
