﻿// <auto-generated />
using System;
using HiTech.Service.AuthAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HiTech.Service.AuthAPI.Migrations
{
    [DbContext(typeof(AuthDbContext))]
    partial class AuthDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HiTech.Service.AuthAPI.Entities.Account", b =>
                {
                    b.Property<int>("AccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("account_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AccountId"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("address");

                    b.Property<string>("Avatar")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("avatar");

                    b.Property<string>("Background")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("background");

                    b.Property<string>("Bio")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("bio");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasColumnName("email");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("full_name");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("is_deleted");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("password");

                    b.Property<string>("Phone")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("phone");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("role");

                    b.HasKey("AccountId");

                    b.ToTable("account", "dbo", t =>
                        {
                            t.HasCheckConstraint("CK_Email_Length", "LEN([email]) >= 6");

                            t.HasCheckConstraint("CK_Email_Valid", "CHARINDEX('@', [email]) > 0");

                            t.HasCheckConstraint("CK_Fullname_Length", "LEN([full_name]) >= 6");

                            t.HasCheckConstraint("CK_Phone_Valid", "LEN([phone]) = 10");

                            t.HasCheckConstraint("CK_Role_Valid", "[role] IN ('Member', 'Admin')");
                        });
                });

            modelBuilder.Entity("HiTech.Service.AuthAPI.Entities.ExpiredToken", b =>
                {
                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("token");

                    b.Property<int>("AccountId")
                        .HasColumnType("int")
                        .HasColumnName("account_id");

                    b.Property<DateTime>("InvalidationTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("invalidation_time");

                    b.HasKey("Token");

                    b.HasIndex("AccountId");

                    b.ToTable("expired_token", "dbo");
                });

            modelBuilder.Entity("HiTech.Service.AuthAPI.Entities.RefreshToken", b =>
                {
                    b.Property<int>("RefreshTokenId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("refresh_token_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RefreshTokenId"));

                    b.Property<int>("AccountId")
                        .HasColumnType("int")
                        .HasColumnName("account_id");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2")
                        .HasColumnName("created");

                    b.Property<DateTime>("Expiry")
                        .HasColumnType("datetime2")
                        .HasColumnName("expiry");

                    b.Property<DateTime?>("Revoked")
                        .HasColumnType("datetime2")
                        .HasColumnName("revoked");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("token");

                    b.HasKey("RefreshTokenId");

                    b.HasIndex("AccountId");

                    b.ToTable("refresh_token", "dbo");
                });

            modelBuilder.Entity("HiTech.Service.AuthAPI.Entities.ExpiredToken", b =>
                {
                    b.HasOne("HiTech.Service.AuthAPI.Entities.Account", "Account")
                        .WithMany("ExpiredTokens")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("HiTech.Service.AuthAPI.Entities.RefreshToken", b =>
                {
                    b.HasOne("HiTech.Service.AuthAPI.Entities.Account", "Account")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("HiTech.Service.AuthAPI.Entities.Account", b =>
                {
                    b.Navigation("ExpiredTokens");

                    b.Navigation("RefreshTokens");
                });
#pragma warning restore 612, 618
        }
    }
}
