using Microsoft.EntityFrameworkCore.Migrations;

namespace CartCraft.Infrastructure.Migrations
{
    public partial class UpdatedProductsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductDetails_ProductDetailId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductDetailId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductDetailId",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "ProductId",
                table: "ProductDetails",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductDetails_ProductId",
                table: "ProductDetails",
                column: "ProductId",
                unique: true,
                filter: "[ProductId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductDetails_Products_ProductId",
                table: "ProductDetails",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductDetails_Products_ProductId",
                table: "ProductDetails");

            migrationBuilder.DropIndex(
                name: "IX_ProductDetails_ProductId",
                table: "ProductDetails");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductDetails");

            migrationBuilder.AddColumn<string>(
                name: "ProductDetailId",
                table: "Products",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductDetailId",
                table: "Products",
                column: "ProductDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductDetails_ProductDetailId",
                table: "Products",
                column: "ProductDetailId",
                principalTable: "ProductDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
