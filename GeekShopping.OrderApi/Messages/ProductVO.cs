using System.ComponentModel.DataAnnotations;

namespace GeekShopping.OrderApi.Messages
{

    public class ProductVO 
    {
        public long Id { get; set; }


        [Required(ErrorMessage ="Informe o nome do produto")]
        [StringLength(150,ErrorMessage ="O nome do produto deve ter no máximo 150 caracteres.")]
        public string name { get; set; }

        [Required]
        public decimal price { get; set; }


        [StringLength(500, ErrorMessage = "A descrição do produto deve ter no máximo 500 caracteres.")]
        public string description { get; set; }

        [StringLength(100, ErrorMessage = "A categoria deve ter no máximo 100 caracteres.")]

        public string categoryName { get; set; }

        [StringLength(300, ErrorMessage = "O caminho da imagem deve ter no máximo 300 caracteres.")]

        public String imageUrl { get; set; }

    }
}
