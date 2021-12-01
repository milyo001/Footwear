using Microsoft.EntityFrameworkCore.Migrations;

namespace Footwear.Migrations
{
    public partial class RemoveEmailFromBillingInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "BillingInformation");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "BillingInformation",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
