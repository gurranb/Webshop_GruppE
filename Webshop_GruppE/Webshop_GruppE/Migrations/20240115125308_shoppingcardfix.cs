using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Webshop_GruppE.Migrations
{
    /// <inheritdoc />
    public partial class shoppingcardfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_ShoppingCarts_ShoppingCartId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ShoppingCarts_ShoppingCartId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ShoppingCartId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Customers_ShoppingCartId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "TotalCost",
                table: "ShoppingCarts");

            migrationBuilder.DropColumn(
                name: "ShoppingCartId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "test123",
                table: "ShoppingCarts",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "ShoppingCarts",
                newName: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "ShoppingCarts",
                newName: "test123");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "ShoppingCarts",
                newName: "Quantity");

            migrationBuilder.AddColumn<float>(
                name: "TotalCost",
                table: "ShoppingCarts",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ShoppingCartId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ShoppingCartId",
                table: "Products",
                column: "ShoppingCartId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_ShoppingCartId",
                table: "Customers",
                column: "ShoppingCartId",
                unique: true,
                filter: "[ShoppingCartId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_ShoppingCarts_ShoppingCartId",
                table: "Customers",
                column: "ShoppingCartId",
                principalTable: "ShoppingCarts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ShoppingCarts_ShoppingCartId",
                table: "Products",
                column: "ShoppingCartId",
                principalTable: "ShoppingCarts",
                principalColumn: "Id");
        }
    }
}
