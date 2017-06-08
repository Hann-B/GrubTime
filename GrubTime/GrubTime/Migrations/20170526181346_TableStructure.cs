using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GrubTime.Migrations
{
    public partial class TableStructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StarredPlaces_Profiles_ProfileId",
                table: "StarredPlaces");

            migrationBuilder.DropForeignKey(
                name: "FK_StarredPlaces_Places_StarredPlaceId",
                table: "StarredPlaces");

            migrationBuilder.DropTable(
                name: "Places");

            migrationBuilder.DropTable(
                name: "Profiles");

            migrationBuilder.DropIndex(
                name: "IX_StarredPlaces_ProfileId",
                table: "StarredPlaces");

            migrationBuilder.DropIndex(
                name: "IX_StarredPlaces_StarredPlaceId",
                table: "StarredPlaces");

            migrationBuilder.DropColumn(
                name: "ProfileId",
                table: "StarredPlaces");

            migrationBuilder.DropColumn(
                name: "StarredPlaceId",
                table: "StarredPlaces");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "StarredPlaces");

            migrationBuilder.AlterColumn<string>(
                name: "PlaceId",
                table: "StarredPlaces",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<bool>(
                name: "IsStarred",
                table: "StarredPlaces",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserToken",
                table: "StarredPlaces",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsStarred",
                table: "StarredPlaces");

            migrationBuilder.DropColumn(
                name: "UserToken",
                table: "StarredPlaces");

            migrationBuilder.AlterColumn<int>(
                name: "PlaceId",
                table: "StarredPlaces",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProfileId",
                table: "StarredPlaces",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StarredPlaceId",
                table: "StarredPlaces",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "StarredPlaces",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Places",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsStarred = table.Column<bool>(nullable: false),
                    PlaceId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Places", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Avatar = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StarredPlaces_ProfileId",
                table: "StarredPlaces",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_StarredPlaces_StarredPlaceId",
                table: "StarredPlaces",
                column: "StarredPlaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_StarredPlaces_Profiles_ProfileId",
                table: "StarredPlaces",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StarredPlaces_Places_StarredPlaceId",
                table: "StarredPlaces",
                column: "StarredPlaceId",
                principalTable: "Places",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
