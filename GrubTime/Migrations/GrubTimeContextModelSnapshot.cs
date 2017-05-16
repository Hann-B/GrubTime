using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using GrubTime.Models;

namespace GrubTime.Migrations
{
    [DbContext(typeof(GrubTimeContext))]
    partial class GrubTimeContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GrubTime.Models.Restaurant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsStarred");

                    b.Property<string>("PlaceId");

                    b.HasKey("Id");

                    b.ToTable("Places");
                });

            modelBuilder.Entity("GrubTime.Models.StarredPlaces", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("PlaceId");

                    b.Property<int?>("ProfileId");

                    b.Property<int?>("StarredPlaceId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ProfileId");

                    b.HasIndex("StarredPlaceId");

                    b.ToTable("StarredPlaces");
                });

            modelBuilder.Entity("GrubTime.Models.UserProfile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Avatar");

                    b.Property<string>("Username")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("GrubTime.Models.StarredPlaces", b =>
                {
                    b.HasOne("GrubTime.Models.UserProfile", "Profile")
                        .WithMany()
                        .HasForeignKey("ProfileId");

                    b.HasOne("GrubTime.Models.Restaurant", "StarredPlace")
                        .WithMany()
                        .HasForeignKey("StarredPlaceId");
                });
        }
    }
}
