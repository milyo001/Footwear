using Microsoft.EntityFrameworkCore.Migrations;

namespace Footwear.Migrations
{
    public partial class RemoveImageForKeyFromCartProductModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartProducts_ProductsImage_ImageId",
                table: "CartProducts");

            migrationBuilder.DropIndex(
                name: "IX_CartProducts_ImageId",
                table: "CartProducts");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "CartProducts");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "CartProducts",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "CartProducts");

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "CartProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CartProducts_ImageId",
                table: "CartProducts",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartProducts_ProductsImage_ImageId",
                table: "CartProducts",
                column: "ImageId",
                principalTable: "ProductsImage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
