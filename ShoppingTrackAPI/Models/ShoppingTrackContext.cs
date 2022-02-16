using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using ShoppingTrackAPI.Models;

namespace ShoppingTrackAPI.Models
{
    public partial class ShoppingTrackContext : DbContext
    {
        public ShoppingTrackContext(DbContextOptions<ShoppingTrackContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Stores> Stores { get; set; }
        public virtual DbSet<ErrorLog> ErrorLog { get; set; }
        public virtual DbSet<Items> Items { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                #if DEBUG
                optionsBuilder.UseMySql("server=db;port=3306;database=ShoppingTrack;user=root;password=Password1;CharSet=utf8");
                #elif RELEASE
                optionsBuilder.UseMySql("server=45.79.198.133;port=3306;database=ShoppingTrack;user=ShoppingTrackAPI;password=Password2~");
                #else
                optionsBuilder.UseMySql("server=db;port=3306;database=ShoppingTrack;user=root;password=Password1;CharSet=utf8");
                #endif
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Stores>(entity =>
            {
                entity.HasKey(e => e.StoreId);

                entity.Property(e => e.StoreId)
                    .HasColumnName("StoreId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .HasColumnName("Name")
                    .HasColumnType("varchar(256)");

                entity.HasData(
                    new Stores
                    {
                        StoreId = 1,
                        Name = "Kroger"
                    },
                    new Stores
                    {
                        StoreId = 2,
                        Name = "Publix"
                    },
                    new Stores
                    {
                        StoreId = 3,
                        Name = "Amazon"
                    },
                    new Stores
                    {
                        StoreId = 4,
                        Name = "Whole Foods"
                    },
                    new Stores
                    {
                        StoreId = 5,
                        Name = "Trader Joes"
                    },
                    new Stores
                    {
                        StoreId = 6,
                        Name = "Walmart"
                    },
                    new Stores
                    {
                        StoreId = 7,
                        Name = "Dollar General"
                    },
                    new Stores
                    {
                        StoreId = 8,
                        Name = "Pick n Save"
                    },
                    new Stores
                    {
                        StoreId = 9,
                        Name = "Giant Eagle"
                    },
                    new Stores
                    {
                        StoreId = 10,
                        Name = "Schnucks"
                    },
                    new Stores
                    {
                        StoreId = 11,
                        Name = "H-E-B"
                    },
                    new Stores
                    {
                        StoreId = 12,
                        Name = "Food Lion"
                    },
                    new Stores
                    {
                        StoreId = 13,
                        Name = "Costco"
                    },
                    new Stores
                    {
                        StoreId = 14,
                        Name = "Sams Club"
                    },
                    new Stores
                    {
                        StoreId = 15,
                        Name = "ALDI"
                    },
                    new Stores
                    {
                        StoreId = 16,
                        Name = "Save A Lot"
                    },
                    new Stores
                    {
                        StoreId = 17,
                        Name = "Orbit Health"
                    }
                );
            });

            modelBuilder.Entity<Prices>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("Id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ItemId)
                    .HasColumnName("ItemId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UserId)
                    .HasColumnName("UserId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Price)
                    .HasColumnName("Price")
                    .HasColumnType("decimal(10,2)");

                entity.Property(e => e.StoreId)
                    .HasColumnName("StoreId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DateOfPrice)
                    .HasColumnName("DateOfPrice")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<ErrorLog>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CallStack)
                    .IsRequired()
                    .HasColumnName("call_stack")
                    .HasColumnType("varchar(1000)");

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasColumnName("location")
                    .HasColumnType("varchar(250)");
            });

            modelBuilder.Entity<Items>(entity =>
            {
                entity.HasKey(e => e.ItemId)
                    .HasName("PRIMARY");

                entity.Property(e => e.ItemId)
                    .HasColumnName("item_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Last_Store_Id)
                    .HasColumnName("last_store_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CurrentStoreId)
                    .HasColumnName("currentStoreId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(250)");

                entity.Property(e => e.Previous_Price)
                    .HasColumnName("previous_price")
                    .HasColumnType("decimal(10,2)");

                entity.Property(e => e.User_Id)
                    .IsRequired()
                    .HasColumnName("user_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Deleted)
                    .IsRequired()
                    .HasColumnName("deleted")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.Purchased)
                    .IsRequired()
                    .HasColumnName("purchased")
                    .HasColumnType("bit(1)");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.User_Id)
                    .HasName("PRIMARY");

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasColumnType("varchar(16)");

                entity.Property(e => e.User_Id)
                    .HasColumnName("user_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasColumnType("varchar(256)");

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasColumnType("char(128)");

                entity.Property(e => e.Admin)
                    .HasColumnName("admin")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.Validated)
                    .HasColumnName("validated")
                    .HasColumnType("bit(1)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public DbSet<ShoppingTrackAPI.Models.Prices> Prices { get; set; }
    }
}
