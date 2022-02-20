using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace database.Migrations
{
    public partial class YoutuberTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "YoutuberTypeId",
                table: "youtuber",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "YoutuberTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    type = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YoutuberTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_youtuber_YoutuberTypeId",
                table: "youtuber",
                column: "YoutuberTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_youtuber_YoutuberTypes_YoutuberTypeId",
                table: "youtuber",
                column: "YoutuberTypeId",
                principalTable: "YoutuberTypes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_youtuber_YoutuberTypes_YoutuberTypeId",
                table: "youtuber");

            migrationBuilder.DropTable(
                name: "YoutuberTypes");

            migrationBuilder.DropIndex(
                name: "IX_youtuber_YoutuberTypeId",
                table: "youtuber");

            migrationBuilder.DropColumn(
                name: "YoutuberTypeId",
                table: "youtuber");
        }
    }
}
