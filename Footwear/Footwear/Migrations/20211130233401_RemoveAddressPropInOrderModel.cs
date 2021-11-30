using Microsoft.EntityFrameworkCore.Migrations;

namespace Footwear.Migrations
{
    public partial class RemoveAddressPropInOrderModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillingInformation_Orders_OrderId",
                table: "BillingInformation");

            migrationBuilder.DropIndex(
                name: "IX_BillingInformation_OrderId",
                table: "BillingInformation");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "BillingInformation");

            migrationBuilder.AddColumn<int>(
                name: "UserDataId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserDataId",
                table: "Orders",
                column: "UserDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_BillingInformation_UserDataId",
                table: "Orders",
                column: "UserDataId",
                principalTable: "BillingInformation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_BillingInformation_UserDataId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_UserDataId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "UserDataId",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OrderId",
                table: "BillingInformation",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BillingInformation_OrderId",
                table: "BillingInformation",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_BillingInformation_Orders_OrderId",
                table: "BillingInformation",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
