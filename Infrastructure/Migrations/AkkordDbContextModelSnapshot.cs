﻿// <auto-generated />
using System;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Migrations
{
    [DbContext(typeof(AkkordDbContext))]
    partial class AkkordDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Application.Entities.CitiesOfBaku", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("cities_of_baku");
                });

            modelBuilder.Entity("Application.Entities.CrmTask", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ClosedDt")
                        .HasColumnName("closed_dt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("InsertedDt")
                        .HasColumnName("inserted_dt")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("InsertedUserId")
                        .HasColumnName("inserted_user_id")
                        .HasColumnType("char(36)");

                    b.Property<string>("MeetingResult")
                        .HasColumnName("meeting_result")
                        .HasColumnType("nvarchar(3000)");

                    b.Property<int>("PurposeOfVisit")
                        .HasColumnName("purpose_of_visit")
                        .HasColumnType("int");

                    b.Property<Guid?>("SaleManagerId")
                        .HasColumnName("assigned_user_id")
                        .HasColumnType("char(36)");

                    b.Property<int>("SalePointId")
                        .HasColumnName("sale_point_id")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnName("status")
                        .HasColumnType("int");

                    b.Property<DateTime?>("VisitDt")
                        .HasColumnName("visit_dt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("InsertedUserId");

                    b.HasIndex("SaleManagerId");

                    b.HasIndex("SalePointId");

                    b.ToTable("crm_task");
                });

            modelBuilder.Entity("Application.Entities.CrmTaskPhoto", b =>
                {
                    b.Property<int>("CrmTaskId")
                        .HasColumnName("crm_task_id")
                        .HasColumnType("int");

                    b.Property<int>("PhotoId")
                        .HasColumnName("photo_id")
                        .HasColumnType("int");

                    b.HasKey("CrmTaskId", "PhotoId");

                    b.HasIndex("PhotoId")
                        .IsUnique();

                    b.ToTable("crm_task_photo");
                });

            modelBuilder.Entity("Application.Entities.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .HasColumnName("content")
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int?>("CrmTaskId")
                        .HasColumnName("task_id")
                        .HasColumnType("int");

                    b.Property<Guid>("FromId")
                        .HasColumnName("from_id")
                        .HasColumnType("char(36)")
                        .HasAnnotation("MySql:Collation", "latin1_swedish_ci");

                    b.Property<int?>("SalePointId")
                        .HasColumnName("sale_point_id")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnName("status")
                        .HasColumnType("int");

                    b.Property<Guid>("ToId")
                        .HasColumnName("to_id")
                        .HasColumnType("char(36)")
                        .HasAnnotation("MySql:Collation", "latin1_swedish_ci");

                    b.HasKey("Id");

                    b.HasIndex("CrmTaskId")
                        .IsUnique();

                    b.HasIndex("FromId");

                    b.HasIndex("SalePointId");

                    b.HasIndex("ToId");

                    b.ToTable("notification");
                });

            modelBuilder.Entity("Application.Entities.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int");

                    b.Property<string>("Src")
                        .HasColumnName("src")
                        .HasColumnType("nvarchar(1000)");

                    b.HasKey("Id");

                    b.ToTable("photo");
                });

            modelBuilder.Entity("Application.Entities.ProductPortfolio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.ToTable("product_portfolio");
                });

            modelBuilder.Entity("Application.Entities.Region", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("region");
                });

            modelBuilder.Entity("Application.Entities.SaleManager", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnName("id")
                        .HasColumnType("char(36)");

                    b.Property<string>("FirstName")
                        .HasColumnName("first_name")
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .HasColumnName("last_name")
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Msisdn")
                        .HasColumnName("msisdn")
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("SaleSegment")
                        .HasColumnName("sale_segment")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnName("status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("sale_manager");
                });

            modelBuilder.Entity("Application.Entities.SaleManagerRegion", b =>
                {
                    b.Property<int>("RegionId")
                        .HasColumnName("region_id")
                        .HasColumnType("int");

                    b.Property<Guid>("SaleManagerId")
                        .HasColumnName("sale_manager_id")
                        .HasColumnType("char(36)");

                    b.HasKey("RegionId", "SaleManagerId");

                    b.HasIndex("SaleManagerId");

                    b.ToTable("sale_manager_region");
                });

            modelBuilder.Entity("Application.Entities.SalePoint", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasColumnName("address")
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("City")
                        .HasColumnName("city")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("CodeName")
                        .HasColumnName("code_name")
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Comment")
                        .HasColumnName("comment")
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("ConstBetonSupplier")
                        .HasColumnName("beton_supplier")
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("ConstructionType")
                        .HasColumnName("construction_type")
                        .HasColumnType("int");

                    b.Property<string>("ContactPerson")
                        .HasColumnName("contact_person")
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("ContactPersonDateOfBirth")
                        .HasColumnName("contact_person_date_of_birth")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ContactPersonMsisdn")
                        .HasColumnName("contact_person_msisdn")
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("CorporateSegment")
                        .HasColumnName("corporate_segment")
                        .HasColumnType("int");

                    b.Property<string>("District")
                        .HasColumnName("district")
                        .HasColumnType("nvarchar(500)");

                    b.Property<decimal>("GpsX")
                        .HasColumnName("gps_x")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("GpsY")
                        .HasColumnName("gps_y")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("JuridicalName")
                        .HasColumnName("juridical_name")
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("OwnerDateOfBirth")
                        .HasColumnName("owner_date_of_birth")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("OwnerMsisdn")
                        .HasColumnName("owner_msisdn")
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("OwnerOrDirector")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int?>("PhotoId")
                        .HasColumnName("photo_id")
                        .HasColumnType("int");

                    b.Property<int>("RegionId")
                        .HasColumnName("region_id")
                        .HasColumnType("int");

                    b.Property<Guid>("SaleManagerId")
                        .HasColumnName("sale_manager_id")
                        .HasColumnType("char(36)");

                    b.Property<int>("SaleSegment")
                        .HasColumnName("sale_segment")
                        .HasColumnType("int");

                    b.Property<decimal>("ShopAgg")
                        .HasColumnName("shop_agg")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("ShopBeton")
                        .HasColumnName("shop_beton")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("ShopCement")
                        .HasColumnName("shop_cement")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("ShopClassification")
                        .HasColumnName("shop_classification")
                        .HasColumnType("int");

                    b.Property<string>("ShopName")
                        .HasColumnName("shop_name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("ShopOwnership")
                        .HasColumnName("shop_ownership")
                        .HasColumnType("int");

                    b.Property<int>("ShopType")
                        .HasColumnName("shop_type")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnName("status")
                        .HasColumnType("int");

                    b.Property<string>("Village")
                        .HasColumnName("village")
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.HasIndex("PhotoId");

                    b.HasIndex("RegionId");

                    b.HasIndex("SaleManagerId");

                    b.ToTable("sale_point");
                });

            modelBuilder.Entity("Application.Entities.SalePointPhoto", b =>
                {
                    b.Property<int>("SalePointId")
                        .HasColumnName("sale_point_id")
                        .HasColumnType("int");

                    b.Property<int>("PhotoId")
                        .HasColumnName("photo_id")
                        .HasColumnType("int");

                    b.HasKey("SalePointId", "PhotoId");

                    b.HasIndex("PhotoId")
                        .IsUnique();

                    b.ToTable("sale_point_photo");
                });

            modelBuilder.Entity("Application.Entities.SalePointPortfolio", b =>
                {
                    b.Property<int>("PortfolioId")
                        .HasColumnName("protfolio_id")
                        .HasColumnType("int");

                    b.Property<int>("SalePointId")
                        .HasColumnName("sale_pointId")
                        .HasColumnType("int");

                    b.HasKey("PortfolioId", "SalePointId");

                    b.HasIndex("SalePointId");

                    b.ToTable("sale_point_portfolio");
                });

            modelBuilder.Entity("Application.Entities.SalePointSeller", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnName("date_of_birth")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Fullname")
                        .HasColumnName("fullname")
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Msisdn")
                        .HasColumnName("msisdn")
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("SalePointId")
                        .HasColumnName("sale_point_id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SalePointId");

                    b.ToTable("sale_point_seller");
                });

            modelBuilder.Entity("Application.Entities.Security.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("role");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "admin"
                        },
                        new
                        {
                            Id = 2,
                            Name = "sale_manager"
                        });
                });

            modelBuilder.Entity("Application.Entities.Security.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("char(36)");

                    b.Property<string>("Password")
                        .HasColumnName("password")
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Username")
                        .HasColumnName("username")
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("user");

                    b.HasData(
                        new
                        {
                            Id = new Guid("9245fe4a-d402-451c-b9ed-9c1a04247482"),
                            Password = "admin",
                            Username = "admin"
                        });
                });

            modelBuilder.Entity("Application.Entities.Security.UserRole", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnName("user_id")
                        .HasColumnType("char(36)");

                    b.Property<int>("RoleId")
                        .HasColumnName("role_id")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("user_role");
                });

            modelBuilder.Entity("Application.Entities.CrmTask", b =>
                {
                    b.HasOne("Application.Entities.Security.User", "InsertedUser")
                        .WithMany()
                        .HasForeignKey("InsertedUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Application.Entities.SaleManager", "SaleManager")
                        .WithMany()
                        .HasForeignKey("SaleManagerId");

                    b.HasOne("Application.Entities.SalePoint", "SalePoint")
                        .WithMany()
                        .HasForeignKey("SalePointId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Application.Entities.CrmTaskPhoto", b =>
                {
                    b.HasOne("Application.Entities.CrmTask", "CrmTask")
                        .WithMany("Photos")
                        .HasForeignKey("CrmTaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Application.Entities.Photo", "Photo")
                        .WithOne("CrmPhoto")
                        .HasForeignKey("Application.Entities.CrmTaskPhoto", "PhotoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Application.Entities.Notification", b =>
                {
                    b.HasOne("Application.Entities.CrmTask", "CrmTask")
                        .WithOne("Notification")
                        .HasForeignKey("Application.Entities.Notification", "CrmTaskId");

                    b.HasOne("Application.Entities.Security.User", "From")
                        .WithMany()
                        .HasForeignKey("FromId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Application.Entities.SalePoint", "SalePoint")
                        .WithMany()
                        .HasForeignKey("SalePointId");

                    b.HasOne("Application.Entities.Security.User", "To")
                        .WithMany()
                        .HasForeignKey("ToId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Application.Entities.SaleManager", b =>
                {
                    b.HasOne("Application.Entities.Security.User", "User")
                        .WithOne()
                        .HasForeignKey("Application.Entities.SaleManager", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Application.Entities.SaleManagerRegion", b =>
                {
                    b.HasOne("Application.Entities.Region", "Region")
                        .WithMany("SaleMangerRegions")
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Application.Entities.SaleManager", "SaleManager")
                        .WithMany("SaleMangerRegions")
                        .HasForeignKey("SaleManagerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Application.Entities.SalePoint", b =>
                {
                    b.HasOne("Application.Entities.Photo", "Photo")
                        .WithMany("SalePoints")
                        .HasForeignKey("PhotoId");

                    b.HasOne("Application.Entities.Region", "Region")
                        .WithMany("SalePoints")
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Application.Entities.SaleManager", "SaleManager")
                        .WithMany("SalePoints")
                        .HasForeignKey("SaleManagerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Application.Entities.SalePointPhoto", b =>
                {
                    b.HasOne("Application.Entities.Photo", "Photo")
                        .WithOne("SalePointPhoto")
                        .HasForeignKey("Application.Entities.SalePointPhoto", "PhotoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Application.Entities.SalePoint", "SalePoint")
                        .WithMany("Photos")
                        .HasForeignKey("SalePointId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Application.Entities.SalePointPortfolio", b =>
                {
                    b.HasOne("Application.Entities.ProductPortfolio", "Portfolio")
                        .WithMany("SalePointPortfolioes")
                        .HasForeignKey("PortfolioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Application.Entities.SalePoint", "SalePoint")
                        .WithMany("SalePointPortfolioes")
                        .HasForeignKey("SalePointId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Application.Entities.SalePointSeller", b =>
                {
                    b.HasOne("Application.Entities.SalePoint", "SalePoint")
                        .WithMany("SalePointSellers")
                        .HasForeignKey("SalePointId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Application.Entities.Security.UserRole", b =>
                {
                    b.HasOne("Application.Entities.Security.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Application.Entities.Security.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
