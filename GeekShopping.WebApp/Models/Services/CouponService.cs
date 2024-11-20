// Copyright (c) GeekShoppingWeb. Todos os direitos reservados.
// .

using System.Net.Http.Headers;

using GeekShopping.WebApp.Models.Services.Iservices;
using GeekShopping.WebApp.Utils;

namespace GeekShopping.WebApp.Models.Services
{
    public class CouponService : ICouponService
    {
        private readonly HttpClient _client;
        public const string basePath = "api/v1/Coupon";

        public CouponService(HttpClient client)
        {
            _client = client;
        }
        public async Task<CouponViewModel> GetCoupon(string code, string access_token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);

            var response = await _client.GetAsync($"{basePath}/couponCode/{code}");
            if (response.IsSuccessStatusCode)
            {
                return await response.ReadContentAsync<CouponViewModel>();

            }
            else
            {
                return new CouponViewModel();
            }
        }
    }
}
