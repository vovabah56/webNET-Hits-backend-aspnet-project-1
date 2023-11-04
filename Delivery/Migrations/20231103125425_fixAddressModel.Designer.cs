﻿// <auto-generated />
using System;
using Delivery.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Delivery.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231103125425_fixAddressModel")]
    partial class fixAddressModel
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Delivery.DB.Models.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("DeliveryTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("OrderTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double>("Price")
                        .HasColumnType("double precision");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Delivery.DB.Models.Token", b =>
                {
                    b.Property<string>("InvalidToken")
                        .HasColumnType("text");

                    b.Property<DateTime>("ExpiredDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("InvalidToken");

                    b.ToTable("Tokens");
                });

            modelBuilder.Entity("Delivery.Data.Models.Cart", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Count")
                        .HasColumnType("integer");

                    b.Property<Guid>("DishId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("DishId", "UserId", "OrderId")
                        .IsUnique();

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("Delivery.Data.Models.Dish", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsVegetarian")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Photo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Price")
                        .HasColumnType("double precision");

                    b.Property<double?>("Rating")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("Dishes");
                });

            modelBuilder.Entity("Delivery.Data.Models.Rating", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("DishId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<int>("Value")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("DishId", "UserId")
                        .IsUnique();

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("Delivery.Data.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Delivery.Data.Models.as_addr_obj", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long?>("ChangeId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("IsActive")
                        .HasColumnType("integer");

                    b.Property<int?>("IsActual")
                        .HasColumnType("integer");

                    b.Property<string>("Level")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<long?>("NextId")
                        .HasColumnType("bigint");

                    b.Property<Guid>("ObjectGuid")
                        .HasColumnType("uuid");

                    b.Property<long?>("ObjectId")
                        .HasColumnType("bigint");

                    b.Property<int?>("OperTypeId")
                        .HasColumnType("integer");

                    b.Property<long?>("PrevId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("TypeName")
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("AsAddrObjs");
                });

            modelBuilder.Entity("Delivery.Data.Models.as_adm_hierarchy", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("AreaCode")
                        .HasColumnType("text");

                    b.Property<long?>("ChangeId")
                        .HasColumnType("bigint");

                    b.Property<string>("CityCode")
                        .HasColumnType("text");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("IsActive")
                        .HasColumnType("integer");

                    b.Property<long?>("NextId")
                        .HasColumnType("bigint");

                    b.Property<long?>("ObjectId")
                        .HasColumnType("bigint");

                    b.Property<long?>("ParentObjectId")
                        .HasColumnType("bigint");

                    b.Property<string>("Path")
                        .HasColumnType("text");

                    b.Property<string>("PlaceCode")
                        .HasColumnType("text");

                    b.Property<string>("PlanCode")
                        .HasColumnType("text");

                    b.Property<long?>("PrevId")
                        .HasColumnType("bigint");

                    b.Property<string>("RegionCode")
                        .HasColumnType("text");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("StreetCode")
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("AsAdmHierarchies");
                });

            modelBuilder.Entity("Delivery.Data.Models.as_houses", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("AddNum1")
                        .HasColumnType("text");

                    b.Property<string>("AddNum2")
                        .HasColumnType("text");

                    b.Property<int?>("AddType1")
                        .HasColumnType("integer");

                    b.Property<int?>("AddType2")
                        .HasColumnType("integer");

                    b.Property<long?>("ChangeId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("HouseNumber")
                        .HasColumnType("text");

                    b.Property<int?>("HouseType")
                        .HasColumnType("integer");

                    b.Property<int?>("IsActive")
                        .HasColumnType("integer");

                    b.Property<int?>("IsActual")
                        .HasColumnType("integer");

                    b.Property<long?>("NextId")
                        .HasColumnType("bigint");

                    b.Property<Guid>("ObjectGuid")
                        .HasColumnType("uuid");

                    b.Property<long?>("ObjectId")
                        .HasColumnType("bigint");

                    b.Property<int?>("OperTypeId")
                        .HasColumnType("integer");

                    b.Property<long?>("PrevId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("AsHouses");
                });

            modelBuilder.Entity("Delivery.Data.Models.Cart", b =>
                {
                    b.HasOne("Delivery.Data.Models.Dish", null)
                        .WithMany("Carts")
                        .HasForeignKey("DishId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Delivery.DB.Models.Order", null)
                        .WithMany("Carts")
                        .HasForeignKey("OrderId");
                });

            modelBuilder.Entity("Delivery.Data.Models.Rating", b =>
                {
                    b.HasOne("Delivery.Data.Models.Dish", null)
                        .WithMany("Ratings")
                        .HasForeignKey("DishId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Delivery.DB.Models.Order", b =>
                {
                    b.Navigation("Carts");
                });

            modelBuilder.Entity("Delivery.Data.Models.Dish", b =>
                {
                    b.Navigation("Carts");

                    b.Navigation("Ratings");
                });
#pragma warning restore 612, 618
        }
    }
}