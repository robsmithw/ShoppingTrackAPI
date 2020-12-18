using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoppingTrackAPI.Migrations
{
    public partial class AddCurrentStoreIdToItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "currentStoreId",
                table: "Items",
                type: "int(11)",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "currentStoreId",
                table: "Items");
        }
    }
}
