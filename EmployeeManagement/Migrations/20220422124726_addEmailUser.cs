using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeManagement.Migrations
{
    public partial class addEmailUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Produits_Produitsid",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_Produitsid",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Produitsid",
                table: "Employees");

            migrationBuilder.AddColumn<string>(
                name: "EmailUserId",
                table: "Employees",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_EmailUserId",
                table: "Employees",
                column: "EmailUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_AspNetUsers_EmailUserId",
                table: "Employees",
                column: "EmailUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_AspNetUsers_EmailUserId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_EmailUserId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "EmailUserId",
                table: "Employees");

            migrationBuilder.AddColumn<int>(
                name: "Produitsid",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Produitsid",
                table: "Employees",
                column: "Produitsid");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Produits_Produitsid",
                table: "Employees",
                column: "Produitsid",
                principalTable: "Produits",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
