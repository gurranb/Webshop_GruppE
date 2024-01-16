using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Webshop_GruppE.Migrations
{
    /// <inheritdoc />
    public partial class reaList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartItem_Products_productId",
                table: "ShoppingCartItem");

            migrationBuilder.RenameColumn(
                name: "productId",
                table: "ShoppingCartItem",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCartItem_productId",
                table: "ShoppingCartItem",
                newName: "IX_ShoppingCartItem_ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartItem_Products_ProductId",
                table: "ShoppingCartItem",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartItem_Products_ProductId",
                table: "ShoppingCartItem");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "ShoppingCartItem",
                newName: "productId");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCartItem_ProductId",
                table: "ShoppingCartItem",
                newName: "IX_ShoppingCartItem_productId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartItem_Products_productId",
                table: "ShoppingCartItem",
                column: "productId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
