namespace GeekShopping.WebApp.Models
{


    public class CartHeaderViewModel
    {
        public long Id { get; set; }

        public string userId { get; set; }


        public string? cuponCode { get; set; }

        public double? purchaseAmount { get; set; }

    }
}
