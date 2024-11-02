﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SecretSanta.MigrationService;

#nullable disable

namespace SecretSanta.MigrationService.Migrations
{
    [DbContext(typeof(SecretSantaDBContext))]
    partial class SecretSantaDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SecretSanta.MigrationService.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long?>("DrawId")
                        .HasColumnType("bigint");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("DrawId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SecretSanta.Models.Models.Draw", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.HasKey("Id");

                    b.ToTable("Draws");
                });

            modelBuilder.Entity("SecretSanta.Models.Models.DrawEntry", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("GiverId")
                        .HasColumnType("bigint");

                    b.Property<long>("ReceiverId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("GiverId");

                    b.HasIndex("ReceiverId");

                    b.ToTable("DrawEntry");
                });

            modelBuilder.Entity("SecretSanta.MigrationService.Models.User", b =>
                {
                    b.HasOne("SecretSanta.Models.Models.Draw", null)
                        .WithMany("Users")
                        .HasForeignKey("DrawId");
                });

            modelBuilder.Entity("SecretSanta.Models.Models.DrawEntry", b =>
                {
                    b.HasOne("SecretSanta.MigrationService.Models.User", "Giver")
                        .WithMany()
                        .HasForeignKey("GiverId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SecretSanta.MigrationService.Models.User", "Receiver")
                        .WithMany()
                        .HasForeignKey("ReceiverId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Giver");

                    b.Navigation("Receiver");
                });

            modelBuilder.Entity("SecretSanta.Models.Models.Draw", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
