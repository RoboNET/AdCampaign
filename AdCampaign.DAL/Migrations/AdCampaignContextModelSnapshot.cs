﻿// <auto-generated />
using System;
using AdCampaign.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace AdCampaign.DAL.Migrations
{
    [DbContext(typeof(AdCampaignContext))]
    partial class AdCampaignContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("AdCampaign.DAL.Entities.Advert", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityByDefaultColumn();

                    b.Property<long?>("BlockedById")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("BlockedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsBlocked")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsVisible")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<long>("OwnerId")
                        .HasColumnType("bigint");

                    b.Property<int>("RequestType")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BlockedById")
                        .IsUnique();

                    b.HasIndex("OwnerId");

                    b.ToTable("Adverts");
                });

            modelBuilder.Entity("AdCampaign.DAL.Entities.AdvertStatistic", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityByDefaultColumn();

                    b.Property<long>("AdvertId")
                        .HasColumnType("bigint");

                    b.Property<int>("AdvertStatisticType")
                        .HasColumnType("integer");

                    b.Property<long>("Value")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("AdvertId");

                    b.ToTable("AdvertsStatistics");
                });

            modelBuilder.Entity("AdCampaign.DAL.Entities.Application", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityByDefaultColumn();

                    b.Property<long>("AdvertId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AdvertId");

                    b.ToTable("Applications");
                });

            modelBuilder.Entity("AdCampaign.DAL.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityByDefaultColumn();

                    b.Property<long?>("BlockedById")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("BlockedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<bool>("IsBlocked")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .HasColumnType("text");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BlockedById")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("AdCampaign.DAL.Entities.Advert", b =>
                {
                    b.HasOne("AdCampaign.DAL.Entities.User", "BlockedBy")
                        .WithOne()
                        .HasForeignKey("AdCampaign.DAL.Entities.Advert", "BlockedById");

                    b.HasOne("AdCampaign.DAL.Entities.User", "Owner")
                        .WithMany("Adverts")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BlockedBy");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("AdCampaign.DAL.Entities.AdvertStatistic", b =>
                {
                    b.HasOne("AdCampaign.DAL.Entities.Advert", "Advert")
                        .WithMany("AdvertStatistics")
                        .HasForeignKey("AdvertId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Advert");
                });

            modelBuilder.Entity("AdCampaign.DAL.Entities.Application", b =>
                {
                    b.HasOne("AdCampaign.DAL.Entities.Advert", "Advert")
                        .WithMany("Applications")
                        .HasForeignKey("AdvertId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Advert");
                });

            modelBuilder.Entity("AdCampaign.DAL.Entities.User", b =>
                {
                    b.HasOne("AdCampaign.DAL.Entities.User", "BlockedBy")
                        .WithOne()
                        .HasForeignKey("AdCampaign.DAL.Entities.User", "BlockedById");

                    b.Navigation("BlockedBy");
                });

            modelBuilder.Entity("AdCampaign.DAL.Entities.Advert", b =>
                {
                    b.Navigation("AdvertStatistics");

                    b.Navigation("Applications");
                });

            modelBuilder.Entity("AdCampaign.DAL.Entities.User", b =>
                {
                    b.Navigation("Adverts");
                });
#pragma warning restore 612, 618
        }
    }
}
