using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoppingTrackAPI.Migrations
{
    public partial class AddPictureFileNameColumnForStores : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PictureFileName",
                table: "Stores",
                type: "varchar(50)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PictureFileName",
                table: "Stores");
        }
    }
}
