﻿// <auto-generated />
using System;
using MagicVilla_API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MagicVilla_API.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MagicVilla_API.Models.NumberVilla", b =>
                {
                    b.Property<int>("VillaNo")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("EspecialDetail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("VillaId")
                        .HasColumnType("int");

                    b.HasKey("VillaNo");

                    b.HasIndex("VillaId");

                    b.ToTable("NumberVillas");
                });

            modelBuilder.Entity("MagicVilla_API.Models.Villa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Amenity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Detail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Occupants")
                        .HasColumnType("int");

                    b.Property<double>("Rate")
                        .HasColumnType("float");

                    b.Property<int>("SquareMeter")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UrlImage")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Villas");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amenity = "",
                            CreationDate = new DateTime(2023, 6, 14, 18, 38, 36, 911, DateTimeKind.Local).AddTicks(6742),
                            Detail = "Detalle de la Villa...",
                            Name = "Villa Real",
                            Occupants = 5,
                            Rate = 200.0,
                            SquareMeter = 50,
                            UpdateDate = new DateTime(2023, 6, 14, 18, 38, 36, 911, DateTimeKind.Local).AddTicks(6766),
                            UrlImage = ""
                        },
                        new
                        {
                            Id = 2,
                            Amenity = "",
                            CreationDate = new DateTime(2023, 6, 14, 18, 38, 36, 911, DateTimeKind.Local).AddTicks(6773),
                            Detail = "Detalle de la Villa...",
                            Name = "Premium Vista a la Piscina",
                            Occupants = 4,
                            Rate = 150.0,
                            SquareMeter = 40,
                            UpdateDate = new DateTime(2023, 6, 14, 18, 38, 36, 911, DateTimeKind.Local).AddTicks(6775),
                            UrlImage = ""
                        });
                });

            modelBuilder.Entity("MagicVilla_API.Models.NumberVilla", b =>
                {
                    b.HasOne("MagicVilla_API.Models.Villa", "Villa")
                        .WithMany()
                        .HasForeignKey("VillaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Villa");
                });
#pragma warning restore 612, 618
        }
    }
}
