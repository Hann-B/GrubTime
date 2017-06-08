using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using GrubTime.Models;

namespace GrubTime.Migrations
{
    [DbContext(typeof(GrubTimeContext))]
    [Migration("20170526200607_PlacesAndFavorites")]
    partial class PlacesAndFavorites
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GrubTime.Models.StarredPlaces", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsStarred");

                    b.Property<string>("PlaceId");

                    b.Property<string>("UserToken");

                    b.HasKey("Id");

                    b.ToTable("StarredPlaces");
                });
        }
    }
}
