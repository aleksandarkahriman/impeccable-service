﻿// <auto-generated />
using ImpeccableService.Backend.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ImpeccableService.Backend.Database.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20200927132414_AddCompany")]
    partial class AddCompany
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ImpeccableService.Backend.Database.Offering.Model.MenuEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("VenueId")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("menu");
                });

            modelBuilder.Entity("ImpeccableService.Backend.Database.Offering.Model.MenuItemEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("MenuSectionId")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("SectionId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("ThumbnailImageSerialized")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("SectionId");

                    b.ToTable("menu_item");
                });

            modelBuilder.Entity("ImpeccableService.Backend.Database.Offering.Model.MenuSectionEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("MenuId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("MenuId");

                    b.ToTable("menu_section");
                });

            modelBuilder.Entity("ImpeccableService.Backend.Database.Offering.Model.VenueEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("venue");
                });

            modelBuilder.Entity("ImpeccableService.Backend.Database.UserManagement.Model.CompanyEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("company");
                });

            modelBuilder.Entity("ImpeccableService.Backend.Database.UserManagement.Model.SessionEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("AccessToken")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("LogoutToken")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("session");
                });

            modelBuilder.Entity("ImpeccableService.Backend.Database.UserManagement.Model.UserEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ProfileImageSerialized")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Role")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("user");
                });

            modelBuilder.Entity("ImpeccableService.Backend.Database.Offering.Model.MenuItemEntity", b =>
                {
                    b.HasOne("ImpeccableService.Backend.Database.Offering.Model.MenuSectionEntity", "Section")
                        .WithMany("Items")
                        .HasForeignKey("SectionId");
                });

            modelBuilder.Entity("ImpeccableService.Backend.Database.Offering.Model.MenuSectionEntity", b =>
                {
                    b.HasOne("ImpeccableService.Backend.Database.Offering.Model.MenuEntity", "Menu")
                        .WithMany("Sections")
                        .HasForeignKey("MenuId");
                });

            modelBuilder.Entity("ImpeccableService.Backend.Database.UserManagement.Model.SessionEntity", b =>
                {
                    b.HasOne("ImpeccableService.Backend.Database.UserManagement.Model.UserEntity", "User")
                        .WithMany("Sessions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
