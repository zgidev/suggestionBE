using Microsoft.EntityFrameworkCore.Migrations;

namespace StaffSuggestAPI.Data.Migrations
{
    public partial class addedgroupemail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GroupEmail",
                table: "Departments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GroupEmail",
                table: "Departments");
        }
    }
}
