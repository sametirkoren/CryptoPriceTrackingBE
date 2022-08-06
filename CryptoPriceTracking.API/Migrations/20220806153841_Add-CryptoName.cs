using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CryptoPriceTracking.API.Migrations
{
    public partial class AddCryptoName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Cryptos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Cryptos");
        }
    }
}
