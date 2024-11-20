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
        private readonly ICouponService _couponService;


        public CartController(IProductService productService, IcartService cartService, ICouponService couponService)
        {
            _productService = productService;
            _cartService = cartService;
            _couponService = couponService;
        }
        [Authorize]
        [HttpPost]
        [ActionName("ApplyCoupon")]
        public async Task<IActionResult> ApplyCoupon(CartViewModel model)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;

            var response = await _cartService.ApplyCoupon(model, token);

            if (response)
            {
                return RedirectToAction(nameof(CartIndex));
            }
            return View();
        }

        [Authorize]
        [HttpPost]
        [ActionName("RemoveCoupon")]
        public async Task<IActionResult> RemoveCoupon()
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;

            var response = await _cartService.RemoveCoupon(userId, token);

            if (response)
            {
                return RedirectToAction(nameof(CartIndex));
            }
            return View();
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

        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
            return View(await FindUserCart());
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(CartViewModel model)
        {

            var token = await HttpContext.GetTokenAsync("access_token");
            model.cartHeader.dateTime = DateTime.Now;
            var response = await _cartService.Checkout(model.cartHeader, token);

            if(response != null && response.GetType() == typeof(string))
            {
                TempData["Error"] = response;
                return RedirectToAction(nameof(Checkout));

            }
            if (response != null)
            {
                return RedirectToAction(nameof(Confirmation));
            }
            return View(await FindUserCart());
        }
        [HttpGet]
        public async Task<IActionResult> Confirmation()
        {
            return View(await FindUserCart());
        }
        private async Task<CartViewModel> FindUserCart()
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;

            var response = await _cartService.GetCartByUserId(userId, token);

            if (response?.cartHeader != null)
            {
                if (!String.IsNullOrEmpty(response.cartHeader.cuponCode))
                {
                    var coupon = await _couponService.GetCoupon(response.cartHeader.cuponCode, token);
                    if(coupon?.couponCode != null)
                    {
                        response.cartHeader.dicountTotal = coupon.discountAmount;
                    }
                }    
                foreach (var detail in response.cartDetails)
                {
                    response.cartHeader.purchaseAmount =  Convert.ToDecimal(response.cartHeader.purchaseAmount) + Convert.ToDecimal(detail.product.price * detail.count);
                }

                response.cartHeader.purchaseAmount = Convert.ToDecimal(response.cartHeader.purchaseAmount) - response.cartHeader.dicountTotal;
            }
            return response;
        }
    }
}
