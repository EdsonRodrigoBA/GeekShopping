namespace GeekShopping.CartApi.Data.ValueObjects
{
    public class CartVO
    {
        public CartHeaderVO? cartHeader { get; set; }
        public IEnumerable<CartDetailVO>? cartDetails { get; set; }
    }
}
