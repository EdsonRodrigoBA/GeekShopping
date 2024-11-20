using GeekShopping.CartApi.Data.ValueObjects;
using GeekShopping.CartApi.Model.Message;
using GeekShopping.CartApi.RabbitMQMessageSender;
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
        private readonly IRabbitMQMessageSender _rabbitMQMessageSender;
        private readonly ICouponRepository _couponRepository;

        public CartController(ICartRepository cartRepository, IRabbitMQMessageSender rabbitMQMessageSender, ICouponRepository couponRepository)
        {
            _cartRepository = cartRepository;
            _rabbitMQMessageSender = rabbitMQMessageSender;
            _couponRepository = couponRepository;
        }

        [HttpGet("find-cart/{userId}")]
        public async Task<IActionResult> Get(string userId)
        {

            var cart = await _cartRepository.GetCartByUserId(userId);
            if (cart == null) return NoContent();

            return Ok(cart);
        }



        [HttpPost("add-cart")]
        public async Task<IActionResult> AddCart(CartVO cartVO)
        {
            try
            {


                var cart = await _cartRepository.SaveOrUpdateCart(cartVO);
                if (cart == null) return NoContent();

                return Ok(cart);
            }
            catch (Exception ex)
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


        [HttpPost("apply-coupon")]
        public async Task<IActionResult> ApplyCoupon(CartVO cartVO)
        {
            try
            {


                var status = await _cartRepository.ApplyCoupon(cartVO.cartHeader.userId, cartVO.cartHeader.cuponCode);
                if (!status) return NoContent();

                return Ok(status);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpDelete("remove-coupon/{userId}")]
        public async Task<IActionResult> removeCoupon(string userId)
        {
            try
            {
                var status = await _cartRepository.RemoveCoupon(userId);
                if (status) return Ok(status);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost("checkout")]
        public async Task<IActionResult> checkout(CheckoutHeaderVO model)
        {
            try
            {
                if (model?.UserId == null) return BadRequest();
                var cart = await _cartRepository.GetCartByUserId(model.UserId);
                if (cart == null) return NoContent();
                string token = Request.Headers["Authorization"];
                if (!string.IsNullOrEmpty(model.cuponCode))
                {
                    CouponVO coupon = await _couponRepository.GetCouponByCouponCode(model.cuponCode, token);
                    if(model.DiscountAmount != coupon.discountAmount)
                    {
                        return StatusCode(412);
                    }
                }

                model.CartDetails = cart.cartDetails;

                _rabbitMQMessageSender.SendMessage(model, "checkout");
                await _cartRepository.ClearCart(model.UserId);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
