using GeekShopping.CartApi.Data.ValueObjects;
using GeekShopping.CartApi.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.CartApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/v1/[controller]")]
    public class CartController : ControllerBase
    {


        private readonly ICartRepository _cartRepository;

        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        [HttpGet("find-cart/{userId}")]
        public async Task<IActionResult> Get(string userId)
        {

            var cart =  await _cartRepository.GetCartByUserId(userId);
            if(cart == null) return NoContent();

            return Ok(cart);
        }



        [HttpPost("add-cart")]
        public async Task<IActionResult> AddCart(CartVO cartVO)
        {
            try{
        

            var cart = await _cartRepository.SaveOrUpdateCart(cartVO);
            if (cart == null) return NoContent();

            return Ok(cart);
            }catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("update-cart")]
        public async Task<IActionResult> UpdateCart(CartVO cartVO)
        {

            var cart = await _cartRepository.SaveOrUpdateCart(cartVO);
            if (cart == null) return NoContent();

            return Ok(cart);
        }

        [HttpDelete("delete-cart/{id}")]
        public async Task<IActionResult> DeleteCart(long id)
        {

            var cart = await _cartRepository.RemoveFromCart(id);


            return StatusCode(204, cart);
        }
    }
}