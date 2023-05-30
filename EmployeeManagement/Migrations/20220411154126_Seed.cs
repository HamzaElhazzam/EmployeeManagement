using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeManagement.Migrations
{
    public partial class Seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                column: "Email",
                value: "YassineEnnajem68@gmail.com");

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Departement", "Email", "Name", "Photo" },
                values: new object[] { 2, 0, "SIMO68@gmail.com", "Noureddine ABABAR", "/images/emp.png" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                column: "Email",
                value: "YassineEnnajer68@gmail.com");
        }
    }
}
