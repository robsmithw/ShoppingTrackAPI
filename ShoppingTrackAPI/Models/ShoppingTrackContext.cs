using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

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
        private IConfiguration Configuration { get; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var baseConnString = Configuration.GetConnectionString("DefaultConnection");
                var userName = Configuration.GetValue<string>("Keys:UserId");
                var passwd = Configuration.GetValue<string>("Keys:Pass");
                var populatedConnString = String.Format(baseConnString, userName, passwd);
                optionsBuilder.UseMySql(populatedConnString);
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
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
