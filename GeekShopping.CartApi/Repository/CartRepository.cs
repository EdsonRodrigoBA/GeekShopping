using AutoMapper;
using GeekShopping.CartApi.Data.ValueObjects;
using GeekShopping.CartApi.Model;
using GeekShopping.CartApi.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.CartApi.Repository
{
    public class CartRepository : ICartRepository
    {

        private readonly MySqlCartApiContext _context;
        private readonly IMapper _mapper;

        public CartRepository(MySqlCartApiContext mySqlContext, IMapper mapper)
        {
            _context = mySqlContext;
            _mapper = mapper;
        }
        public async Task<bool> ApplyCoupon(string userId, string codeCoupon)
        {
            var cartHeader = await _context.cartHeaders.FirstOrDefaultAsync(x => x.userId == userId);
            if (cartHeader != null)
            {

                cartHeader.cuponCode = codeCoupon;
                _context.cartHeaders.Update(cartHeader);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> ClearCart(string userId)
        {
            var cartHeader= await _context.cartHeaders.FirstOrDefaultAsync(x => x.userId == userId);
            if(cartHeader != null)
            {

                _context.cartDetails.RemoveRange(
                    _context.cartDetails.Where(x => x.cartHeaderId == cartHeader.Id));
                _context.cartHeaders.Remove(cartHeader);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<CartVO> GetCartByUserId(string userId)
        {
            Cart cart = new Cart()
            {
                cartHeader = await _context.cartHeaders.FirstOrDefaultAsync(c => c.userId == userId),

            };
            if(cart.cartHeader != null)
            {
                cart.cartDetails = _context.cartDetails.Where(x => x.cartHeaderId == cart.cartHeader.Id)
                    .Include(p => p.product);
            }



            return _mapper.Map<CartVO>(cart);
        }

        public async Task<bool> RemoveCoupon(string userId)
        {
            var cartHeader = await _context.cartHeaders.FirstOrDefaultAsync(x => x.userId == userId);
            if (cartHeader != null)
            {

                cartHeader.cuponCode = "";
                _context.cartHeaders.Update(cartHeader);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> RemoveFromCart(long cartDetailsId)
        {
            try
            {
                var cartDetail = _context.cartDetails.FirstOrDefault(x => x.Id == cartDetailsId);
                var total = _context.cartDetails.Where(x => x.cartHeaderId == cartDetail.cartHeaderId).Count();
                _context.cartDetails.Remove(cartDetail);
                if(total == 1)
                {
                    var cartHeaderRemover = await _context.cartHeaders.FirstOrDefaultAsync(x => x.Id == cartDetail.cartHeaderId);
                    _context.cartHeaders.Remove(cartHeaderRemover);
                }

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<CartVO> SaveOrUpdateCart(CartVO cartVo)
        {
            Cart cart = _mapper.Map<Cart>(cartVo);
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == cartVo.cartDetails.FirstOrDefault().productId);
            if (product == null)
            {
                await _context.Products.AddAsync(cart.cartDetails.FirstOrDefault().product);
                await _context.SaveChangesAsync();
            }

            var cartHeaders = await _context.cartHeaders.AsNoTracking().FirstOrDefaultAsync(c => c.userId == cart.cartHeader.userId);
            if (cartHeaders == null)
            {
                await _context.cartHeaders.AddAsync(cart.cartHeader);
                await _context.SaveChangesAsync();
                cart.cartDetails.FirstOrDefault().cartHeaderId = cart.cartHeader.Id;
                cart.cartDetails.FirstOrDefault().product = null;
                await _context.cartDetails.AddAsync(cart.cartDetails.FirstOrDefault());
                await _context.SaveChangesAsync();

            }
            else
            {
                var cardDetails = await _context.cartDetails.AsNoTracking().FirstOrDefaultAsync(
                        p => p.productId == cart.cartDetails.FirstOrDefault().productId && p.cartHeaderId == cartHeaders.Id);
                if (cardDetails == null)
                {
                    cart.cartDetails.FirstOrDefault().cartHeaderId = cartHeaders.Id;
                    cart.cartDetails.FirstOrDefault().product = null;
                    await _context.cartDetails.AddAsync(cart.cartDetails.FirstOrDefault());
                    await _context.SaveChangesAsync();
                }
                else
                {
                    cart.cartDetails.FirstOrDefault().product = null;
                    cart.cartDetails.FirstOrDefault().count += cardDetails.count;
                    cart.cartDetails.FirstOrDefault().Id = cardDetails.Id;
                    cart.cartDetails.FirstOrDefault().cartHeaderId = cardDetails.cartHeaderId;
                     _context.cartDetails.Update(cart.cartDetails.FirstOrDefault());
                    await _context.SaveChangesAsync();

                }
            }
            return _mapper.Map<CartVO>(cart);

        }
    }
}
