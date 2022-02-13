using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace database.Migrations
{
    public partial class ChangedNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_video_category_CategoryId",
                table: "video");

            migrationBuilder.DropForeignKey(
                name: "FK_video_youtuber_UploaderAccountUrl",
                table: "video");

            migrationBuilder.RenameColumn(
                name: "VideoId",
                table: "video",
                newName: "id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "date",
                table: "youtuber_cache",
                type: "Date",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "Date",
                oldDefaultValueSql: "getDate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "date",
                table: "video_cache",
                type: "Date",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "Date",
                oldDefaultValueSql: "getDate()");

            migrationBuilder.AddForeignKey(
                name: "category_id",
                table: "video",
                column: "CategoryId",
                principalTable: "category",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "uploader_id",
                table: "video",
                column: "UploaderAccountUrl",
                principalTable: "youtuber",
                principalColumn: "AccountUrl");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "category_id",
                table: "video");

            migrationBuilder.DropForeignKey(
                name: "uploader_id",
                table: "video");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "video",
                newName: "VideoId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "date",
                table: "youtuber_cache",
                type: "Date",
                nullable: false,
                defaultValueSql: "getDate()",
                oldClrType: typeof(DateTime),
                oldType: "Date",
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "date",
                table: "video_cache",
                type: "Date",
                nullable: false,
                defaultValueSql: "getDate()",
                oldClrType: typeof(DateTime),
                oldType: "Date",
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AddForeignKey(
                name: "FK_video_category_CategoryId",
                table: "video",
                column: "CategoryId",
                principalTable: "category",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_video_youtuber_UploaderAccountUrl",
                table: "video",
                column: "UploaderAccountUrl",
                principalTable: "youtuber",
                principalColumn: "AccountUrl");
        }
    }
}
