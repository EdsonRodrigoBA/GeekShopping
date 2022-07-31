using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeekShopping.ProductAPI.Migrations
{
    public partial class Add_SeedProduct_Data_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "categoryName", "description", "imageUrl", "name", "price" },
                values: new object[] { 2L, "Seed Register categoryName Product", "Seed Register description Product", "http:localhost/imagem.jpg", "Seed Register name Product", 10m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 2L);
        }
    }
}
