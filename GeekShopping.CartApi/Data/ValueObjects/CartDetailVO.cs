namespace GeekShopping.CartApi.Data.ValueObjects
{
    public class CartDetailVO 
    {
        public long Id { get; set; }

        public long? cartHeaderId { get; set; }

        public CartHeaderVO? cartHeader { get; set; }

        public long productId { get; set; }

        public ProductVO product { get; set; }

        public int? count { get; set; }
    }
}
