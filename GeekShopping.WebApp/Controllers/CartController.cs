using GeekShopping.WebApp.Models;
using GeekShopping.WebApp.Models.Services;
using GeekShopping.WebApp.Models.Services.Iservices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.WebApp.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductService _productService;
        private readonly IcartService _cartService;

        public CartController(IProductService productService, IcartService cartService)
        {
            _productService = productService;
            _cartService = cartService;
        }
        [Authorize]
        public async Task<IActionResult> CartIndex()
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;

            return View(await FindUserCart());
        }

        public async Task<IActionResult> Remove(int id)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;

            var response = await _cartService.RemoveFromCart(id, token);

            if (response)
            {
                return RedirectToAction(nameof(CartIndex));
            }
            return View();
        }

        private async Task<CartViewModel> FindUserCart()
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;

            var response = await _cartService.GetCartByUserId(userId, token);

            if (response?.cartHeader != null)
            {
                foreach (var detail in response.cartDetails)
                {
                    response.cartHeader.purchaseAmount += Convert.ToDouble(detail.product.price * detail.count);
                }
            }
            return response;
        }
    }
}
