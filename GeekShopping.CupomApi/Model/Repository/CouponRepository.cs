// Copyright (c) GeekShoppingWeb. Todos os direitos reservados.
// .

using AutoMapper;

using GeekShopping.CupomApi.Data.ValueObject;
using GeekShopping.CupomApi.Models.Context;

using Microsoft.EntityFrameworkCore;

namespace GeekShopping.CupomApi.Model.Repository
{
    public class CouponRepository : ICouponRepository
    {
        private readonly MySqlCupmApiContext _context;
        private readonly IMapper _mapper;

        public CouponRepository(MySqlCupmApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CouponVO> GetCouponByCouponCode(string couponCode)
        {
            var coupon = await _context.Coupon.FirstOrDefaultAsync(x => x.couponCode == couponCode);
            return _mapper.Map<CouponVO>(coupon);
        }
    }
}
