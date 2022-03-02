using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoppingTrackAPI.Migrations
{
    public partial class UpdateNamingAndProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PRIMARY",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stores",
                table: "Stores");

            migrationBuilder.DropPrimaryKey(
                name: "PRIMARY",
                table: "Items");

            migrationBuilder.DeleteData(
                table: "Stores",
                keyColumn: "StoreId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Stores",
                keyColumn: "StoreId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Stores",
                keyColumn: "StoreId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Stores",
                keyColumn: "StoreId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Stores",
                keyColumn: "StoreId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Stores",
                keyColumn: "StoreId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Stores",
                keyColumn: "StoreId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Stores",
                keyColumn: "StoreId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Stores",
                keyColumn: "StoreId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Stores",
                keyColumn: "StoreId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Stores",
                keyColumn: "StoreId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Stores",
                keyColumn: "StoreId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Stores",
                keyColumn: "StoreId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Stores",
                keyColumn: "StoreId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Stores",
                keyColumn: "StoreId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Stores",
                keyColumn: "StoreId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Stores",
                keyColumn: "StoreId",
                keyValue: 17);

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "User");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "item_id",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "deleted",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "User",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "ErrorLog",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "User",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "char(128)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "User",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "User",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "User",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Stores",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Stores",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Stores",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Stores",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Prices",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int(11)");

            migrationBuilder.AlterColumn<Guid>(
                name: "StoreId",
                table: "Prices",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int(11)");

            migrationBuilder.AlterColumn<Guid>(
                name: "ItemId",
                table: "Prices",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int(11)");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Prices",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int(11)")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Prices",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Prices",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Prices",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "user_id",
                table: "Items",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int(11)");

            migrationBuilder.AlterColumn<Guid>(
                name: "last_store_id",
                table: "Items",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int(11)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "currentStoreId",
                table: "Items",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int(11)");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Items",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Items",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Items",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Items",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "ErrorLog",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int(11)")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "ErrorLog",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ErrorLog",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "ErrorLog",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stores",
                table: "Stores",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Items",
                table: "Items",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Stores",
                columns: new[] { "Id", "CreatedDate", "IsDeleted", "ModifiedDate", "Name" },
                values: new object[,]
                {
                    { new Guid("f19b1ffb-cfc1-40cf-9b71-5a9b702e0001"), new DateTime(2022, 2, 28, 0, 46, 41, 835, DateTimeKind.Utc).AddTicks(2197), false, null, "Kroger" },
                    { new Guid("f19b1ffb-cfc1-40cf-9b71-5a9b702e0015"), new DateTime(2022, 2, 28, 0, 46, 41, 835, DateTimeKind.Utc).AddTicks(5451), false, null, "ALDI" },
                    { new Guid("f19b1ffb-cfc1-40cf-9b71-5a9b702e0014"), new DateTime(2022, 2, 28, 0, 46, 41, 835, DateTimeKind.Utc).AddTicks(5409), false, null, "Sams Club" },
                    { new Guid("f19b1ffb-cfc1-40cf-9b71-5a9b702e0013"), new DateTime(2022, 2, 28, 0, 46, 41, 835, DateTimeKind.Utc).AddTicks(5368), false, null, "Costco" },
                    { new Guid("f19b1ffb-cfc1-40cf-9b71-5a9b702e0012"), new DateTime(2022, 2, 28, 0, 46, 41, 835, DateTimeKind.Utc).AddTicks(5326), false, null, "Food Lion" },
                    { new Guid("f19b1ffb-cfc1-40cf-9b71-5a9b702e0011"), new DateTime(2022, 2, 28, 0, 46, 41, 835, DateTimeKind.Utc).AddTicks(5284), false, null, "H-E-B" },
                    { new Guid("f19b1ffb-cfc1-40cf-9b71-5a9b702e0010"), new DateTime(2022, 2, 28, 0, 46, 41, 835, DateTimeKind.Utc).AddTicks(5243), false, null, "Schnucks" },
                    { new Guid("f19b1ffb-cfc1-40cf-9b71-5a9b702e0016"), new DateTime(2022, 2, 28, 0, 46, 41, 835, DateTimeKind.Utc).AddTicks(5492), false, null, "Save A Lot" },
                    { new Guid("f19b1ffb-cfc1-40cf-9b71-5a9b702e0009"), new DateTime(2022, 2, 28, 0, 46, 41, 835, DateTimeKind.Utc).AddTicks(5201), false, null, "Giant Eagle" },
                    { new Guid("f19b1ffb-cfc1-40cf-9b71-5a9b702e0007"), new DateTime(2022, 2, 28, 0, 46, 41, 835, DateTimeKind.Utc).AddTicks(5115), false, null, "Dollar General" },
                    { new Guid("f19b1ffb-cfc1-40cf-9b71-5a9b702e0006"), new DateTime(2022, 2, 28, 0, 46, 41, 835, DateTimeKind.Utc).AddTicks(5073), false, null, "Walmart" },
                    { new Guid("f19b1ffb-cfc1-40cf-9b71-5a9b702e0005"), new DateTime(2022, 2, 28, 0, 46, 41, 835, DateTimeKind.Utc).AddTicks(5031), false, null, "Trader Joes" },
                    { new Guid("f19b1ffb-cfc1-40cf-9b71-5a9b702e0004"), new DateTime(2022, 2, 28, 0, 46, 41, 835, DateTimeKind.Utc).AddTicks(4989), false, null, "Whole Foods" },
                    { new Guid("f19b1ffb-cfc1-40cf-9b71-5a9b702e0003"), new DateTime(2022, 2, 28, 0, 46, 41, 835, DateTimeKind.Utc).AddTicks(4942), false, null, "Amazon" },
                    { new Guid("f19b1ffb-cfc1-40cf-9b71-5a9b702e0002"), new DateTime(2022, 2, 28, 0, 46, 41, 835, DateTimeKind.Utc).AddTicks(4855), false, null, "Publix" },
                    { new Guid("f19b1ffb-cfc1-40cf-9b71-5a9b702e0008"), new DateTime(2022, 2, 28, 0, 46, 41, 835, DateTimeKind.Utc).AddTicks(5157), false, null, "Pick n Save" },
                    { new Guid("f19b1ffb-cfc1-40cf-9b71-5a9b702e0017"), new DateTime(2022, 2, 28, 0, 46, 41, 835, DateTimeKind.Utc).AddTicks(5534), false, null, "Orbit Health" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stores",
                table: "Stores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Items",
                table: "Items");

            migrationBuilder.DeleteData(
                table: "Stores",
                keyColumn: "Id",
                keyValue: new Guid("f19b1ffb-cfc1-40cf-9b71-5a9b702e0001"));

            migrationBuilder.DeleteData(
                table: "Stores",
                keyColumn: "Id",
                keyValue: new Guid("f19b1ffb-cfc1-40cf-9b71-5a9b702e0002"));

            migrationBuilder.DeleteData(
                table: "Stores",
                keyColumn: "Id",
                keyValue: new Guid("f19b1ffb-cfc1-40cf-9b71-5a9b702e0003"));

            migrationBuilder.DeleteData(
                table: "Stores",
                keyColumn: "Id",
                keyValue: new Guid("f19b1ffb-cfc1-40cf-9b71-5a9b702e0004"));

            migrationBuilder.DeleteData(
                table: "Stores",
                keyColumn: "Id",
                keyValue: new Guid("f19b1ffb-cfc1-40cf-9b71-5a9b702e0005"));

            migrationBuilder.DeleteData(
                table: "Stores",
                keyColumn: "Id",
                keyValue: new Guid("f19b1ffb-cfc1-40cf-9b71-5a9b702e0006"));

            migrationBuilder.DeleteData(
                table: "Stores",
                keyColumn: "Id",
                keyValue: new Guid("f19b1ffb-cfc1-40cf-9b71-5a9b702e0007"));

            migrationBuilder.DeleteData(
                table: "Stores",
                keyColumn: "Id",
                keyValue: new Guid("f19b1ffb-cfc1-40cf-9b71-5a9b702e0008"));

            migrationBuilder.DeleteData(
                table: "Stores",
                keyColumn: "Id",
                keyValue: new Guid("f19b1ffb-cfc1-40cf-9b71-5a9b702e0009"));

            migrationBuilder.DeleteData(
                table: "Stores",
                keyColumn: "Id",
                keyValue: new Guid("f19b1ffb-cfc1-40cf-9b71-5a9b702e0010"));

            migrationBuilder.DeleteData(
                table: "Stores",
                keyColumn: "Id",
                keyValue: new Guid("f19b1ffb-cfc1-40cf-9b71-5a9b702e0011"));

            migrationBuilder.DeleteData(
                table: "Stores",
                keyColumn: "Id",
                keyValue: new Guid("f19b1ffb-cfc1-40cf-9b71-5a9b702e0012"));

            migrationBuilder.DeleteData(
                table: "Stores",
                keyColumn: "Id",
                keyValue: new Guid("f19b1ffb-cfc1-40cf-9b71-5a9b702e0013"));

            migrationBuilder.DeleteData(
                table: "Stores",
                keyColumn: "Id",
                keyValue: new Guid("f19b1ffb-cfc1-40cf-9b71-5a9b702e0014"));

            migrationBuilder.DeleteData(
                table: "Stores",
                keyColumn: "Id",
                keyValue: new Guid("f19b1ffb-cfc1-40cf-9b71-5a9b702e0015"));

            migrationBuilder.DeleteData(
                table: "Stores",
                keyColumn: "Id",
                keyValue: new Guid("f19b1ffb-cfc1-40cf-9b71-5a9b702e0016"));

            migrationBuilder.DeleteData(
                table: "Stores",
                keyColumn: "Id",
                keyValue: new Guid("f19b1ffb-cfc1-40cf-9b71-5a9b702e0017"));

            migrationBuilder.DropColumn(
                name: "Id",
                table: "User");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "User");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Prices");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Prices");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Prices");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "ErrorLog");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ErrorLog");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "ErrorLog");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "User",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ErrorLog",
                newName: "id");

            migrationBuilder.AlterColumn<string>(
                name: "password",
                table: "User",
                type: "char(128)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "user_id",
                table: "User",
                type: "int(11)",
                nullable: false,
                defaultValue: 0)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "Stores",
                type: "int(11)",
                nullable: false,
                defaultValue: 0)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Prices",
                type: "int(11)",
                nullable: false,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<int>(
                name: "StoreId",
                table: "Prices",
                type: "int(11)",
                nullable: false,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<int>(
                name: "ItemId",
                table: "Prices",
                type: "int(11)",
                nullable: false,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Prices",
                type: "int(11)",
                nullable: false,
                oldClrType: typeof(Guid))
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<int>(
                name: "user_id",
                table: "Items",
                type: "int(11)",
                nullable: false,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<int>(
                name: "last_store_id",
                table: "Items",
                type: "int(11)",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<int>(
                name: "currentStoreId",
                table: "Items",
                type: "int(11)",
                nullable: false,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<int>(
                name: "item_id",
                table: "Items",
                type: "int(11)",
                nullable: false,
                defaultValue: 0)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<ulong>(
                name: "deleted",
                table: "Items",
                type: "bit(1)",
                nullable: false,
                defaultValue: 0ul);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "ErrorLog",
                type: "int(11)",
                nullable: false,
                oldClrType: typeof(Guid))
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PRIMARY",
                table: "User",
                column: "user_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stores",
                table: "Stores",
                column: "StoreId");

            migrationBuilder.AddPrimaryKey(
                name: "PRIMARY",
                table: "Items",
                column: "item_id");

            migrationBuilder.InsertData(
                table: "Stores",
                columns: new[] { "StoreId", "Name" },
                values: new object[,]
                {
                    { 1, "Kroger" },
                    { 15, "ALDI" },
                    { 14, "Sams Club" },
                    { 13, "Costco" },
                    { 12, "Food Lion" },
                    { 11, "H-E-B" },
                    { 10, "Schnucks" },
                    { 16, "Save A Lot" },
                    { 9, "Giant Eagle" },
                    { 7, "Dollar General" },
                    { 6, "Walmart" },
                    { 5, "Trader Joes" },
                    { 4, "Whole Foods" },
                    { 3, "Amazon" },
                    { 2, "Publix" },
                    { 8, "Pick n Save" },
                    { 17, "Orbit Health" }
                });
        }
    }
}
