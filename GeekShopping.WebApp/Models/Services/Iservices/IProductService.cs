namespace GeekShopping.WebApp.Models.Services.Iservices
{
    public interface IProductService
    {
        Task<IEnumerable<ProductModel>> FindAll(string token);
        Task<ProductModel> FindById(long Id, string token);
        Task<ProductModel> Create(ProductModel vo, string token);
        Task<ProductModel> Update(ProductModel vo, string token);
        Task<bool> Delete(long id, string token);
    }
}
