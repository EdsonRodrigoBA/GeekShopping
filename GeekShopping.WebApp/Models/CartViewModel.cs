namespace GeekShopping.WebApp.Models
{
    public class CartViewModel
    {
        public CartHeaderViewModel? cartHeader { get; set; }
        public IEnumerable<CartDetailViewModel>? cartDetails { get; set; }
    }
}
