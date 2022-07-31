namespace GeekShopping.WebApp.Models.Services.Iservices
{
    public interface IProductService
    {
        Task<IEnumerable<ProductModel>> FindAll();
        Task<ProductModel> FindById(long Id);
        Task<ProductModel> Create(ProductModel vo);
        Task<ProductModel> Update(ProductModel vo);
        Task<bool> Delete(long id);
    }
}
