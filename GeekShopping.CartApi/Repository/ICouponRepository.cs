// Copyright (c) GeekShoppingWeb. Todos os direitos reservados.
// .



using GeekShopping.CartApi.Data.ValueObjects;

namespace GeekShopping.CartApi.Repository
{
    public interface ICouponRepository
    {
        Task<CouponVO> GetCouponByCouponCode(string couponCode, string token);
    }
}
