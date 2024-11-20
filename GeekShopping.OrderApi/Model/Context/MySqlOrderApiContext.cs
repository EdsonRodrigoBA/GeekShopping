using GeekShopping.OrderApi.Model;

using Microsoft.EntityFrameworkCore;

namespace GeekShopping.OrderApi.Models.Context
{
    public class MySqlOrderApiContext : DbContext
    {
        public MySqlOrderApiContext()
        {

        }
        public MySqlOrderApiContext (DbContextOptions<MySqlOrderApiContext> options) : base(options)
        {

        }

        public DbSet<OrderDetail> orderDetails { get; set; }
        public DbSet<OrderHeader> orderHeaders { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //base.OnModelCreating(modelBuilder); 
        }
    }
}
