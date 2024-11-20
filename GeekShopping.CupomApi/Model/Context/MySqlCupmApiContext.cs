using GeekShopping.CupomApi.Model;

using Microsoft.EntityFrameworkCore;

namespace GeekShopping.CupomApi.Models.Context
{
    public class MySqlCupmApiContext : DbContext
    {
        public MySqlCupmApiContext()
        {

        }
        public MySqlCupmApiContext(DbContextOptions<MySqlCupmApiContext> options) : base(options)
        {

        }

        public DbSet<Coupon> Coupon { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Coupon>().HasData(new Coupon()
            {
                Id = 1,
                couponCode = "CUPOM123",
                discountAmount = 10,
                name = "Dez % de desconto"
            });

            modelBuilder.Entity<Coupon>().HasData(new Coupon()
            {
                Id = 2,
                couponCode = "CUPOM1234",
                discountAmount = 20,
                name = "Vinte % de desconto"
            });
        }
    }
}
