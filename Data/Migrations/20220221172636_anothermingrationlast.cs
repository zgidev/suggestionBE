using Microsoft.EntityFrameworkCore.Migrations;

namespace StaffSuggestAPI.Data.Migrations
{
    public partial class anothermingrationlast : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastDepartment",
                table: "Complaints",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastDepartment",
                table: "Complaints");
        }
    }
}
