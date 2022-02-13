﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using database;

#nullable disable

namespace database.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20220213035457_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresExtension(modelBuilder, "uuid-ossp");
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("database.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<string>("CategoryString")
                        .HasColumnType("varchar")
                        .HasColumnName("category");

                    b.HasKey("Id")
                        .HasName("category_id");

                    b.ToTable("category", (string)null);
                });

            modelBuilder.Entity("database.Video", b =>
                {
                    b.Property<string>("VideoId")
                        .HasColumnType("text");

                    b.Property<Guid?>("CategoryId")
                        .HasColumnType("uuid");

                    b.Property<string>("Country")
                        .HasColumnType("varchar")
                        .HasColumnName("country");

                    b.Property<string>("IFrame")
                        .HasColumnType("varchar")
                        .HasColumnName("iframe");

                    b.Property<bool>("IsFamilyFriendly")
                        .HasColumnType("boolean")
                        .HasColumnName("is_family_friendly");

                    b.Property<long>("Length")
                        .HasColumnType("bigint")
                        .HasColumnName("length");

                    b.Property<DateTime>("UploadDate")
                        .HasColumnType("Date")
                        .HasColumnName("upload_date");

                    b.Property<string>("UploaderAccountUrl")
                        .HasColumnType("text");

                    b.HasKey("VideoId")
                        .HasName("video_id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("UploaderAccountUrl");

                    b.ToTable("video", (string)null);
                });

            modelBuilder.Entity("database.VideoCache", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<DateTime>("Date")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("Date")
                        .HasColumnName("date")
                        .HasDefaultValueSql("getDate()");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<long>("Likes")
                        .HasColumnType("bigint")
                        .HasColumnName("likes");

                    b.Property<string>("Title")
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.Property<string>("VideoId")
                        .HasColumnType("text");

                    b.Property<long>("Views")
                        .HasColumnType("bigint")
                        .HasColumnName("views");

                    b.HasKey("Id")
                        .HasName("videocache_id");

                    b.HasIndex("VideoId");

                    b.ToTable("video_cache", (string)null);
                });

            modelBuilder.Entity("database.Youtuber", b =>
                {
                    b.Property<string>("AccountUrl")
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .HasColumnType("varchar")
                        .HasColumnName("username");

                    b.HasKey("AccountUrl")
                        .HasName("youtuber_id");

                    b.ToTable("youtuber", (string)null);
                });

            modelBuilder.Entity("database.YoutuberCache", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<DateTime>("Date")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("Date")
                        .HasColumnName("date")
                        .HasDefaultValueSql("getDate()");

                    b.Property<long>("SubscriberCount")
                        .HasColumnType("bigint")
                        .HasColumnName("number_of_subscribers");

                    b.Property<string>("YoutuberAccountUrl")
                        .HasColumnType("text");

                    b.HasKey("Id")
                        .HasName("youtuber_cache_id");

                    b.HasIndex("YoutuberAccountUrl");

                    b.ToTable("youtuber_cache", (string)null);
                });

            modelBuilder.Entity("database.Video", b =>
                {
                    b.HasOne("database.Category", "Category")
                        .WithMany("Videos")
                        .HasForeignKey("CategoryId");

                    b.HasOne("database.Youtuber", "Uploader")
                        .WithMany("Videos")
                        .HasForeignKey("UploaderAccountUrl");

                    b.Navigation("Category");

                    b.Navigation("Uploader");
                });

            modelBuilder.Entity("database.VideoCache", b =>
                {
                    b.HasOne("database.Video", "Video")
                        .WithMany("Cache")
                        .HasForeignKey("VideoId");

                    b.Navigation("Video");
                });

            modelBuilder.Entity("database.YoutuberCache", b =>
                {
                    b.HasOne("database.Youtuber", "Youtuber")
                        .WithMany("YoutuberCache")
                        .HasForeignKey("YoutuberAccountUrl");

                    b.Navigation("Youtuber");
                });

            modelBuilder.Entity("database.Category", b =>
                {
                    b.Navigation("Videos");
                });

            modelBuilder.Entity("database.Video", b =>
                {
                    b.Navigation("Cache");
                });

            modelBuilder.Entity("database.Youtuber", b =>
                {
                    b.Navigation("Videos");

                    b.Navigation("YoutuberCache");
                });
#pragma warning restore 612, 618
        }
    }
}
