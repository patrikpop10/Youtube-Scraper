using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace database.Migrations
{
    public partial class YoutuberTypesChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_youtuber_YoutuberTypes_YoutuberTypeId",
                table: "youtuber");

            migrationBuilder.DropPrimaryKey(
                name: "PK_YoutuberTypes",
                table: "YoutuberTypes");

            migrationBuilder.RenameTable(
                name: "YoutuberTypes",
                newName: "yotuber_type");

            migrationBuilder.AddPrimaryKey(
                name: "youtuber_type_id",
                table: "yotuber_type",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_youtuber_yotuber_type_YoutuberTypeId",
                table: "youtuber",
                column: "YoutuberTypeId",
                principalTable: "yotuber_type",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_youtuber_yotuber_type_YoutuberTypeId",
                table: "youtuber");

            migrationBuilder.DropPrimaryKey(
                name: "youtuber_type_id",
                table: "yotuber_type");

            migrationBuilder.RenameTable(
                name: "yotuber_type",
                newName: "YoutuberTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_YoutuberTypes",
                table: "YoutuberTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_youtuber_YoutuberTypes_YoutuberTypeId",
                table: "youtuber",
                column: "YoutuberTypeId",
                principalTable: "YoutuberTypes",
                principalColumn: "Id");
        }
    }
}
