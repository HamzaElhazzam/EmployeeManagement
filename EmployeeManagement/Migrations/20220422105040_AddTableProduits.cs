using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeManagement.Migrations
{
    public partial class AddTableProduits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Produitsid",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Produits",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prix = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produits", x => x.id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Produits_Produitsid",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "Produits");

            migrationBuilder.DropIndex(
                name: "IX_Employees_Produitsid",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Produitsid",
                table: "Employees");
        }
    }
}
