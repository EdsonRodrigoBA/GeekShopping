
using GeekShopping.OrderApi.Model;
using GeekShopping.OrderApi.Models.Context;

using Microsoft.EntityFrameworkCore;

namespace GeekShopping.OrderApi.Repository
{
    public class OrderRepository : IOrderRepository
    {

        private readonly DbContextOptions<MySqlOrderApiContext> _context;

        public OrderRepository(DbContextOptions<MySqlOrderApiContext> mySqlContext)
        {
            _context = mySqlContext;
        }

        public async Task<bool> AddOrder(OrderHeader orderHeader)
        {
            await using var _db = new MySqlOrderApiContext(_context);
            _db.orderHeaders.Add(orderHeader);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task UpdateOrderPaymentStatus(long orderHeraderId, bool paid)
        {
            await using var _db = new MySqlOrderApiContext(_context);
            var header = await _db.orderHeaders.FirstOrDefaultAsync(x => x.Id == orderHeraderId);
            if(header != null)
            {
                header.PaymentStatus = paid;
                _db.orderHeaders.Update(header);
                await _db.SaveChangesAsync();
            }
            
        }


    }
}
