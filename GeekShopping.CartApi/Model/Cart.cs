namespace GeekShopping.CartApi.Model
{
    public class Cart
    {
        public CartHeader? cartHeader { get; set; }
        public IEnumerable<CartDetail>? cartDetails { get; set; }
    }
}
