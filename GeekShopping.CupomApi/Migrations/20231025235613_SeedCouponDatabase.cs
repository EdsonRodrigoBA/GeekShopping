using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeekShopping.CupomApi.Migrations
{
    public partial class SeedCouponDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "coupon",
                columns: new[] { "Id", "couponCode", "discountAmount", "name" },
                values: new object[] { 1L, "CUPOM123", 10m, "Dez % de desconto" });

            migrationBuilder.InsertData(
                table: "coupon",
                columns: new[] { "Id", "couponCode", "discountAmount", "name" },
                values: new object[] { 2L, "CUPOM1234", 20m, "Vinte % de desconto" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "coupon",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "coupon",
                keyColumn: "Id",
                keyValue: 2L);
        }
    }
}
