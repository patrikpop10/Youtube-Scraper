using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace database.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("create function public.getdate() returns timestamptz stable language sql as 'select now()'");
            
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.CreateTable(
                name: "category",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    category = table.Column<string>(type: "varchar", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("category_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "youtuber",
                columns: table => new
                {
                    AccountUrl = table.Column<string>(type: "text", nullable: false),
                    username = table.Column<string>(type: "varchar", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("youtuber_id", x => x.AccountUrl);
                });

            migrationBuilder.CreateTable(
                name: "video",
                columns: table => new
                {
                    VideoId = table.Column<string>(type: "text", nullable: false),
                    UploaderAccountUrl = table.Column<string>(type: "text", nullable: true),
                    length = table.Column<long>(type: "bigint", nullable: false),
                    country = table.Column<string>(type: "varchar", nullable: true),
                    upload_date = table.Column<DateTime>(type: "Date", nullable: false),
                    is_family_friendly = table.Column<bool>(type: "boolean", nullable: false),
                    iframe = table.Column<string>(type: "varchar", nullable: true),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("video_id", x => x.VideoId);
                    table.ForeignKey(
                        name: "FK_video_category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "category",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_video_youtuber_UploaderAccountUrl",
                        column: x => x.UploaderAccountUrl,
                        principalTable: "youtuber",
                        principalColumn: "AccountUrl");
                });

            migrationBuilder.CreateTable(
                name: "youtuber_cache",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    date = table.Column<DateTime>(type: "Date", nullable: false, defaultValueSql: "getdate()"),
                    number_of_subscribers = table.Column<long>(type: "bigint", nullable: false),
                    YoutuberAccountUrl = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("youtuber_cache_id", x => x.id);
                    table.ForeignKey(
                        name: "FK_youtuber_cache_youtuber_YoutuberAccountUrl",
                        column: x => x.YoutuberAccountUrl,
                        principalTable: "youtuber",
                        principalColumn: "AccountUrl");
                });

            migrationBuilder.CreateTable(
                name: "video_cache",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    date = table.Column<DateTime>(type: "Date", nullable: false, defaultValueSql: "getdate()"),
                    views = table.Column<long>(type: "bigint", nullable: false),
                    likes = table.Column<long>(type: "bigint", nullable: false),
                    title = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    VideoId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("videocache_id", x => x.id);
                    table.ForeignKey(
                        name: "FK_video_cache_video_VideoId",
                        column: x => x.VideoId,
                        principalTable: "video",
                        principalColumn: "VideoId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_video_CategoryId",
                table: "video",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_video_UploaderAccountUrl",
                table: "video",
                column: "UploaderAccountUrl");

            migrationBuilder.CreateIndex(
                name: "IX_video_cache_VideoId",
                table: "video_cache",
                column: "VideoId");

            migrationBuilder.CreateIndex(
                name: "IX_youtuber_cache_YoutuberAccountUrl",
                table: "youtuber_cache",
                column: "YoutuberAccountUrl");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "video_cache");

            migrationBuilder.DropTable(
                name: "youtuber_cache");

            migrationBuilder.DropTable(
                name: "video");

            migrationBuilder.DropTable(
                name: "category");

            migrationBuilder.DropTable(
                name: "youtuber");
        }
    }
}
