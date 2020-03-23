using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AspNetCoreSpa.Web.Migrations
{
    public partial class DBInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prices_TouristTypes_TouristTypeID",
                table: "Prices");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "TourBookings");

            migrationBuilder.RenameColumn(
                name: "TouristTypeID",
                table: "Prices",
                newName: "TouristTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Prices_TouristTypeID",
                table: "Prices",
                newName: "IX_Prices_TouristTypeId");

            migrationBuilder.AlterColumn<bool>(
                name: "Censorship",
                table: "Tours",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ProvinceId",
                table: "Tours",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProvinceId",
                table: "TourPrograms",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "TourBookings",
                type: "NVARCHAR(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(100)");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "TourBookings",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "TourBookings",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "TourBookings",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "TourBookings",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "TourBookings",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "TourBookings",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "TourBookings",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Province",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "NVARCHAR(100)", nullable: true),
                    Longitude = table.Column<decimal>(nullable: false),
                    Latitude = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Province", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tours_ProvinceId",
                table: "Tours",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_TourPrograms_ProvinceId",
                table: "TourPrograms",
                column: "ProvinceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prices_TouristTypes_TouristTypeId",
                table: "Prices",
                column: "TouristTypeId",
                principalTable: "TouristTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TourPrograms_Province_ProvinceId",
                table: "TourPrograms",
                column: "ProvinceId",
                principalTable: "Province",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tours_Province_ProvinceId",
                table: "Tours",
                column: "ProvinceId",
                principalTable: "Province",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prices_TouristTypes_TouristTypeId",
                table: "Prices");

            migrationBuilder.DropForeignKey(
                name: "FK_TourPrograms_Province_ProvinceId",
                table: "TourPrograms");

            migrationBuilder.DropForeignKey(
                name: "FK_Tours_Province_ProvinceId",
                table: "Tours");

            migrationBuilder.DropTable(
                name: "Province");

            migrationBuilder.DropIndex(
                name: "IX_Tours_ProvinceId",
                table: "Tours");

            migrationBuilder.DropIndex(
                name: "IX_TourPrograms_ProvinceId",
                table: "TourPrograms");

            migrationBuilder.DropColumn(
                name: "ProvinceId",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "ProvinceId",
                table: "TourPrograms");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "TourBookings");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "TourBookings");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "TourBookings");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "TourBookings");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "TourBookings");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "TourBookings");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "TourBookings");

            migrationBuilder.RenameColumn(
                name: "TouristTypeId",
                table: "Prices",
                newName: "TouristTypeID");

            migrationBuilder.RenameIndex(
                name: "IX_Prices_TouristTypeId",
                table: "Prices",
                newName: "IX_Prices_TouristTypeID");

            migrationBuilder.AlterColumn<int>(
                name: "Censorship",
                table: "Tours",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "TourBookings",
                type: "NVARCHAR(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(100)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "TourBookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Prices_TouristTypes_TouristTypeID",
                table: "Prices",
                column: "TouristTypeID",
                principalTable: "TouristTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
