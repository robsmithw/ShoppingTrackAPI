using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoppingTrackAPI.Migrations
{
    public partial class RemovePictureFileName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PictureFileName",
                table: "Stores");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PictureFileName",
                table: "Stores",
                type: "varchar(50)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Stores",
                keyColumn: "StoreId",
                keyValue: 1,
                column: "PictureFileName",
                value: "");

            migrationBuilder.UpdateData(
                table: "Stores",
                keyColumn: "StoreId",
                keyValue: 2,
                column: "PictureFileName",
                value: "");

            migrationBuilder.UpdateData(
                table: "Stores",
                keyColumn: "StoreId",
                keyValue: 3,
                column: "PictureFileName",
                value: "");

            migrationBuilder.UpdateData(
                table: "Stores",
                keyColumn: "StoreId",
                keyValue: 4,
                column: "PictureFileName",
                value: "");

            migrationBuilder.UpdateData(
                table: "Stores",
                keyColumn: "StoreId",
                keyValue: 5,
                column: "PictureFileName",
                value: "");

            migrationBuilder.UpdateData(
                table: "Stores",
                keyColumn: "StoreId",
                keyValue: 6,
                column: "PictureFileName",
                value: "");

            migrationBuilder.UpdateData(
                table: "Stores",
                keyColumn: "StoreId",
                keyValue: 7,
                column: "PictureFileName",
                value: "");

            migrationBuilder.UpdateData(
                table: "Stores",
                keyColumn: "StoreId",
                keyValue: 8,
                column: "PictureFileName",
                value: "");

            migrationBuilder.UpdateData(
                table: "Stores",
                keyColumn: "StoreId",
                keyValue: 9,
                column: "PictureFileName",
                value: "");

            migrationBuilder.UpdateData(
                table: "Stores",
                keyColumn: "StoreId",
                keyValue: 10,
                column: "PictureFileName",
                value: "");

            migrationBuilder.UpdateData(
                table: "Stores",
                keyColumn: "StoreId",
                keyValue: 11,
                column: "PictureFileName",
                value: "");

            migrationBuilder.UpdateData(
                table: "Stores",
                keyColumn: "StoreId",
                keyValue: 12,
                column: "PictureFileName",
                value: "");

            migrationBuilder.UpdateData(
                table: "Stores",
                keyColumn: "StoreId",
                keyValue: 13,
                column: "PictureFileName",
                value: "");

            migrationBuilder.UpdateData(
                table: "Stores",
                keyColumn: "StoreId",
                keyValue: 14,
                column: "PictureFileName",
                value: "");

            migrationBuilder.UpdateData(
                table: "Stores",
                keyColumn: "StoreId",
                keyValue: 15,
                column: "PictureFileName",
                value: "");

            migrationBuilder.UpdateData(
                table: "Stores",
                keyColumn: "StoreId",
                keyValue: 16,
                column: "PictureFileName",
                value: "");

            migrationBuilder.UpdateData(
                table: "Stores",
                keyColumn: "StoreId",
                keyValue: 17,
                column: "PictureFileName",
                value: "");
        }
    }
}
