﻿// <auto-generated />
using Fio.Lunch.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Fio.Lunch.API.Migrations
{
    [DbContext(typeof(FioLunchAPIContext))]
    [Migration("20180616140851_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Fio.Lunch.API.Models.Day", b =>
                {
                    b.Property<DateTime>("Date");

                    b.Property<int?>("MenuId");

                    b.HasKey("Date");

                    b.HasIndex("MenuId");

                    b.ToTable("Day");
                });

            modelBuilder.Entity("Fio.Lunch.API.Models.Meal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("DayDate");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<double>("Rating");

                    b.HasKey("Id");

                    b.HasIndex("DayDate");

                    b.ToTable("Meal");
                });

            modelBuilder.Entity("Fio.Lunch.API.Models.Menu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.HasKey("Id");

                    b.ToTable("Menu");
                });

            modelBuilder.Entity("Fio.Lunch.API.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("DayDate");

                    b.Property<int?>("MealId");

                    b.Property<int?>("MenuId");

                    b.Property<bool>("SelfOrdered");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("DayDate");

                    b.HasIndex("MealId");

                    b.HasIndex("MenuId");

                    b.HasIndex("UserId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("Fio.Lunch.API.Models.Rating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("MealId");

                    b.Property<int?>("OrderId");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("MealId");

                    b.HasIndex("OrderId");

                    b.HasIndex("UserId");

                    b.ToTable("Rating");
                });

            modelBuilder.Entity("Fio.Lunch.API.Models.Resource", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("Data");

                    b.Property<string>("Description");

                    b.HasKey("Id");

                    b.ToTable("Resource");
                });

            modelBuilder.Entity("Fio.Lunch.API.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("Role");

                    b.Property<int?>("UserImageId");

                    b.HasKey("Id");

                    b.HasIndex("UserImageId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Fio.Lunch.API.Models.Day", b =>
                {
                    b.HasOne("Fio.Lunch.API.Models.Menu")
                        .WithMany("Days")
                        .HasForeignKey("MenuId");
                });

            modelBuilder.Entity("Fio.Lunch.API.Models.Meal", b =>
                {
                    b.HasOne("Fio.Lunch.API.Models.Day")
                        .WithMany("Meals")
                        .HasForeignKey("DayDate");
                });

            modelBuilder.Entity("Fio.Lunch.API.Models.Order", b =>
                {
                    b.HasOne("Fio.Lunch.API.Models.Day", "Day")
                        .WithMany()
                        .HasForeignKey("DayDate");

                    b.HasOne("Fio.Lunch.API.Models.Meal", "Meal")
                        .WithMany()
                        .HasForeignKey("MealId");

                    b.HasOne("Fio.Lunch.API.Models.Menu", "Menu")
                        .WithMany()
                        .HasForeignKey("MenuId");

                    b.HasOne("Fio.Lunch.API.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Fio.Lunch.API.Models.Rating", b =>
                {
                    b.HasOne("Fio.Lunch.API.Models.Meal", "Meal")
                        .WithMany()
                        .HasForeignKey("MealId");

                    b.HasOne("Fio.Lunch.API.Models.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId");

                    b.HasOne("Fio.Lunch.API.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Fio.Lunch.API.Models.User", b =>
                {
                    b.HasOne("Fio.Lunch.API.Models.Resource", "UserImage")
                        .WithMany()
                        .HasForeignKey("UserImageId");
                });
#pragma warning restore 612, 618
        }
    }
}
