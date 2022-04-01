using Microsoft.EntityFrameworkCore.Migrations;

namespace InventoryManagementSystem.Migrations
{
    public partial class Inventory11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_USERS_PRODUCTS_Product_Access",
                table: "USERS");

            migrationBuilder.AddForeignKey(
                name: "FK_USERS_PRODUCT_CATEGORIES_Product_Access",
                table: "USERS",
                column: "Product_Access",
                principalTable: "PRODUCT_CATEGORIES",
                principalColumn: "Product_Category_ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_USERS_PRODUCT_CATEGORIES_Product_Access",
                table: "USERS");

            migrationBuilder.AddForeignKey(
                name: "FK_USERS_PRODUCTS_Product_Access",
                table: "USERS",
                column: "Product_Access",
                principalTable: "PRODUCTS",
                principalColumn: "Products_ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
