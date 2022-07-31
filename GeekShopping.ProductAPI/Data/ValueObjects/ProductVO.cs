namespace GeekShopping.ProductAPI.Data.ValueObjects
{
    public class ProductVO
    {
        public long Id { get; set; }

        public string  name { get; set; }
        public decimal price { get; set; }
        public string  description { get; set; }
        public string  categoryName { get; set; }
        public string  imageUrl { get; set; }
    }
}
