using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SecretSanta.MigrationService.Migrations
{
    /// <inheritdoc />
    public partial class DrawnMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "DrawId",
                table: "Users",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DrawEntry",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GiverId = table.Column<long>(type: "bigint", nullable: false),
                    ReceiverId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrawEntry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DrawEntry_Users_GiverId",
                        column: x => x.GiverId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DrawEntry_Users_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Draws",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Draws", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_DrawId",
                table: "Users",
                column: "DrawId");

            migrationBuilder.CreateIndex(
                name: "IX_DrawEntry_GiverId",
                table: "DrawEntry",
                column: "GiverId");

            migrationBuilder.CreateIndex(
                name: "IX_DrawEntry_ReceiverId",
                table: "DrawEntry",
                column: "ReceiverId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Draws_DrawId",
                table: "Users",
                column: "DrawId",
                principalTable: "Draws",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Draws_DrawId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "DrawEntry");

            migrationBuilder.DropTable(
                name: "Draws");

            migrationBuilder.DropIndex(
                name: "IX_Users_DrawId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DrawId",
                table: "Users");
        }
    }
}
