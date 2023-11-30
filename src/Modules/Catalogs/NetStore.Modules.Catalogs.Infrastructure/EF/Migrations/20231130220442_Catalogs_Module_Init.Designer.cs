﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetStore.Modules.Catalogs.Infrastructure.EF;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace NetStore.Modules.Catalogs.Infrastructure.EF.Migrations
{
    [DbContext(typeof(CatalogsDbContext))]
    [Migration("20231130220442_Catalogs_Module_Init")]
    partial class Catalogs_Module_Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("catalogs")
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("NetStore.Modules.Catalogs.Domain.Brand.Brand", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Brands", "catalogs");
                });

            modelBuilder.Entity("NetStore.Modules.Catalogs.Domain.Category.Category", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Code")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Categories", "catalogs");
                });

            modelBuilder.Entity("NetStore.Modules.Catalogs.Domain.Product.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<int>("AgeCategory")
                        .HasColumnType("integer");

                    b.Property<long?>("BrandId")
                        .HasColumnType("bigint");

                    b.Property<long?>("CategoryId")
                        .HasColumnType("bigint");

                    b.Property<int>("Color")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Fabric")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Gender")
                        .HasColumnType("integer");

                    b.Property<double>("GrossPrice")
                        .HasColumnType("double precision");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("NetPrice")
                        .HasColumnType("double precision");

                    b.Property<string>("SKU")
                        .HasColumnType("text");

                    b.Property<int>("Size")
                        .HasColumnType("integer");

                    b.Property<int>("Version")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("Name");

                    b.ToTable("Product", "catalogs");
                });

            modelBuilder.Entity("NetStore.Modules.Catalogs.Domain.Product.Product", b =>
                {
                    b.HasOne("NetStore.Modules.Catalogs.Domain.Brand.Brand", "Brand")
                        .WithMany()
                        .HasForeignKey("BrandId");

                    b.HasOne("NetStore.Modules.Catalogs.Domain.Category.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");

                    b.OwnsOne("NetStore.Modules.Catalogs.Domain.Product.ValueObjects.ProductWeight", "Weight", b1 =>
                        {
                            b1.Property<Guid>("ProductId")
                                .HasColumnType("uuid");

                            b1.HasKey("ProductId");

                            b1.ToTable("Product", "catalogs");

                            b1.WithOwner()
                                .HasForeignKey("ProductId");
                        });

                    b.Navigation("Brand");

                    b.Navigation("Category");

                    b.Navigation("Weight");
                });
#pragma warning restore 612, 618
        }
    }
}
