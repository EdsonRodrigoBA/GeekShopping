using GeekShopping.CartApi.Model.Base;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.CartApi.Model
{

    [Table("CartHeader")]

    public class CartHeader : BaseEntity
    {
        public string? userId { get; set; }


        public string ? cuponCode { get; set; }

    }
}
