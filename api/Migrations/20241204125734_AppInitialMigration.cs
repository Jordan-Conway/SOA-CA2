using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class AppInitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PokemonEntry",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonEntry", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuestionEntry",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Question = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionEntry", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VoteEntry",
                columns: table => new
                {
                    PokemonId = table.Column<int>(type: "INTEGER", nullable: false),
                    QuestionId = table.Column<int>(type: "INTEGER", nullable: false),
                    VoteCount = table.Column<int>(type: "INTEGER", nullable: false),
                    PokemonsId = table.Column<int>(type: "INTEGER", nullable: false),
                    QuestionsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoteEntry", x => new { x.PokemonId, x.QuestionId });
                    table.ForeignKey(
                        name: "FK_VoteEntry_PokemonEntry_PokemonsId",
                        column: x => x.PokemonsId,
                        principalTable: "PokemonEntry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VoteEntry_QuestionEntry_QuestionsId",
                        column: x => x.QuestionsId,
                        principalTable: "QuestionEntry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VoteEntry_PokemonsId",
                table: "VoteEntry",
                column: "PokemonsId");

            migrationBuilder.CreateIndex(
                name: "IX_VoteEntry_QuestionsId",
                table: "VoteEntry",
                column: "QuestionsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VoteEntry");

            migrationBuilder.DropTable(
                name: "PokemonEntry");

            migrationBuilder.DropTable(
                name: "QuestionEntry");
        }
    }
}
