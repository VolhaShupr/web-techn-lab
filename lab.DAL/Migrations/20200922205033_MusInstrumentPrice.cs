using Microsoft.EntityFrameworkCore.Migrations;

namespace lab.DAL.Migrations
{
    public partial class MusInstrumentPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "MusInstruments",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "MusInstruments");
        }
    }
}
