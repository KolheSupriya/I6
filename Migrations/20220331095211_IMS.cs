using Microsoft.EntityFrameworkCore.Migrations;

namespace InventoryManagementSystem.Migrations
{
    public partial class IMS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PRODUCTS_PRODUCTS_Products_ID1",
                table: "PRODUCTS");

            migrationBuilder.DropIndex(
                name: "IX_PRODUCTS_Products_ID1",
                table: "PRODUCTS");

            migrationBuilder.DropColumn(
                name: "Products_ID1",
                table: "PRODUCTS");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Products_ID1",
                table: "PRODUCTS",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCTS_Products_ID1",
                table: "PRODUCTS",
                column: "Products_ID1");

            migrationBuilder.AddForeignKey(
                name: "FK_PRODUCTS_PRODUCTS_Products_ID1",
                table: "PRODUCTS",
                column: "Products_ID1",
                principalTable: "PRODUCTS",
                principalColumn: "Products_ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
