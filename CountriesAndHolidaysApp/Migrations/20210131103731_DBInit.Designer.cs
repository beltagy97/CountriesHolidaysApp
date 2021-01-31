﻿// <auto-generated />
using CountriesAndHolidaysApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CountriesAndHolidaysApp.Migrations
{
    [DbContext(typeof(CountriesAndHolidaysContext))]
    [Migration("20210131103731_DBInit")]
    partial class DBInit
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("CountriesAndHolidaysApp.Models.Countries", b =>
                {
                    b.Property<string>("Code")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Code");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("CountriesAndHolidaysApp.Models.Holidays", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CountryCode")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("end_date")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("start_date")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("CountryCode");

                    b.ToTable("Holidays");
                });

            modelBuilder.Entity("CountriesAndHolidaysApp.Models.Holidays", b =>
                {
                    b.HasOne("CountriesAndHolidaysApp.Models.Countries", "Country")
                        .WithMany("Holidays")
                        .HasForeignKey("CountryCode");

                    b.Navigation("Country");
                });

            modelBuilder.Entity("CountriesAndHolidaysApp.Models.Countries", b =>
                {
                    b.Navigation("Holidays");
                });
#pragma warning restore 612, 618
        }
    }
}