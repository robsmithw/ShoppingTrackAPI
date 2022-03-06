using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoppingTrackAPI.Migrations
{
    public partial class InitialCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ErrorLog",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    location = table.Column<string>(type: "varchar(250)", nullable: false),
                    call_stack = table.Column<string>(type: "varchar(1000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrorLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    user_id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(type: "varchar(250)", nullable: true),
                    previous_price = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    last_store_id = table.Column<Guid>(nullable: false),
                    currentStoreId = table.Column<Guid>(nullable: false),
                    purchased = table.Column<ulong>(type: "bit(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Prices",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ItemId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    StoreId = table.Column<Guid>(nullable: false),
                    DateOfPrice = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stores",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(type: "varchar(256)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    username = table.Column<string>(type: "varchar(16)", nullable: true),
                    email = table.Column<string>(type: "varchar(256)", nullable: true),
                    Password = table.Column<string>(nullable: true),
                    admin = table.Column<ulong>(type: "bit(1)", nullable: false),
                    validated = table.Column<ulong>(type: "bit(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Stores",
                columns: new[] { "Id", "CreatedDate", "IsDeleted", "ModifiedDate", "Name" },
                values: new object[,]
                {
                    { new Guid("f19b1ffb-cfc1-40cf-9b71-5a9b702e0001"), new DateTime(2022, 3, 2, 4, 9, 16, 117, DateTimeKind.Utc).AddTicks(4721), false, null, "Kroger" },
                    { new Guid("f19b1ffb-cfc1-40cf-9b71-5a9b702e0015"), new DateTime(2022, 3, 2, 4, 9, 16, 117, DateTimeKind.Utc).AddTicks(7868), false, null, "ALDI" },
                    { new Guid("f19b1ffb-cfc1-40cf-9b71-5a9b702e0014"), new DateTime(2022, 3, 2, 4, 9, 16, 117, DateTimeKind.Utc).AddTicks(7828), false, null, "Sams Club" },
                    { new Guid("f19b1ffb-cfc1-40cf-9b71-5a9b702e0013"), new DateTime(2022, 3, 2, 4, 9, 16, 117, DateTimeKind.Utc).AddTicks(7787), false, null, "Costco" },
                    { new Guid("f19b1ffb-cfc1-40cf-9b71-5a9b702e0012"), new DateTime(2022, 3, 2, 4, 9, 16, 117, DateTimeKind.Utc).AddTicks(7747), false, null, "Food Lion" },
                    { new Guid("f19b1ffb-cfc1-40cf-9b71-5a9b702e0011"), new DateTime(2022, 3, 2, 4, 9, 16, 117, DateTimeKind.Utc).AddTicks(7706), false, null, "H-E-B" },
                    { new Guid("f19b1ffb-cfc1-40cf-9b71-5a9b702e0010"), new DateTime(2022, 3, 2, 4, 9, 16, 117, DateTimeKind.Utc).AddTicks(7666), false, null, "Schnucks" },
                    { new Guid("f19b1ffb-cfc1-40cf-9b71-5a9b702e0016"), new DateTime(2022, 3, 2, 4, 9, 16, 117, DateTimeKind.Utc).AddTicks(7908), false, null, "Save A Lot" },
                    { new Guid("f19b1ffb-cfc1-40cf-9b71-5a9b702e0009"), new DateTime(2022, 3, 2, 4, 9, 16, 117, DateTimeKind.Utc).AddTicks(7626), false, null, "Giant Eagle" },
                    { new Guid("f19b1ffb-cfc1-40cf-9b71-5a9b702e0007"), new DateTime(2022, 3, 2, 4, 9, 16, 117, DateTimeKind.Utc).AddTicks(7542), false, null, "Dollar General" },
                    { new Guid("f19b1ffb-cfc1-40cf-9b71-5a9b702e0006"), new DateTime(2022, 3, 2, 4, 9, 16, 117, DateTimeKind.Utc).AddTicks(7501), false, null, "Walmart" },
                    { new Guid("f19b1ffb-cfc1-40cf-9b71-5a9b702e0005"), new DateTime(2022, 3, 2, 4, 9, 16, 117, DateTimeKind.Utc).AddTicks(7460), false, null, "Trader Joes" },
                    { new Guid("f19b1ffb-cfc1-40cf-9b71-5a9b702e0004"), new DateTime(2022, 3, 2, 4, 9, 16, 117, DateTimeKind.Utc).AddTicks(7418), false, null, "Whole Foods" },
                    { new Guid("f19b1ffb-cfc1-40cf-9b71-5a9b702e0003"), new DateTime(2022, 3, 2, 4, 9, 16, 117, DateTimeKind.Utc).AddTicks(7374), false, null, "Amazon" },
                    { new Guid("f19b1ffb-cfc1-40cf-9b71-5a9b702e0002"), new DateTime(2022, 3, 2, 4, 9, 16, 117, DateTimeKind.Utc).AddTicks(7291), false, null, "Publix" },
                    { new Guid("f19b1ffb-cfc1-40cf-9b71-5a9b702e0008"), new DateTime(2022, 3, 2, 4, 9, 16, 117, DateTimeKind.Utc).AddTicks(7585), false, null, "Pick n Save" },
                    { new Guid("f19b1ffb-cfc1-40cf-9b71-5a9b702e0017"), new DateTime(2022, 3, 2, 4, 9, 16, 117, DateTimeKind.Utc).AddTicks(7949), false, null, "Orbit Health" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ErrorLog");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Prices");

            migrationBuilder.DropTable(
                name: "Stores");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
