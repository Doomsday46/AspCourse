using Microsoft.EntityFrameworkCore.Migrations;

namespace AspCourse.Migrations
{
    public partial class Updatedatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TournamentPlayer",
                table: "TournamentPlayer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TournamentLocation",
                table: "TournamentLocation");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TournamentPlayer",
                table: "TournamentPlayer",
                columns: new[] { "Id", "PlayerId", "TournamentId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_TournamentLocation",
                table: "TournamentLocation",
                columns: new[] { "Id", "LocationId", "TournamentId" });

            migrationBuilder.CreateIndex(
                name: "IX_TournamentPlayer_PlayerId",
                table: "TournamentPlayer",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentLocation_LocationId",
                table: "TournamentLocation",
                column: "LocationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TournamentPlayer",
                table: "TournamentPlayer");

            migrationBuilder.DropIndex(
                name: "IX_TournamentPlayer_PlayerId",
                table: "TournamentPlayer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TournamentLocation",
                table: "TournamentLocation");

            migrationBuilder.DropIndex(
                name: "IX_TournamentLocation_LocationId",
                table: "TournamentLocation");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TournamentPlayer",
                table: "TournamentPlayer",
                columns: new[] { "PlayerId", "TournamentId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_TournamentLocation",
                table: "TournamentLocation",
                columns: new[] { "LocationId", "TournamentId" });
        }
    }
}
