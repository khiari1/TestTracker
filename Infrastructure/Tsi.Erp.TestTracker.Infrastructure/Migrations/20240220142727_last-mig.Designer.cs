﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Tsi.Erp.TestTracker.EntityFrameworkCore.Context;


#nullable disable

namespace Tsi.Erp.TestTracker.Infrastructure.Migrations
{
    [DbContext(typeof(TestTrackerContext))]
    [Migration("20240220142727_last-mig")]
    partial class lastmig
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Tsi.AspNetCore.Identity.TsiIdentityGroup", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.HasKey("Id");

                    b.ToTable("TsiGroups");
                });

            modelBuilder.Entity("Tsi.AspNetCore.Identity.TsiIdentityPermission", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("GroupId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("TsiPermissions");
                });

            modelBuilder.Entity("Tsi.AspNetCore.Identity.TsiIdentityUserGroup<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("GroupId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "GroupId");

                    b.HasIndex("GroupId");

                    b.ToTable("TsiUserGroups");
                });

            modelBuilder.Entity("Tsi.Erp.TestTracker.Domain.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("AccountEnabled")
                        .HasMaxLength(250)
                        .HasColumnType("bit");

                    b.Property<string>("City")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("CompanyName")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Country")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("CreationType")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Department")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("EmployeeType")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("FirstName")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("GivenName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("JobTitle")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("LastName")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Login")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Mail")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("MailNickname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MobilePhone")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("OfficeLocation")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Password")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("PostalCode")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("StreetAddress")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Surname")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("UserPrincipalName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TsiUsers");

                    b.HasData(
                        new
                        {
                            Id = "a7467acf-4af4-4504-aa43-7ff02cad6d39",
                            AccountEnabled = true,
                            DisplayName = "Louhichi",
                            FirstName = "Louhichi",
                            GivenName = "Louhichi",
                            IsAdmin = true,
                            LastName = "Marwen",
                            Login = "louhichi.marwen@outlook.com",
                            Mail = "louhichi.marwen@outlook.com",
                            MailNickname = "admin",
                            Password = "",
                            Surname = "Marwen",
                            UserName = "Louhichi",
                            UserPrincipalName = "louhichi.marwen@testtracker.onmicrosoft.com"
                        });
                });

            modelBuilder.Entity("Tsi.Erp.TestTracker.Domain.Models.AssemblyFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<byte[]>("AssemblyBytes")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Assembly");
                });

            modelBuilder.Entity("Tsi.Erp.TestTracker.Domain.Models.Attachement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Data")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Extension")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KeyGroup")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RelatedObject")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Attachements");
                });

            modelBuilder.Entity("Tsi.Erp.TestTracker.Domain.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KeyGroup")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("RelatedObject")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("UserId1")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("date")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("UserId1");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Tsi.Erp.TestTracker.Domain.Models.Functionality", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("SubMenuId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SubMenuId");

                    b.ToTable("Functionalities");
                });

            modelBuilder.Entity("Tsi.Erp.TestTracker.Domain.Models.Job", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Finishdate")
                        .HasColumnType("datetime2");

                    b.Property<int>("JobState")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("Startdate")
                        .HasColumnType("datetime2");

                    b.Property<int>("TaskId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Job");
                });

            modelBuilder.Entity("Tsi.Erp.TestTracker.Domain.Models.Menu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ModuleId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("ModuleId");

                    b.ToTable("Menus");
                });

            modelBuilder.Entity("Tsi.Erp.TestTracker.Domain.Models.Module", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Modules");
                });

            modelBuilder.Entity("Tsi.Erp.TestTracker.Domain.Models.Monitoring", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AwaitedResult")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FailingSince")
                        .HasColumnType("datetime2");

                    b.Property<int>("FunctionalityId")
                        .HasColumnType("int");

                    b.Property<string>("NameMethodTest")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Preconditions")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResponsibleId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TesterId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UseCase")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FunctionalityId");

                    b.HasIndex("ResponsibleId");

                    b.HasIndex("TesterId");

                    b.ToTable("Monitorings");
                });

            modelBuilder.Entity("Tsi.Erp.TestTracker.Domain.Models.MonitoringDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("BuildVersion")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("time");

                    b.Property<string>("ExceptionMessage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExceptionType")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MonitoringId")
                        .HasColumnType("int");

                    b.Property<string>("StackTrace")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.Property<string>("Ticket")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.HasKey("Id");

                    b.HasIndex("MonitoringId");

                    b.ToTable("MonitoringDetails");
                });

            modelBuilder.Entity("Tsi.Erp.TestTracker.Domain.Models.Settings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.HasKey("Id");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("Tsi.Erp.TestTracker.Domain.Models.SubMenu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("MenuId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("MenuId");

                    b.ToTable("SubMenus");
                });

            modelBuilder.Entity("Tsi.AspNetCore.Identity.TsiIdentityPermission", b =>
                {
                    b.HasOne("Tsi.AspNetCore.Identity.TsiIdentityGroup", null)
                        .WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Tsi.AspNetCore.Identity.TsiIdentityUserGroup<string>", b =>
                {
                    b.HasOne("Tsi.AspNetCore.Identity.TsiIdentityGroup", null)
                        .WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Tsi.Erp.TestTracker.Domain.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Tsi.Erp.TestTracker.Domain.Models.Comment", b =>
                {
                    b.HasOne("Tsi.Erp.TestTracker.Domain.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId1");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Tsi.Erp.TestTracker.Domain.Models.Functionality", b =>
                {
                    b.HasOne("Tsi.Erp.TestTracker.Domain.Models.SubMenu", "SubMenu")
                        .WithMany()
                        .HasForeignKey("SubMenuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SubMenu");
                });

            modelBuilder.Entity("Tsi.Erp.TestTracker.Domain.Models.Menu", b =>
                {
                    b.HasOne("Tsi.Erp.TestTracker.Domain.Models.Module", "Module")
                        .WithMany("Menus")
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Module");
                });

            modelBuilder.Entity("Tsi.Erp.TestTracker.Domain.Models.Monitoring", b =>
                {
                    b.HasOne("Tsi.Erp.TestTracker.Domain.Models.Functionality", "Functionality")
                        .WithMany("Monitorings")
                        .HasForeignKey("FunctionalityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Tsi.Erp.TestTracker.Domain.Models.ApplicationUser", "Responsible")
                        .WithMany()
                        .HasForeignKey("ResponsibleId");

                    b.HasOne("Tsi.Erp.TestTracker.Domain.Models.ApplicationUser", "Tester")
                        .WithMany()
                        .HasForeignKey("TesterId");

                    b.Navigation("Functionality");

                    b.Navigation("Responsible");

                    b.Navigation("Tester");
                });

            modelBuilder.Entity("Tsi.Erp.TestTracker.Domain.Models.MonitoringDetail", b =>
                {
                    b.HasOne("Tsi.Erp.TestTracker.Domain.Models.Monitoring", "Monitoring")
                        .WithMany("MonitoringDetails")
                        .HasForeignKey("MonitoringId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Monitoring");
                });

            modelBuilder.Entity("Tsi.Erp.TestTracker.Domain.Models.SubMenu", b =>
                {
                    b.HasOne("Tsi.Erp.TestTracker.Domain.Models.Menu", "Menu")
                        .WithMany("SubMenus")
                        .HasForeignKey("MenuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Menu");
                });

            modelBuilder.Entity("Tsi.Erp.TestTracker.Domain.Models.Functionality", b =>
                {
                    b.Navigation("Monitorings");
                });

            modelBuilder.Entity("Tsi.Erp.TestTracker.Domain.Models.Menu", b =>
                {
                    b.Navigation("SubMenus");
                });

            modelBuilder.Entity("Tsi.Erp.TestTracker.Domain.Models.Module", b =>
                {
                    b.Navigation("Menus");
                });

            modelBuilder.Entity("Tsi.Erp.TestTracker.Domain.Models.Monitoring", b =>
                {
                    b.Navigation("MonitoringDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
