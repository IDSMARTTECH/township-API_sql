﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Township_API.Data;

#nullable disable

namespace Township_API.Migrations
{
    [DbContext(typeof(AppDBContext))]
    [Migration("20250403132756_CodeModule_Create")]
    partial class CodeModule_Create
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Township_API.Models.Module", b =>
                {
                    b.Property<int>("ModuleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ModuleID"));

                    b.Property<string>("ModuleName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ModuleID");

                    b.ToTable("[tblModules]");
                });

            modelBuilder.Entity("Township_API.Models.ModuleData", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(21)
                        .HasColumnType("nvarchar(21)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("ParentID")
                        .HasColumnType("int");

                    b.Property<int>("TypeID")
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    b.Property<int?>("createdby")
                        .HasColumnType("int");

                    b.Property<DateTime?>("createdon")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("isactive")
                        .HasColumnType("bit");

                    b.Property<int?>("updatedby")
                        .HasColumnType("int");

                    b.Property<DateTime?>("updatedon")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.ToTable("tblModuleData");

                    b.HasDiscriminator().HasValue("ModuleData");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Township_API.Models.Profile", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int?>("createdby")
                        .HasColumnType("int");

                    b.Property<DateTime?>("createdon")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("isactive")
                        .HasColumnType("bit");

                    b.Property<bool?>("isdeleted")
                        .HasColumnType("bit");

                    b.Property<string>("profilename")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("uid")
                        .HasColumnType("int");

                    b.Property<int?>("updatedby")
                        .HasColumnType("int");

                    b.Property<DateTime?>("updatedon")
                        .HasColumnType("datetime2");

                    b.Property<int?>("userId")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("userId");

                    b.ToTable("[tblProfile]");
                });

            modelBuilder.Entity("Township_API.Models.ProfileDetails", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<bool?>("CanDelete")
                        .HasColumnType("bit");

                    b.Property<bool?>("CanInsert")
                        .HasColumnType("bit");

                    b.Property<bool?>("CanUpdate")
                        .HasColumnType("bit");

                    b.Property<bool?>("CanView")
                        .HasColumnType("bit");

                    b.Property<int?>("createdby")
                        .HasColumnType("int");

                    b.Property<DateTime?>("createdon")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("isactive")
                        .HasColumnType("bit");

                    b.Property<bool?>("isdeleted")
                        .HasColumnType("bit");

                    b.Property<int?>("moduleId")
                        .HasColumnType("int");

                    b.Property<int?>("profileid")
                        .HasColumnType("int");

                    b.Property<int?>("updatedby")
                        .HasColumnType("int");

                    b.Property<DateTime?>("updatedon")
                        .HasColumnType("datetime2");

                    b.HasKey("id");

                    b.HasIndex("moduleId");

                    b.HasIndex("profileid");

                    b.ToTable("[tblProfileDetails]");
                });

            modelBuilder.Entity("Township_API.Models.Role", b =>
                {
                    b.Property<int>("RoleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleID"));

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("RoleID");

                    b.ToTable("[tblRole]");
                });

            modelBuilder.Entity("Township_API.Models.Service_Provider", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("ServiceProviderID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("createdby")
                        .HasColumnType("int");

                    b.Property<DateTime?>("createdon")
                        .HasColumnType("datetime2");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("isactive")
                        .HasColumnType("bit");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("updatedby")
                        .HasColumnType("int");

                    b.Property<DateTime?>("updatedon")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.ToTable("tblServiceProvider");
                });

            modelBuilder.Entity("Township_API.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("RoleID")
                        .HasColumnType("int");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("uid")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("RoleID");

                    b.ToTable("[tblUser]");
                });

            modelBuilder.Entity("Township_API.Models.Vehicle", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Logical_Delete")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("PrintedTagID")
                        .HasColumnType("datetime2");

                    b.Property<string>("RegNo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("StickerNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("TagEncodingDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("TagUID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("createdby")
                        .HasColumnType("int");

                    b.Property<DateTime?>("createdon")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("isactive")
                        .HasColumnType("bit");

                    b.Property<int?>("updatedby")
                        .HasColumnType("int");

                    b.Property<DateTime?>("updatedon")
                        .HasColumnType("datetime2");

                    b.Property<string>("vColor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("vMake")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("vType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("tblVehicle");
                });

            modelBuilder.Entity("Township_API.Models.Amenities", b =>
                {
                    b.HasBaseType("Township_API.Models.ModuleData");

                    b.ToTable("tblModuleData");

                    b.HasDiscriminator().HasValue("Amenities");
                });

            modelBuilder.Entity("Township_API.Models.Building", b =>
                {
                    b.HasBaseType("Township_API.Models.ModuleData");

                    b.ToTable("tblModuleData");

                    b.HasDiscriminator().HasValue("Building");
                });

            modelBuilder.Entity("Township_API.Models.ContractorType", b =>
                {
                    b.HasBaseType("Township_API.Models.ModuleData");

                    b.ToTable("tblModuleData");

                    b.HasDiscriminator().HasValue("ContractorType");
                });

            modelBuilder.Entity("Township_API.Models.NRD", b =>
                {
                    b.HasBaseType("Township_API.Models.ModuleData");

                    b.ToTable("tblModuleData");

                    b.HasDiscriminator().HasValue("NRD");
                });

            modelBuilder.Entity("Township_API.Models.Phase", b =>
                {
                    b.HasBaseType("Township_API.Models.ModuleData");

                    b.ToTable("tblModuleData");

                    b.HasDiscriminator().HasValue("Phase");
                });

            modelBuilder.Entity("Township_API.Models.ReaderLocation", b =>
                {
                    b.HasBaseType("Township_API.Models.ModuleData");

                    b.ToTable("tblModuleData");

                    b.HasDiscriminator().HasValue("ReaderLocation");
                });

            modelBuilder.Entity("Township_API.Models.ReaderRelay", b =>
                {
                    b.HasBaseType("Township_API.Models.ModuleData");

                    b.ToTable("tblModuleData");

                    b.HasDiscriminator().HasValue("ReaderRelay");
                });

            modelBuilder.Entity("Township_API.Models.ServiceType", b =>
                {
                    b.HasBaseType("Township_API.Models.ModuleData");

                    b.ToTable("tblModuleData");

                    b.HasDiscriminator().HasValue("ServiceType");
                });

            modelBuilder.Entity("Township_API.Models.VehicleMake", b =>
                {
                    b.HasBaseType("Township_API.Models.ModuleData");

                    b.ToTable("tblModuleData");

                    b.HasDiscriminator().HasValue("VehicleMake");
                });

            modelBuilder.Entity("Township_API.Models.Profile", b =>
                {
                    b.HasOne("Township_API.Models.User", "user")
                        .WithMany()
                        .HasForeignKey("userId");

                    b.Navigation("user");
                });

            modelBuilder.Entity("Township_API.Models.ProfileDetails", b =>
                {
                    b.HasOne("Township_API.Models.Module", "module")
                        .WithMany()
                        .HasForeignKey("moduleId");

                    b.HasOne("Township_API.Models.Profile", "profile")
                        .WithMany()
                        .HasForeignKey("profileid");

                    b.Navigation("module");

                    b.Navigation("profile");
                });

            modelBuilder.Entity("Township_API.Models.User", b =>
                {
                    b.HasOne("Township_API.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleID");

                    b.Navigation("Role");
                });
#pragma warning restore 612, 618
        }
    }
}
