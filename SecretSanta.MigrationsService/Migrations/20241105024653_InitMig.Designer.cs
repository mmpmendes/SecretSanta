﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SecretSanta.MigrationService;

#nullable disable

namespace SecretSanta.MigrationService.Migrations
{
    [DbContext(typeof(SecretSantaDBContext))]
    [Migration("20241105024653_InitMig")]
    partial class InitMig
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SecretSanta.Models.Models.DrawEntry", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("GiverEmail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("GiverId")
                        .HasColumnType("bigint");

                    b.Property<string>("GiverName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ReceiverEmail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("ReceiverId")
                        .HasColumnType("bigint");

                    b.Property<string>("ReceiverName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Entries");
                });
#pragma warning restore 612, 618
        }
    }
}
