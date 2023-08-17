using System.ComponentModel.DataAnnotations;

namespace GeekShopping.WebApp.Models
{
    public class ProductModel
    {
        public long Id { get; set; }

        public string name { get; set; }
        public decimal price { get; set; }
        public string description { get; set; }
        public string categoryName { get; set; }
        public string imageUrl { get; set; }
        [Range(1,100)]
        public int count { get; set; } = 1;
        public string SubStringName()
        {
            if(name.Length < 24)
            {
                return name;
            }

            return $"{name.Substring(0, 21)}..";
        }

        public string SubStringDescription()
        {
            if (name.Length < 350)
            {
                return name;
            }

            return $"{name.Substring(0, 300)}..";
        }
    }
}
