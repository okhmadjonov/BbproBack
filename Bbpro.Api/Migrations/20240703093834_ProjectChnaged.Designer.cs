﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Bbpro.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Bbpro.Api.Migrations
{
    [DbContext(typeof(BbproDbContext))]
    [Migration("20240703093834_ProjectChnaged")]
    partial class ProjectChnaged
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Bbpro.Domain.Entities.About.About", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Abouts");
                });

            modelBuilder.Entity("Bbpro.Domain.Entities.Brands.Brand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<List<string>>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Brands");
                });

            modelBuilder.Entity("Bbpro.Domain.Entities.Categories.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Bbpro.Domain.Entities.Categories.CategoryConnectSolution", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("SolutionId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("SolutionId");

                    b.ToTable("CategoryConnectSolution");
                });

            modelBuilder.Entity("Bbpro.Domain.Entities.Latests.Latest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Latests");
                });

            modelBuilder.Entity("Bbpro.Domain.Entities.MainContact.Contact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("MapFrame")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("Bbpro.Domain.Entities.Orders.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Bbpro.Domain.Entities.Projects.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DownloadLink")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("Bbpro.Domain.Entities.Roles.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Role");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2024, 5, 30, 16, 13, 56, 461, DateTimeKind.Utc),
                            IsActive = true,
                            Name = "SuperAdmin",
                            UpdatedAt = new DateTime(2024, 5, 30, 16, 13, 56, 461, DateTimeKind.Utc)
                        });
                });

            modelBuilder.Entity("Bbpro.Domain.Entities.Roles.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRole");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2024, 5, 30, 16, 13, 56, 461, DateTimeKind.Utc),
                            RoleId = 1,
                            UpdatedAt = new DateTime(2024, 5, 30, 16, 13, 56, 461, DateTimeKind.Utc),
                            UserId = 1
                        });
                });

            modelBuilder.Entity("Bbpro.Domain.Entities.Solutions.Solution", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Solutions");
                });

            modelBuilder.Entity("Bbpro.Domain.Entities.Users.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phonenumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2024, 5, 30, 16, 13, 56, 461, DateTimeKind.Utc),
                            Email = "bigboxpro@gmail.com",
                            Password = "6230ed845243cc96e03127a483eaed5783a0ccdb4760473a1f5dc0f617d9241d",
                            Phonenumber = "+99898 000 00 00",
                            UpdatedAt = new DateTime(2024, 5, 30, 16, 13, 56, 461, DateTimeKind.Utc),
                            Username = "Admin"
                        });
                });

            modelBuilder.Entity("Bbpro.Domain.Entities.About.About", b =>
                {
                    b.OwnsOne("Bbpro.Domain.Entities.Multilanguage.Language", "Description", b1 =>
                        {
                            b1.Property<int>("AboutId")
                                .HasColumnType("integer");

                            b1.Property<string>("EN")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Description_EN");

                            b1.Property<string>("RU")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Description_RU");

                            b1.Property<string>("UZ")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Description_UZ");

                            b1.HasKey("AboutId");

                            b1.ToTable("Abouts");

                            b1.WithOwner()
                                .HasForeignKey("AboutId");
                        });

                    b.OwnsOne("Bbpro.Domain.Entities.Multilanguage.Language", "Title", b1 =>
                        {
                            b1.Property<int>("AboutId")
                                .HasColumnType("integer");

                            b1.Property<string>("EN")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Title_EN");

                            b1.Property<string>("RU")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Title_RU");

                            b1.Property<string>("UZ")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Title_UZ");

                            b1.HasKey("AboutId");

                            b1.ToTable("Abouts");

                            b1.WithOwner()
                                .HasForeignKey("AboutId");
                        });

                    b.Navigation("Description")
                        .IsRequired();

                    b.Navigation("Title")
                        .IsRequired();
                });

            modelBuilder.Entity("Bbpro.Domain.Entities.Categories.Category", b =>
                {
                    b.OwnsOne("Bbpro.Domain.Entities.Multilanguage.Language", "Title", b1 =>
                        {
                            b1.Property<int>("CategoryId")
                                .HasColumnType("integer");

                            b1.Property<string>("EN")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Title_EN");

                            b1.Property<string>("RU")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Title_RU");

                            b1.Property<string>("UZ")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Title_UZ");

                            b1.HasKey("CategoryId");

                            b1.ToTable("Categories");

                            b1.WithOwner()
                                .HasForeignKey("CategoryId");
                        });

                    b.Navigation("Title")
                        .IsRequired();
                });

            modelBuilder.Entity("Bbpro.Domain.Entities.Categories.CategoryConnectSolution", b =>
                {
                    b.HasOne("Bbpro.Domain.Entities.Categories.Category", "Category")
                        .WithMany("Solutions")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Bbpro.Domain.Entities.Solutions.Solution", "Solution")
                        .WithMany()
                        .HasForeignKey("SolutionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Solution");
                });

            modelBuilder.Entity("Bbpro.Domain.Entities.Latests.Latest", b =>
                {
                    b.OwnsOne("Bbpro.Domain.Entities.Multilanguage.Language", "Description", b1 =>
                        {
                            b1.Property<int>("LatestId")
                                .HasColumnType("integer");

                            b1.Property<string>("EN")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Description_EN");

                            b1.Property<string>("RU")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Description_RU");

                            b1.Property<string>("UZ")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Description_UZ");

                            b1.HasKey("LatestId");

                            b1.ToTable("Latests");

                            b1.WithOwner()
                                .HasForeignKey("LatestId");
                        });

                    b.OwnsOne("Bbpro.Domain.Entities.Multilanguage.Language", "Title", b1 =>
                        {
                            b1.Property<int>("LatestId")
                                .HasColumnType("integer");

                            b1.Property<string>("EN")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Title_EN");

                            b1.Property<string>("RU")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Title_RU");

                            b1.Property<string>("UZ")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Title_UZ");

                            b1.HasKey("LatestId");

                            b1.ToTable("Latests");

                            b1.WithOwner()
                                .HasForeignKey("LatestId");
                        });

                    b.Navigation("Description")
                        .IsRequired();

                    b.Navigation("Title")
                        .IsRequired();
                });

            modelBuilder.Entity("Bbpro.Domain.Entities.MainContact.Contact", b =>
                {
                    b.OwnsOne("Bbpro.Domain.Entities.Multilanguage.Language", "Address", b1 =>
                        {
                            b1.Property<int>("ContactId")
                                .HasColumnType("integer");

                            b1.Property<string>("EN")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Address_EN");

                            b1.Property<string>("RU")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Address_RU");

                            b1.Property<string>("UZ")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Address_UZ");

                            b1.HasKey("ContactId");

                            b1.ToTable("Contacts");

                            b1.WithOwner()
                                .HasForeignKey("ContactId");
                        });

                    b.OwnsOne("Bbpro.Domain.Entities.Multilanguage.Language", "Weekend", b1 =>
                        {
                            b1.Property<int>("ContactId")
                                .HasColumnType("integer");

                            b1.Property<string>("EN")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Weekend_EN");

                            b1.Property<string>("RU")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Weekend_RU");

                            b1.Property<string>("UZ")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Weekend_UZ");

                            b1.HasKey("ContactId");

                            b1.ToTable("Contacts");

                            b1.WithOwner()
                                .HasForeignKey("ContactId");
                        });

                    b.OwnsOne("Bbpro.Domain.Entities.Multilanguage.Language", "WorkDay", b1 =>
                        {
                            b1.Property<int>("ContactId")
                                .HasColumnType("integer");

                            b1.Property<string>("EN")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("WorkDay_EN");

                            b1.Property<string>("RU")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("WorkDay_RU");

                            b1.Property<string>("UZ")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("WorkDay_UZ");

                            b1.HasKey("ContactId");

                            b1.ToTable("Contacts");

                            b1.WithOwner()
                                .HasForeignKey("ContactId");
                        });

                    b.Navigation("Address")
                        .IsRequired();

                    b.Navigation("Weekend")
                        .IsRequired();

                    b.Navigation("WorkDay")
                        .IsRequired();
                });

            modelBuilder.Entity("Bbpro.Domain.Entities.Projects.Project", b =>
                {
                    b.OwnsOne("Bbpro.Domain.Entities.Multilanguage.Language", "Description", b1 =>
                        {
                            b1.Property<int>("ProjectId")
                                .HasColumnType("integer");

                            b1.Property<string>("EN")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Description_EN");

                            b1.Property<string>("RU")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Description_RU");

                            b1.Property<string>("UZ")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Description_UZ");

                            b1.HasKey("ProjectId");

                            b1.ToTable("Projects");

                            b1.WithOwner()
                                .HasForeignKey("ProjectId");
                        });

                    b.OwnsOne("Bbpro.Domain.Entities.Multilanguage.Language", "Title", b1 =>
                        {
                            b1.Property<int>("ProjectId")
                                .HasColumnType("integer");

                            b1.Property<string>("EN")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Title_EN");

                            b1.Property<string>("RU")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Title_RU");

                            b1.Property<string>("UZ")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Title_UZ");

                            b1.HasKey("ProjectId");

                            b1.ToTable("Projects");

                            b1.WithOwner()
                                .HasForeignKey("ProjectId");
                        });

                    b.Navigation("Description")
                        .IsRequired();

                    b.Navigation("Title")
                        .IsRequired();
                });

            modelBuilder.Entity("Bbpro.Domain.Entities.Roles.UserRole", b =>
                {
                    b.HasOne("Bbpro.Domain.Entities.Roles.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Bbpro.Domain.Entities.Users.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Bbpro.Domain.Entities.Solutions.Solution", b =>
                {
                    b.OwnsOne("Bbpro.Domain.Entities.Multilanguage.Language", "Description", b1 =>
                        {
                            b1.Property<int>("SolutionId")
                                .HasColumnType("integer");

                            b1.Property<string>("EN")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Description_EN");

                            b1.Property<string>("RU")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Description_RU");

                            b1.Property<string>("UZ")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Description_UZ");

                            b1.HasKey("SolutionId");

                            b1.ToTable("Solutions");

                            b1.WithOwner()
                                .HasForeignKey("SolutionId");
                        });

                    b.OwnsOne("Bbpro.Domain.Entities.Multilanguage.Language", "Title", b1 =>
                        {
                            b1.Property<int>("SolutionId")
                                .HasColumnType("integer");

                            b1.Property<string>("EN")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Title_EN");

                            b1.Property<string>("RU")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Title_RU");

                            b1.Property<string>("UZ")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Title_UZ");

                            b1.HasKey("SolutionId");

                            b1.ToTable("Solutions");

                            b1.WithOwner()
                                .HasForeignKey("SolutionId");
                        });

                    b.Navigation("Description")
                        .IsRequired();

                    b.Navigation("Title")
                        .IsRequired();
                });

            modelBuilder.Entity("Bbpro.Domain.Entities.Categories.Category", b =>
                {
                    b.Navigation("Solutions");
                });
#pragma warning restore 612, 618
        }
    }
}
