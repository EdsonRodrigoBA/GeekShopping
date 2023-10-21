using System.Net.Http.Headers;
using System.Text.Json;

namespace GeekShopping.WebApp.Utils
{
    public static class HttpClienteExtensios
    {
        private static MediaTypeHeaderValue contentType = new MediaTypeHeaderValue("application/json");
        public static async Task<T> ReadContentAsync<T>(this HttpResponseMessage response)
        {
            //if (!response.IsSuccessStatusCode)
            //{
            //    throw new ApplicationException($"Desculpe algo deu errado : {response.ReasonPhrase}");
            //}
            var data = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonSerializer.Deserialize<T>(data, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        }

        public static Task<HttpResponseMessage> PostAsJson<T>(this HttpClient httpClient, String url, T Data)
        {
            var dataString = JsonSerializer.Serialize(Data);
            var content = new StringContent(dataString);
            content.Headers.ContentType = contentType;
            return httpClient.PostAsync(url, content);

        }

        public static Task<HttpResponseMessage> PutAsJson<T>(this HttpClient httpClient, String url, T Data)
        {
            var dataString = JsonSerializer.Serialize(Data);
            var content = new StringContent(dataString);
            content.Headers.ContentType = contentType;
            return httpClient.PutAsync(url, content);

        }
    }
}
