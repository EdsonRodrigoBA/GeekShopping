namespace GeekShopping.WebApp.Models
{
    public class CartDetailViewModel
    {
        public long Id { get; set; }

        public long? cartHeaderId { get; set; }

        public CartHeaderViewModel? cartHeader { get; set; }

        public long productId { get; set; }

        public ProductViewModel product { get; set; }

        public int count { get; set; }
    }
}
