using System.Text.Json.Serialization;

namespace GeekShopping.WebApp.Models
{


    public class CartHeaderViewModel
    {
        public long Id { get; set; }

        public string userId { get; set; }


        public string? cuponCode { get; set; }
        public string? couponCode => cuponCode;


        public decimal? purchaseAmount { get; set; }
        [JsonPropertyName("DiscountAmount")]
        public decimal? dicountTotal { get; set; }
        public string  firstName { get; set; }
        public string lastName { get; set; }
        public DateTime ? dateTime { get; set; }
        public string Phone { get; set; }

        public string email { get; set; }

        public string cvv { get; set; }
        public string cardNumber { get; set; }

        public string expiryMonthYear { get; set; }






    }
}
