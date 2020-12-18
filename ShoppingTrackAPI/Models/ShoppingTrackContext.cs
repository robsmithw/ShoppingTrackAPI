using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using ShoppingTrackAPI.Models;

namespace ShoppingTrackAPI.Models
{
    public partial class ShoppingTrackContext : DbContext
    {
        public ShoppingTrackContext()
        {
        }

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
