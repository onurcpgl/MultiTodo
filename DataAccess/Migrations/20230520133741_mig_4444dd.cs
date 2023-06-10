using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class mig_4444dd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Users_AdminId",
                table: "Teams");

            migrationBuilder.RenameColumn(
                name: "AdminId",
                table: "Teams",
                newName: "ownerId");

            migrationBuilder.RenameIndex(
                name: "IX_Teams_AdminId",
                table: "Teams",
                newName: "IX_Teams_ownerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Users_ownerId",
                table: "Teams",
                column: "ownerId",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Users_ownerId",
                table: "Teams");

            migrationBuilder.RenameColumn(
                name: "ownerId",
                table: "Teams",
                newName: "AdminId");

            migrationBuilder.RenameIndex(
                name: "IX_Teams_ownerId",
                table: "Teams",
                newName: "IX_Teams_AdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Users_AdminId",
                table: "Teams",
                column: "AdminId",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
