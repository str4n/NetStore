﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetStore.Modules.Orders.Infrastructure.EF;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace NetStore.Modules.Orders.Infrastructure.EF.Migrations
{
    [DbContext(typeof(OrdersDbContext))]
    partial class OrdersDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("orders")
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("NetStore.Modules.Orders.Domain.Cart.Cart", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Carts", "orders");
                });

            modelBuilder.Entity("NetStore.Modules.Orders.Domain.Cart.CartProduct", b =>
                {
                    b.Property<Guid>("CartId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CheckoutCartId")
                        .HasColumnType("uuid");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.HasKey("CartId", "ProductId");

                    b.HasIndex("CheckoutCartId");

                    b.HasIndex("ProductId");

                    b.ToTable("CartProduct", "orders");
                });

            modelBuilder.Entity("NetStore.Modules.Orders.Domain.Cart.CheckoutCart", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uuid");

                    b.Property<string>("Payment")
                        .HasColumnType("text");

                    b.Property<string>("Shipment")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("CheckoutCarts", "orders");
                });

            modelBuilder.Entity("NetStore.Modules.Orders.Domain.Order.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uuid");

                    b.Property<string>("Payment")
                        .HasColumnType("text");

                    b.Property<DateTime>("PlaceDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Shipment")
                        .HasColumnType("text");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Version")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Orders", "orders");
                });

            modelBuilder.Entity("NetStore.Modules.Orders.Domain.Order.OrderLine", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("uuid");

                    b.Property<int>("OrderLineNumber")
                        .HasColumnType("integer");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<string>("SKU")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("UnitPrice")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderLine", "orders");
                });

            modelBuilder.Entity("NetStore.Modules.Orders.Domain.Product.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Color")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<double>("Price")
                        .HasColumnType("double precision");

                    b.Property<string>("SKU")
                        .HasColumnType("text");

                    b.Property<string>("Size")
                        .HasColumnType("text");

                    b.Property<int>("Stock")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Products", "orders");
                });

            modelBuilder.Entity("NetStore.Modules.Orders.Domain.Cart.CartProduct", b =>
                {
                    b.HasOne("NetStore.Modules.Orders.Domain.Cart.Cart", null)
                        .WithMany("Products")
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NetStore.Modules.Orders.Domain.Cart.CheckoutCart", null)
                        .WithMany("Products")
                        .HasForeignKey("CheckoutCartId");

                    b.HasOne("NetStore.Modules.Orders.Domain.Product.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("NetStore.Modules.Orders.Domain.Order.OrderLine", b =>
                {
                    b.HasOne("NetStore.Modules.Orders.Domain.Order.Order", null)
                        .WithMany("Lines")
                        .HasForeignKey("OrderId");
                });

            modelBuilder.Entity("NetStore.Modules.Orders.Domain.Cart.Cart", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("NetStore.Modules.Orders.Domain.Cart.CheckoutCart", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("NetStore.Modules.Orders.Domain.Order.Order", b =>
                {
                    b.Navigation("Lines");
                });
#pragma warning restore 612, 618
        }
    }
}
