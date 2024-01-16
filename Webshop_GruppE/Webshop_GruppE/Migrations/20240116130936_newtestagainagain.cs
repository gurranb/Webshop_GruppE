using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Webshop_GruppE.Migrations
{
    /// <inheritdoc />
    public partial class newtestagainagain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SelectTopDealItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    SelectTopDealItemId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SelectTopDealItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SelectTopDealItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SelectTopDealItems_SelectTopDealItems_SelectTopDealItemId",
                        column: x => x.SelectTopDealItemId,
                        principalTable: "SelectTopDealItems",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SelectTopDealItems_ProductId",
                table: "SelectTopDealItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SelectTopDealItems_SelectTopDealItemId",
                table: "SelectTopDealItems",
                column: "SelectTopDealItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SelectTopDealItems");
        }
    }
}
