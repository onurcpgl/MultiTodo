﻿// <auto-generated />
using System;
using DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataAccess.Migrations
{
    [DbContext(typeof(TodoDbContext))]
    [Migration("20230623124301_mig_12")]
    partial class mig_12
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DataAccess.Models.Media", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<string>("AbsolutePath")
                        .HasColumnType("text");

                    b.Property<bool?>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<string>("EncodedFilename")
                        .HasColumnType("text");

                    b.Property<string>("Extension")
                        .HasColumnType("text");

                    b.Property<string>("FilePath")
                        .HasColumnType("text");

                    b.Property<string>("Mime")
                        .HasColumnType("text");

                    b.Property<string>("RealFilename")
                        .HasColumnType("text");

                    b.Property<string>("RootPath")
                        .HasColumnType("text");

                    b.Property<string>("ServePath")
                        .HasColumnType("text");

                    b.Property<long?>("Size")
                        .HasColumnType("bigint");

                    b.Property<int?>("teamId")
                        .HasColumnType("integer");

                    b.Property<int?>("userId")
                        .HasColumnType("integer");

                    b.HasKey("id");

                    b.HasIndex("teamId")
                        .IsUnique();

                    b.HasIndex("userId")
                        .IsUnique();

                    b.ToTable("Medias");
                });

            modelBuilder.Entity("DataAccess.Models.Team", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<string>("description")
                        .HasColumnType("text");

                    b.Property<string>("name")
                        .HasColumnType("text");

                    b.Property<int?>("ownerId")
                        .HasColumnType("integer");

                    b.Property<string>("teamImage")
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.HasIndex("ownerId")
                        .IsUnique();

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("Models.Models.Todo", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<int>("Userid")
                        .HasColumnType("integer");

                    b.Property<DateTime>("createdDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.HasIndex("Userid");

                    b.ToTable("Todos");
                });

            modelBuilder.Entity("Models.Models.User", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<string>("RefreshToken")
                        .HasColumnType("text");

                    b.Property<DateTime?>("RefreshTokenEndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("firstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("lastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("mail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TeamUser", b =>
                {
                    b.Property<int>("Teamsid")
                        .HasColumnType("integer");

                    b.Property<int>("memberListid")
                        .HasColumnType("integer");

                    b.HasKey("Teamsid", "memberListid");

                    b.HasIndex("memberListid");

                    b.ToTable("TeamUser");
                });

            modelBuilder.Entity("DataAccess.Models.Media", b =>
                {
                    b.HasOne("DataAccess.Models.Team", "team")
                        .WithOne("media")
                        .HasForeignKey("DataAccess.Models.Media", "teamId");

                    b.HasOne("Models.Models.User", "user")
                        .WithOne("media")
                        .HasForeignKey("DataAccess.Models.Media", "userId");

                    b.Navigation("team");

                    b.Navigation("user");
                });

            modelBuilder.Entity("DataAccess.Models.Team", b =>
                {
                    b.HasOne("Models.Models.User", "owner")
                        .WithOne("teamOwner")
                        .HasForeignKey("DataAccess.Models.Team", "ownerId");

                    b.Navigation("owner");
                });

            modelBuilder.Entity("Models.Models.Todo", b =>
                {
                    b.HasOne("Models.Models.User", "User")
                        .WithMany("Todos")
                        .HasForeignKey("Userid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("TeamUser", b =>
                {
                    b.HasOne("DataAccess.Models.Team", null)
                        .WithMany()
                        .HasForeignKey("Teamsid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Models.User", null)
                        .WithMany()
                        .HasForeignKey("memberListid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DataAccess.Models.Team", b =>
                {
                    b.Navigation("media");
                });

            modelBuilder.Entity("Models.Models.User", b =>
                {
                    b.Navigation("Todos");

                    b.Navigation("media");

                    b.Navigation("teamOwner");
                });
#pragma warning restore 612, 618
        }
    }
}
