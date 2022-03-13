using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Video.Infrastructure.Migrations
{
    public partial class FixPropertyTypoInMoviesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InStoch",
                table: "Movies",
                newName: "InStock");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InStock",
                table: "Movies",
                newName: "InStoch");
        }
    }
}
