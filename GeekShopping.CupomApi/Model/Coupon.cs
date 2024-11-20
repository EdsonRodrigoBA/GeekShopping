using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using GeekShopping.CupomApi.Model.Base;

namespace GeekShopping.CupomApi.Model
{

    [Table("coupon")]
    public class Coupon : BaseEntity
    {



        [StringLength(150,ErrorMessage ="O nome do produto deve ter no máximo 150 caracteres.")]
        public string name { get; set; }

        [Required(ErrorMessage = "Informe o nome do produto")]
        [StringLength(50, ErrorMessage = "O nome do produto deve ter no máximo 50 caracteres.")]
        public string couponCode { get; set; }

        [Required]
        public decimal discountAmount { get; set; }



    }
}
