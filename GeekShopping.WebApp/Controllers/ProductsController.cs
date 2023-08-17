using GeekShopping.WebApp.Models;
using GeekShopping.WebApp.Models.Services.Iservices;
using GeekShopping.WebApp.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.WebApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [Route("products")]
     
        public async Task<IActionResult> ProductIndex()
        {
            var token = await HttpContext.GetTokenAsync("access_token");

            var products = await  _productService.FindAll(token);
            return View(products);
        }


        [Route("ProductCreate")]
        public  IActionResult ProductCreate()
        {
            return View();
        }

        [Route("ProductCreate")]
        [HttpPost]
        [Authorize]

        public async Task<IActionResult> ProductCreate(ProductModel model)
        {


            if (ModelState.IsValid)
            {
                var token = await HttpContext.GetTokenAsync("access_token");

                var response = await _productService.Create(model, token);
                //response = null;
                if (!ReferenceEquals(response, null))
                {
                    return RedirectToAction(nameof(ProductIndex));
                }
          
            }

            return View(model);
        }

        [Route("ProductUpdate")]
        public async Task<IActionResult> ProductUpdate(int id)
        {
            var token = await HttpContext.GetTokenAsync("access_token");

            var product = await _productService.FindById(id,token);
            if(!ReferenceEquals(product, null))
            {
                return View(product);

            }
            else
            {
                return NotFound();
            }
        }

        [Route("ProductUpdate")]
        [HttpPost]
        [Authorize]

        public async Task<IActionResult> ProductUpdate(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                var token = await HttpContext.GetTokenAsync("access_token");

                var product = await _productService.Update(model,token);
                if (!ReferenceEquals(product, null))
                {
                    return RedirectToAction(nameof(ProductIndex));

                }
            }     
            return View(model);

        }


        [Route("ProductDelete")]
        [Authorize(Roles = Role.Admin)]

        public async Task<IActionResult> ProductDelete(int id)
        {
            var token = await HttpContext.GetTokenAsync("access_token");

            var product = await _productService.FindById(id,token);
            if (!ReferenceEquals(product, null))
            {
                return View(product);

            }
            else
            {
                return NotFound();
            }
        }

        [Route("ProductDelete")]
        [HttpPost]
        public async Task<IActionResult> ProductDelete(ProductModel model)
        {
            var token = await HttpContext.GetTokenAsync("access_token");

            var response = await _productService.Delete(model.Id,token);
                if (response)
                {
                    return RedirectToAction(nameof(ProductIndex));

                }
            
            return View(model);

        }

    }
}
