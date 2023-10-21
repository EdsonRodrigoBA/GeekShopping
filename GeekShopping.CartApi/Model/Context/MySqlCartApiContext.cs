using GeekShopping.CartApi.Model;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.CartApi.Models.Context
{
    public class MySqlCartApiContext : DbContext
    {
        public MySqlCartApiContext()
        {

        }
        public MySqlCartApiContext(DbContextOptions<MySqlCartApiContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<CartHeader> cartHeaders { get; set; }
        public DbSet<CartDetail> cartDetails { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //base.OnModelCreating(modelBuilder); 
        }
    }
}
