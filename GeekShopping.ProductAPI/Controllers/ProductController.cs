﻿using GeekShopping.ProductAPI.Data.ValueObjects;
using GeekShopping.ProductAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.ProductAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IproductRepository _iproductRepository;

        public ProductController(IproductRepository iproductRepository )
        {
            _iproductRepository = iproductRepository ?? throw new ArgumentNullException(nameof(iproductRepository));
        }
        
        
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductVO>>  FindById(long id)
        {
            var product = await _iproductRepository.FindById(id);
            if(product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable< ProductVO> >> FindAll()
        {
            var products = await _iproductRepository.FindAll();

            return Ok(products);
        }

        [HttpPost]
        public async Task<ActionResult<ProductVO>> Create([FromBody] ProductVO vo)
        {
            if(vo == null)
            {
                return BadRequest();
            }
            var product = await _iproductRepository.Create(vo);


            return Ok(product);
        }

        [HttpPut]
        public async Task<ActionResult<ProductVO>> Update([FromBody] ProductVO vo)
        {
            if (vo == null)
            {
                return BadRequest();
            }
            var product = await _iproductRepository.Update(vo);


            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            var status = await _iproductRepository.Delete(id);
            if (!status)
            {
                return BadRequest();
            }

            return Ok(status);
        }

    }
}
