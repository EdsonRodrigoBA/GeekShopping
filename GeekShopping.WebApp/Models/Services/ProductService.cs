using GeekShopping.WebApp.Models.Services.Iservices;
using GeekShopping.WebApp.Utils;
using System.Net.Http;
using System.Net.Http.Headers;

namespace GeekShopping.WebApp.Models.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _client;
        public const string basePath = "api/v1/product";

        public ProductService(HttpClient client)
        {
            _client = client;
        }

        public async Task<ProductViewModel> Create(ProductViewModel vo, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.PostAsJson(basePath, vo);
            if (response.IsSuccessStatusCode)
            {
                return await response.ReadContentAsync<ProductViewModel>();

            }
            else
            {
                throw new Exception("Ocorreu um erro. Tente novamente.");
            }
        }

        public async Task<bool> Delete(long id, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.DeleteAsync($"{basePath}/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.ReadContentAsync<bool>();

            }
            else
            {
                throw new Exception("Ocorreu um erro. Tente novamente.");
            }
        }

        public async Task<IEnumerable<ProductViewModel>> FindAll(string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.GetAsync(basePath);
            return await response.ReadContentAsync<List<ProductViewModel>>();
        }

        public async Task<ProductViewModel> FindById(long Id, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.GetAsync($"{basePath}/{Id}");
            return await response.ReadContentAsync<ProductViewModel>();
        }

        public async Task<ProductViewModel> Update(ProductViewModel vo, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.PutAsJson(basePath, vo);
            if (response.IsSuccessStatusCode)
            {
                return await response.ReadContentAsync<ProductViewModel>();

            }
            else
            {
                throw new Exception("Ocorreu um erro. Tente novamente.");
            }
        }
    }
}
