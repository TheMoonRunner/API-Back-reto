using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WEB_API_MIN.Migrations
{
    public partial class firstmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Informaciones",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Informaciones", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Informaciones");
        }
    }
}
