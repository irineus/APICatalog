using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICatalog.Migrations
{
    public partial class SeedCategories : Migration
    {
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("Insert into categories(Name, ImageURL) Values('Bebidas','bebidas.jpg')");
            mb.Sql("Insert into categories(Name, ImageURL) Values('Lanches','lanches.jpg')");
            mb.Sql("Insert into categories(Name, ImageURL) Values('Sobremesas','sobremesas.jpg')");
        }

        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete from categories");
        }
    }
}
