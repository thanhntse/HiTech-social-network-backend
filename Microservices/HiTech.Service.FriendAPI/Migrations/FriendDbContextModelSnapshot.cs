﻿// <auto-generated />
using System;
using HiTech.Service.FriendAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HiTech.Service.FriendAPI.Migrations
{
    [DbContext(typeof(FriendDbContext))]
    partial class FriendDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HiTech.Service.FriendAPI.Entities.FriendRequest", b =>
                {
                    b.Property<int>("FriendRequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("friend_request_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FriendRequestId"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_at");

                    b.Property<int>("ReceiverId")
                        .HasColumnType("int")
                        .HasColumnName("receiver_id");

                    b.Property<int>("SenderId")
                        .HasColumnType("int")
                        .HasColumnName("sender_id");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("status");

                    b.HasKey("FriendRequestId");

                    b.HasIndex("ReceiverId");

                    b.HasIndex("SenderId");

                    b.ToTable("friend_request", "dbo", t =>
                        {
                            t.HasCheckConstraint("CK_Status_Valid", "[status] IN ('Pending', 'Accepted', 'Denied')");
                        });
                });

            modelBuilder.Entity("HiTech.Service.FriendAPI.Entities.Friendship", b =>
                {
                    b.Property<int>("FriendshipId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("friendship_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FriendshipId"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_at");

                    b.Property<bool>("Status")
                        .HasColumnType("bit")
                        .HasColumnName("status");

                    b.Property<int>("UserReceivedId")
                        .HasColumnType("int")
                        .HasColumnName("user_received_id");

                    b.Property<int>("UserSentId")
                        .HasColumnType("int")
                        .HasColumnName("user_sent_id");

                    b.HasKey("FriendshipId");

                    b.HasIndex("UserReceivedId");

                    b.HasIndex("UserSentId");

                    b.ToTable("friendship", "dbo");
                });

            modelBuilder.Entity("HiTech.Service.FriendAPI.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Avatar")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("avatar");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("full_name");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit")
                        .HasColumnName("is_active");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("is_deleted");

                    b.Property<DateTime?>("LastLogin")
                        .HasColumnType("datetime2")
                        .HasColumnName("last_login");

                    b.HasKey("UserId");

                    b.ToTable("user", "dbo");
                });

            modelBuilder.Entity("HiTech.Service.FriendAPI.Entities.FriendRequest", b =>
                {
                    b.HasOne("HiTech.Service.FriendAPI.Entities.User", "Receiver")
                        .WithMany("ReceivedRequests")
                        .HasForeignKey("ReceiverId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("HiTech.Service.FriendAPI.Entities.User", "Sender")
                        .WithMany("SentRequests")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Receiver");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("HiTech.Service.FriendAPI.Entities.Friendship", b =>
                {
                    b.HasOne("HiTech.Service.FriendAPI.Entities.User", "UserReceived")
                        .WithMany("FriendshipsReceived")
                        .HasForeignKey("UserReceivedId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("HiTech.Service.FriendAPI.Entities.User", "UserSent")
                        .WithMany("FriendshipsSent")
                        .HasForeignKey("UserSentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("UserReceived");

                    b.Navigation("UserSent");
                });

            modelBuilder.Entity("HiTech.Service.FriendAPI.Entities.User", b =>
                {
                    b.Navigation("FriendshipsReceived");

                    b.Navigation("FriendshipsSent");

                    b.Navigation("ReceivedRequests");

                    b.Navigation("SentRequests");
                });
#pragma warning restore 612, 618
        }
    }
}
