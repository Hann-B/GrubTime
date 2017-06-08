using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using GrubTime.Models;

namespace GrubTime.Migrations
{
    [DbContext(typeof(GrubTimeContext))]
    [Migration("20170526181346_TableStructure")]
    partial class TableStructure
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
