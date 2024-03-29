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

        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<ErrorLog> ErrorLog { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Price> Prices { get; set; }

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
            modelBuilder.Entity<Store>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasColumnName("Name")
                    .HasColumnType("varchar(256)");

                entity.HasData(
                    new Store
                    {
                        Id = new Guid("f19b1ffb-cfc1-40cf-9b71-5a9b702e0001"),
                        Name = "Kroger"
                    },
                    new Store
                    {
                        Id = new Guid("f19b1ffb-cfc1-40cf-9b71-5a9b702e0002"),
                        Name = "Publix"
                    },
                    new Store
                    {
                        Id = new Guid("f19b1ffb-cfc1-40cf-9b71-5a9b702e0003"),
                        Name = "Amazon"
                    },
                    new Store
                    {
                        Id = new Guid("f19b1ffb-cfc1-40cf-9b71-5a9b702e0004"),
                        Name = "Whole Foods"
                    },
                    new Store
                    {
                        Id = new Guid("f19b1ffb-cfc1-40cf-9b71-5a9b702e0005"),
                        Name = "Trader Joes"
                    },
                    new Store
                    {
                        Id = new Guid("f19b1ffb-cfc1-40cf-9b71-5a9b702e0006"),
                        Name = "Walmart"
                    },
                    new Store
                    {
                        Id = new Guid("f19b1ffb-cfc1-40cf-9b71-5a9b702e0007"),
                        Name = "Dollar General"
                    },
                    new Store
                    {
                        Id = new Guid("f19b1ffb-cfc1-40cf-9b71-5a9b702e0008"),
                        Name = "Pick n Save"
                    },
                    new Store
                    {
                        Id = new Guid("f19b1ffb-cfc1-40cf-9b71-5a9b702e0009"),
                        Name = "Giant Eagle"
                    },
                    new Store
                    {
                        Id = new Guid("f19b1ffb-cfc1-40cf-9b71-5a9b702e0010"),
                        Name = "Schnucks"
                    },
                    new Store
                    {
                        Id = new Guid("f19b1ffb-cfc1-40cf-9b71-5a9b702e0011"),
                        Name = "H-E-B"
                    },
                    new Store
                    {
                        Id = new Guid("f19b1ffb-cfc1-40cf-9b71-5a9b702e0012"),
                        Name = "Food Lion"
                    },
                    new Store
                    {
                        Id = new Guid("f19b1ffb-cfc1-40cf-9b71-5a9b702e0013"),
                        Name = "Costco"
                    },
                    new Store
                    {
                        Id = new Guid("f19b1ffb-cfc1-40cf-9b71-5a9b702e0014"),
                        Name = "Sams Club"
                    },
                    new Store
                    {
                        Id = new Guid("f19b1ffb-cfc1-40cf-9b71-5a9b702e0015"),
                        Name = "ALDI"
                    },
                    new Store
                    {
                        Id = new Guid("f19b1ffb-cfc1-40cf-9b71-5a9b702e0016"),
                        Name = "Save A Lot"
                    },
                    new Store
                    {
                        Id = new Guid("f19b1ffb-cfc1-40cf-9b71-5a9b702e0017"),
                        Name = "Orbit Health"
                    }
                );
            });

            modelBuilder.Entity<Price>(entity =>
            {
                entity.Property(e => e.ItemId)
                    .HasColumnName("ItemId");

                entity.Property(e => e.UserId)
                    .HasColumnName("UserId");

                entity.Property(e => e.CurrentPrice)
                    .HasColumnName("Price")
                    .HasColumnType("decimal(10,2)");

                entity.Property(e => e.StoreId)
                    .HasColumnName("StoreId");

                entity.Property(e => e.DateOfPrice)
                    .HasColumnName("DateOfPrice")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<ErrorLog>(entity =>
            {
                entity.Property(e => e.CallStack)
                    .IsRequired()
                    .HasColumnName("call_stack")
                    .HasColumnType("varchar(1000)");

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasColumnName("location")
                    .HasColumnType("varchar(250)");
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.Property(e => e.LastStoreId)
                    .HasColumnName("last_store_id");

                entity.Property(e => e.CurrentStoreId)
                    .HasColumnName("currentStoreId");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(250)");

                entity.Property(e => e.PreviousPrice)
                    .HasColumnName("previous_price")
                    .HasColumnType("decimal(10,2)");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnName("user_id");

                entity.Property(e => e.Purchased)
                    .IsRequired()
                    .HasColumnName("purchased")
                    .HasColumnType("bit(1)");
            });

            modelBuilder.Entity<User>(entity =>
            {

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasColumnType("varchar(16)");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasColumnType("varchar(256)");

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
    }
}
