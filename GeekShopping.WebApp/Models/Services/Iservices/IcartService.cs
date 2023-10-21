namespace GeekShopping.WebApp.Models.Services.Iservices
{
    public interface IcartService
    {
        Task<CartViewModel> GetCartByUserId(string userId, string access_token);

        Task<CartViewModel> AddItemToCart(CartViewModel cartViewModel, string access_token);
        Task<CartViewModel> UpdateItemToCart(CartViewModel cartViewModel, string access_token);

        Task<bool> RemoveFromCart(long cartDetailsId, string access_token);

        Task<bool> ApplyCoupon(CartViewModel cartViewModel, string codeCoupon, string access_token);
        Task<bool> RemoveCoupon(string userId, string access_token);
        Task<bool> ClearCart(string userId, string access_token);

        Task<CartViewModel> Checkout(CartHeaderViewModel cartHeaderViewModel, string access_token);
    }
}
