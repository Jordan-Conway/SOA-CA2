using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class AppRemoveKeyConstraintsMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VoteEntry_PokemonEntry_PokemonsId",
                table: "VoteEntry");

            migrationBuilder.DropForeignKey(
                name: "FK_VoteEntry_QuestionEntry_QuestionsId",
                table: "VoteEntry");

            migrationBuilder.DropIndex(
                name: "IX_VoteEntry_PokemonsId",
                table: "VoteEntry");

            migrationBuilder.DropIndex(
                name: "IX_VoteEntry_QuestionsId",
                table: "VoteEntry");

            migrationBuilder.DropColumn(
                name: "PokemonsId",
                table: "VoteEntry");

            migrationBuilder.DropColumn(
                name: "QuestionsId",
                table: "VoteEntry");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PokemonsId",
                table: "VoteEntry",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QuestionsId",
                table: "VoteEntry",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_VoteEntry_PokemonsId",
                table: "VoteEntry",
                column: "PokemonsId");

            migrationBuilder.CreateIndex(
                name: "IX_VoteEntry_QuestionsId",
                table: "VoteEntry",
                column: "QuestionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_VoteEntry_PokemonEntry_PokemonsId",
                table: "VoteEntry",
                column: "PokemonsId",
                principalTable: "PokemonEntry",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VoteEntry_QuestionEntry_QuestionsId",
                table: "VoteEntry",
                column: "QuestionsId",
                principalTable: "QuestionEntry",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
