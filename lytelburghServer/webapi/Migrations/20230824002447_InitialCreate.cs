using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LytelburghApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Begetters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GivenName = table.Column<string>(type: "TEXT", nullable: false),
                    SurName = table.Column<string>(type: "TEXT", nullable: false),
                    NickName = table.Column<string>(type: "TEXT", nullable: false),
                    Age = table.Column<float>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Begetters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Thinkbacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Thought = table.Column<string>(type: "TEXT", nullable: false),
                    BegetterId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Thinkbacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Thinkbacks_Begetters_BegetterId",
                        column: x => x.BegetterId,
                        principalTable: "Begetters",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Thinkbacks_BegetterId",
                table: "Thinkbacks",
                column: "BegetterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Thinkbacks");

            migrationBuilder.DropTable(
                name: "Begetters");
        }
    }
}
