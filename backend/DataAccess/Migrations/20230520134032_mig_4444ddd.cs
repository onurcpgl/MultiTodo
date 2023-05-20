using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class mig_4444ddd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medias_Teams_userId",
                table: "Medias");

            migrationBuilder.CreateIndex(
                name: "IX_Medias_teamId",
                table: "Medias",
                column: "teamId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Medias_Teams_teamId",
                table: "Medias",
                column: "teamId",
                principalTable: "Teams",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medias_Teams_teamId",
                table: "Medias");

            migrationBuilder.DropIndex(
                name: "IX_Medias_teamId",
                table: "Medias");

            migrationBuilder.AddForeignKey(
                name: "FK_Medias_Teams_userId",
                table: "Medias",
                column: "userId",
                principalTable: "Teams",
                principalColumn: "id");
        }
    }
}
