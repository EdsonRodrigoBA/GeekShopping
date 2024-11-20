namespace GeekShopping.WebApp.Models.Services.Iservices
{
    public interface ICouponService
    {
        Task<CouponViewModel> GetCoupon(string code, string access_token);


    }
}
