// Copyright (c) GeekShoppingWeb. Todos os direitos reservados.
// .

using GeekShopping.CupomApi.Data.ValueObject;

namespace GeekShopping.CupomApi.Model.Repository
{
    public interface ICouponRepository
    {
        Task<CouponVO> GetCouponByCouponCode(string couponCode);
    }
}
