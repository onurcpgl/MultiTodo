using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class mig_22 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Todos",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "Todos",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "assignUserid",
                table: "Todos",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Todos_assignUserid",
                table: "Todos",
                column: "assignUserid");

            migrationBuilder.AddForeignKey(
                name: "FK_Todos_Users_assignUserid",
                table: "Todos",
                column: "assignUserid",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Todos_Users_assignUserid",
                table: "Todos");

            migrationBuilder.DropIndex(
                name: "IX_Todos_assignUserid",
                table: "Todos");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Todos");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Todos");

            migrationBuilder.DropColumn(
                name: "assignUserid",
                table: "Todos");
        }
    }
}
