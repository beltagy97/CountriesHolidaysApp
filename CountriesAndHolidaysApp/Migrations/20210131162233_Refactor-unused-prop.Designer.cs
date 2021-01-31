﻿// <auto-generated />
using System;
using CountriesAndHolidaysApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CountriesAndHolidaysApp.Migrations
{
    [DbContext(typeof(CountriesAndHolidaysContext))]
    [Migration("20210131162233_Refactor-unused-prop")]
    partial class Refactorunusedprop
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("CountriesAndHolidaysApp.Models.Countries", b =>
                {
                    b.Property<int>("CountryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("CountryID");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("CountriesAndHolidaysApp.Models.Holidays", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("CountriesCountryID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("end_date")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("start_date")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("ID");

                    b.HasIndex("CountriesCountryID");

                    b.ToTable("Holidays");
                });

            modelBuilder.Entity("CountriesAndHolidaysApp.Models.Holidays", b =>
                {
                    b.HasOne("CountriesAndHolidaysApp.Models.Countries", null)
                        .WithMany("Holidays")
                        .HasForeignKey("CountriesCountryID");
                });

            modelBuilder.Entity("CountriesAndHolidaysApp.Models.Countries", b =>
                {
                    b.Navigation("Holidays");
                });
#pragma warning restore 612, 618
        }
    }
}