using GeekShopping.WebApp.Models.Services.Iservices;
using GeekShopping.WebApp.Utils;

using System.Net;
using System.Net.Http.Headers;

namespace GeekShopping.WebApp.Models.Services
{
    public class CartService : IcartService
    {
        private readonly HttpClient _client;
        public const string basePath = "api/v1/Cart";

        public CartService(HttpClient client)
        {
            _client = client;
        }
        public async Task<CartViewModel> AddItemToCart(CartViewModel cartViewModel, string access_token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
            var response = await _client.PostAsJson($"{basePath}/add-cart", cartViewModel);
            var t = await response.ReadContentAsync<object>();
            if (response.IsSuccessStatusCode)
            {
                return await response.ReadContentAsync<CartViewModel>();

            }
            else
            {
                throw new Exception("Ocorreu um erro. Tente novamente.");
            }
        }

        public async Task<bool> ApplyCoupon(CartViewModel cartViewModel, string access_token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);

            var response = await _client.PostAsJson($"{basePath}/apply-coupon/", cartViewModel);
            if (response.IsSuccessStatusCode)
            {
                var r = response?.ReadContentAsync<bool>();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<Object> Checkout(CartHeaderViewModel cartHeaderViewModel, string access_token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);

            var response = await _client.PostAsJson($"{basePath}/checkout/", cartHeaderViewModel);
            var stringRet = await response?.ReadContentAsync<object>();

            if (response.IsSuccessStatusCode)
            {
                var r = await response?.ReadContentAsync<CartHeaderViewModel>();
                return r;
            }
            else if (response.StatusCode.Equals(HttpStatusCode.PreconditionFailed))
            {
                return "Coupon price has changed, prease confirm";
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> ClearCart(string userId, string access_token)
        {
            throw new NotImplementedException();
        }

        public async Task<CartViewModel> GetCartByUserId(string userId, string access_token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);

            var response = await _client.GetAsync($"{basePath}/find-cart/{userId}");
            return await response.ReadContentAsync<CartViewModel>();
        }

        public async Task<bool> RemoveCoupon(string userId, string access_token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);

            var response = await _client.DeleteAsync($"{basePath}/remove-coupon/{userId}");
            if (response.IsSuccessStatusCode)
            {
                var r = response?.ReadContentAsync<bool>();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> RemoveFromCart(long cartDetailsId, string access_token)
        {

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);

            var response = await _client.DeleteAsync($"{basePath}/delete-cart/{cartDetailsId}");
            if (response.IsSuccessStatusCode)
            {
               var r = response?.ReadContentAsync<bool>();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<CartViewModel> UpdateItemToCart(CartViewModel cartViewModel, string access_token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
            var response = await _client.PutAsJson($"{basePath}/update-cart", cartViewModel);
            if (response.IsSuccessStatusCode)
            {
                return await response.ReadContentAsync<CartViewModel>();

            }
            else
            {
                throw new Exception("Ocorreu um erro. Tente novamente.");
            }
        }
    }
}
