namespace GeekShopping.OrderApi.Messages
{
    public class CartDetailVO 
    {
        public long Id { get; set; }

        public long? cartHeaderId { get; set; }


        public long productId { get; set; }

        public virtual ProductVO product { get; set; }

        public int? count { get; set; }
    }
}
