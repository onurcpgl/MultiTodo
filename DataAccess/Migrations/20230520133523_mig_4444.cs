using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class mig_4444 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medias_Teams_teamId",
                table: "Medias");

            migrationBuilder.DropForeignKey(
                name: "FK_Medias_Users_userId",
                table: "Medias");

            migrationBuilder.DropIndex(
                name: "IX_Medias_teamId",
                table: "Medias");

            migrationBuilder.AlterColumn<int>(
                name: "userId",
                table: "Medias",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "teamId",
                table: "Medias",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_AdminId",
                table: "Teams",
                column: "AdminId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Medias_Teams_userId",
                table: "Medias",
                column: "userId",
                principalTable: "Teams",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Medias_Users_userId",
                table: "Medias",
                column: "userId",
                principalTable: "Users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Users_AdminId",
                table: "Teams",
                column: "AdminId",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medias_Teams_userId",
                table: "Medias");

            migrationBuilder.DropForeignKey(
                name: "FK_Medias_Users_userId",
                table: "Medias");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Users_AdminId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_AdminId",
                table: "Teams");

            migrationBuilder.AlterColumn<int>(
                name: "userId",
                table: "Medias",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "teamId",
                table: "Medias",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

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
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Medias_Users_userId",
                table: "Medias",
                column: "userId",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
