using Microsoft.EntityFrameworkCore.Migrations;

namespace ChatsterApi.Migrations
{
    public partial class AddIsApprovedToPhoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Photo",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Photo");
        }
    }
}
