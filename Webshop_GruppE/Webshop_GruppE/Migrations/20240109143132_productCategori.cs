using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Webshop_GruppE.Migrations
{
    /// <inheritdoc />
    public partial class productCategori : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryProduct_Categories_CategoriesId",
                table: "CategoryProduct");

            migrationBuilder.RenameColumn(
                name: "CategoriesId",
                table: "CategoryProduct",
                newName: "CategoriId");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryProduct_Categories_CategoriId",
                table: "CategoryProduct",
                column: "CategoriId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryProduct_Categories_CategoriId",
                table: "CategoryProduct");

            migrationBuilder.RenameColumn(
                name: "CategoriId",
                table: "CategoryProduct",
                newName: "CategoriesId");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryProduct_Categories_CategoriesId",
                table: "CategoryProduct",
                column: "CategoriesId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
