// Copyright (c) GeekShoppingWeb. Todos os direitos reservados.
// .

using GeekShopping.CupomApi.Model.Repository;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.CupomApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly ICouponRepository _couponRepository;

        public CouponController(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
        }

        [HttpGet("couponCode/{couponCode}")]
        public async  Task<IActionResult> FindByCouponCode(string couponCode)
        {
            var coupon = await _couponRepository.GetCouponByCouponCode(couponCode);
            return Ok(coupon);
        }


    }
}
