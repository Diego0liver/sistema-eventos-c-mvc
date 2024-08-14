using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace sistema_eventos.Migrations
{
    /// <inheritdoc />
    public partial class CadeiraEventoCreatTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cadeiras_eventos",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cadeira_id = table.Column<int>(type: "integer", nullable: false),
                    evento_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cadeiras_eventos", x => x.id);
                    table.ForeignKey(
                        name: "FK_cadeiras_eventos_cadeiras_cadeira_id",
                        column: x => x.cadeira_id,
                        principalTable: "cadeiras",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cadeiras_eventos_eventos_evento_id",
                        column: x => x.evento_id,
                        principalTable: "eventos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cadeiras_eventos_cadeira_id",
                table: "cadeiras_eventos",
                column: "cadeira_id");

            migrationBuilder.CreateIndex(
                name: "IX_cadeiras_eventos_evento_id",
                table: "cadeiras_eventos",
                column: "evento_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cadeiras_eventos");
        }
    }
}
