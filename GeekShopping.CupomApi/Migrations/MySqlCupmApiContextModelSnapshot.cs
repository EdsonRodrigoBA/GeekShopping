﻿// <auto-generated />
using GeekShopping.CupomApi.Models.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GeekShopping.CupomApi.Migrations
{
    [DbContext(typeof(MySqlCupmApiContext))]
    partial class MySqlCupmApiContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("GeekShopping.CupomApi.Model.Coupon", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("Id");

                    b.Property<string>("couponCode")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<decimal>("discountAmount")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.HasKey("Id");

                    b.ToTable("coupon");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            couponCode = "CUPOM123",
                            discountAmount = 10m,
                            name = "Dez % de desconto"
                        },
                        new
                        {
                            Id = 2L,
                            couponCode = "CUPOM1234",
                            discountAmount = 20m,
                            name = "Vinte % de desconto"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}