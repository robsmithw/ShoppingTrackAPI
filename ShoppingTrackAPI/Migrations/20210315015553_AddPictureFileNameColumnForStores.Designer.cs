﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShoppingTrackAPI.Models;

namespace ShoppingTrackAPI.Migrations
{
    [DbContext(typeof(ShoppingTrackContext))]
    [Migration("20210315015553_AddPictureFileNameColumnForStores")]
    partial class AddPictureFileNameColumnForStores
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ShoppingTrackAPI.Models.ErrorLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int(11)");

                    b.Property<string>("CallStack")
                        .IsRequired()
                        .HasColumnName("call_stack")
                        .HasColumnType("varchar(1000)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnName("location")
                        .HasColumnType("varchar(250)");

                    b.HasKey("Id");

                    b.ToTable("ErrorLog");
                });

            modelBuilder.Entity("ShoppingTrackAPI.Models.Items", b =>
                {
                    b.Property<int>("ItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("item_id")
                        .HasColumnType("int(11)");

                    b.Property<int>("CurrentStoreId")
                        .HasColumnName("currentStoreId")
                        .HasColumnType("int(11)");

                    b.Property<ulong>("Deleted")
                        .HasColumnName("deleted")
                        .HasColumnType("bit(1)");

                    b.Property<int?>("Last_Store_Id")
                        .HasColumnName("last_store_id")
                        .HasColumnType("int(11)");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("varchar(250)");

                    b.Property<decimal?>("Previous_Price")
                        .HasColumnName("previous_price")
                        .HasColumnType("decimal(10,2)");

                    b.Property<ulong>("Purchased")
                        .HasColumnName("purchased")
                        .HasColumnType("bit(1)");

                    b.Property<int>("User_Id")
                        .HasColumnName("user_id")
                        .HasColumnType("int(11)");

                    b.HasKey("ItemId")
                        .HasName("PRIMARY");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("ShoppingTrackAPI.Models.Prices", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("int(11)");

                    b.Property<DateTime>("DateOfPrice")
                        .HasColumnName("DateOfPrice")
                        .HasColumnType("datetime");

                    b.Property<int>("ItemId")
                        .HasColumnName("ItemId")
                        .HasColumnType("int(11)");

                    b.Property<decimal>("Price")
                        .HasColumnName("Price")
                        .HasColumnType("decimal(10,2)");

                    b.Property<int>("StoreId")
                        .HasColumnName("StoreId")
                        .HasColumnType("int(11)");

                    b.Property<int>("UserId")
                        .HasColumnName("UserId")
                        .HasColumnType("int(11)");

                    b.HasKey("Id");

                    b.ToTable("Prices");
                });

            modelBuilder.Entity("ShoppingTrackAPI.Models.Stores", b =>
                {
                    b.Property<int>("StoreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("StoreId")
                        .HasColumnType("int(11)");

                    b.Property<string>("Name")
                        .HasColumnName("Name")
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PictureFileName")
                        .HasColumnName("PictureFileName")
                        .HasColumnType("varchar(50)");

                    b.HasKey("StoreId");

                    b.ToTable("Stores");
                });

            modelBuilder.Entity("ShoppingTrackAPI.Models.User", b =>
                {
                    b.Property<int>("User_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("user_id")
                        .HasColumnType("int(11)");

                    b.Property<ulong>("Admin")
                        .HasColumnName("admin")
                        .HasColumnType("bit(1)");

                    b.Property<string>("Email")
                        .HasColumnName("email")
                        .HasColumnType("varchar(256)");

                    b.Property<string>("Password")
                        .HasColumnName("password")
                        .HasColumnType("char(128)");

                    b.Property<string>("Username")
                        .HasColumnName("username")
                        .HasColumnType("varchar(16)");

                    b.Property<ulong>("Validated")
                        .HasColumnName("validated")
                        .HasColumnType("bit(1)");

                    b.HasKey("User_Id")
                        .HasName("PRIMARY");

                    b.ToTable("User");
                });
#pragma warning restore 612, 618
        }
    }
}
