using GeekShopping.WebApp.Models;
using GeekShopping.WebApp.Models.Services.Iservices;
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
            var products = await  _productService.FindAll();
            return View(products);
        }


        [Route("ProductCreate")]
        public  IActionResult ProductCreate()
        {
            return View();
        }

        [Route("ProductCreate")]
        [HttpPost]
        public async Task<IActionResult> ProductCreate(ProductModel model)
        {


            if (ModelState.IsValid)
            {
                var response = await _productService.Create(model);
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
            var product = await _productService.FindById(id);
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
        public async Task<IActionResult> ProductUpdate(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                var product = await _productService.Update(model);
                if (!ReferenceEquals(product, null))
                {
                    return RedirectToAction(nameof(ProductIndex));

                }
            }     
            return View(model);

        }


        [Route("ProductDelete")]
        public async Task<IActionResult> ProductDelete(int id)
        {
            var product = await _productService.FindById(id);
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
           
                var response = await _productService.Delete(model.Id);
                if (response)
                {
                    return RedirectToAction(nameof(ProductIndex));

                }
            
            return View(model);

        }

    }
}
