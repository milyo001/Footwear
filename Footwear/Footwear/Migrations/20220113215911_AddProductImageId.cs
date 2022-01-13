using Microsoft.EntityFrameworkCore.Migrations;

namespace Footwear.Migrations
{
    public partial class AddProductImageId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductsImage_ProductImageId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductImageId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductImageId",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ImageId",
                table: "Products",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductsImage_ImageId",
                table: "Products",
                column: "ImageId",
                principalTable: "ProductsImage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductsImage_ImageId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ImageId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "ProductImageId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductImageId",
                table: "Products",
                column: "ProductImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductsImage_ProductImageId",
                table: "Products",
                column: "ProductImageId",
                principalTable: "ProductsImage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
