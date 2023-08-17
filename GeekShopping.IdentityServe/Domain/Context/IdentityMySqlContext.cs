using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.IdentityServe.Domain.Context
{
    public class IdentityMySqlContext : IdentityDbContext
    {
        public IdentityMySqlContext()
        {

        }
        public IdentityMySqlContext(DbContextOptions<IdentityMySqlContext> options) : base(options)
        {

        }

        public DbSet<ApplicationUser> user { get; set; }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Product>().HasData(new Product
        //    {
        //        Id = 2,
        //        name = "Seed Register name Product",
        //        description = "Seed Register description Product",
        //        categoryName = "Seed Register categoryName Product",
        //        price = 10,
        //        imageUrl = "http:localhost/imagem.jpg"
        //    });
        //    //base.OnModelCreating(modelBuilder); 
        //}
    }
}
