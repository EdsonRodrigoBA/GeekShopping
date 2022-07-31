using Microsoft.EntityFrameworkCore;

namespace GeekShopping.ProductAPI.Model.Context
{
    public class MySqlContext : DbContext
    {
        public MySqlContext()
        {

        }
        public MySqlContext(DbContextOptions<MySqlContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id =2,
                name="Seed Register name Product",
                description= "Seed Register description Product",
                categoryName= "Seed Register categoryName Product",
                 price = 10,
                 imageUrl = "http:localhost/imagem.jpg"
            });
            //base.OnModelCreating(modelBuilder); 
        }
    }
}
