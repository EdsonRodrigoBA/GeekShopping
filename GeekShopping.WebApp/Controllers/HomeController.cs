using GeekShopping.WebApp.Models;
using GeekShopping.WebApp.Models.Services.Iservices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GeekShopping.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        private readonly IcartService _icartService;

        public HomeController(ILogger<HomeController> logger, IProductService productService, IcartService icartService)
        {
            _logger = logger;
            _productService = productService;
            _icartService = icartService;
        }

        public async Task<IActionResult> Index()
        {
            //var token = await HttpContext.GetTokenAsync("access_token");

            var products = await _productService.FindAll("");
            return View(products);
        }
        [Authorize]
        public async Task<IActionResult> Details(long id)
        {
            var token = await HttpContext.GetTokenAsync("access_token");

            var product = await _productService.FindById(id, token);
            return View(product);
        }

        [Authorize]
        [HttpPost]
        [ActionName("Details")]
        public async Task<IActionResult> DetailsPost(ProductViewModel model)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            CartViewModel cart = new CartViewModel()
            {
                cartHeader = new CartHeaderViewModel()
                {
                    userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value,

                },


            };
            CartDetailViewModel cartDetail = new CartDetailViewModel()
            {
                count = model.count,
                productId = model.Id,
                product = await _productService.FindById(model.Id, token),

            };

            List<CartDetailViewModel> cartDetails = new List<CartDetailViewModel>();
            cartDetails.Add(cartDetail);
            cart.cartDetails = cartDetails;

            var response = await _icartService.AddItemToCart(cart, token);
            if(response != null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        public IActionResult Privacy()
        {
            return View();
        }
        [Authorize]
        public async Task<IActionResult> Login()
        {
            var access_token = await HttpContext.GetTokenAsync("access_token");
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Loggout()
        {
            return SignOut("Cookies", "oidc");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}