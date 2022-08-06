using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CryptoPriceTracking.API.Migrations
{
    public partial class AddCloseTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CloseDate",
                table: "Cryptos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CloseDate",
                table: "Cryptos");
        }
    }
}
