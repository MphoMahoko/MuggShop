using Microsoft.EntityFrameworkCore.Migrations;

namespace MuggShop.Data.Migrations
{
    public partial class AddDetailsToOrdersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Orders",
                newName: "Province");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Orders",
                newName: "PostCode");

            migrationBuilder.AddColumn<string>(
                name: "Address1",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address2",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Orders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address1",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Address2",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "Province",
                table: "Orders",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "PostCode",
                table: "Orders",
                newName: "Address");
        }
    }
}
