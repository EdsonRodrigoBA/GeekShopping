﻿using GeekShopping.CartApi.Data.ValueObjects;

namespace GeekShopping.CartApi.Repository
{
    public interface ICartRepository
    {
        Task<CartVO> GetCartByUserId(string userId);

        Task<CartVO> SaveOrUpdateCart(CartVO cartVo);
        Task<bool> RemoveFromCart(long cartDetailsId);

        Task<bool> ApplyCoupon(string userId, string codeCoupon);
        Task<bool> RemoveCoupon(string userId);
        Task<bool> ClearCart(string userId);

    }
}
