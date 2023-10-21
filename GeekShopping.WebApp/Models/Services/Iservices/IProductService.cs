namespace GeekShopping.WebApp.Models.Services.Iservices
{
    public interface IProductService
    {
        Task<IEnumerable<ProductViewModel>> FindAll(string token);
        Task<ProductViewModel> FindById(long Id, string token);
        Task<ProductViewModel> Create(ProductViewModel vo, string token);
        Task<ProductViewModel> Update(ProductViewModel vo, string token);
        Task<bool> Delete(long id, string token);
    }
}
