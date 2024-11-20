// Copyright (c) GeekShoppingWeb. Todos os direitos reservados.
// .

using System.Net.Http.Headers;
using System.Text.Json;

using AutoMapper;

using GeekShopping.CartApi.Data.ValueObjects;


namespace GeekShopping.CartApi.Repository
{
    public class CouponRepository : ICouponRepository
    {
        private readonly HttpClient _client;
        public const string basePath = "api/v1/Coupon";

        public CouponRepository(HttpClient client)
        {
            _client = client;
        }

        public async Task<CouponVO> GetCouponByCouponCode(string couponCode, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync($"{basePath}/couponCode/{couponCode}");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode) return new CouponVO();
            return JsonSerializer.Deserialize<CouponVO>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}
