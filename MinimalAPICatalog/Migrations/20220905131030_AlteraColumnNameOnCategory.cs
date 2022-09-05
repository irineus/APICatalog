using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MinimalAPICatalog.Migrations
{
    public partial class AlteraColumnNameOnCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CategoyId",
                table: "Categories",
                newName: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Categories",
                newName: "CategoyId");
        }
    }
}
