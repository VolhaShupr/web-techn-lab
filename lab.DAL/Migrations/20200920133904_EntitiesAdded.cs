using Microsoft.EntityFrameworkCore.Migrations;

namespace lab.DAL.Migrations
{
    public partial class EntitiesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MusInstrumentGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusInstrumentGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MusInstruments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Brand = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    GroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusInstruments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MusInstruments_MusInstrumentGroups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "MusInstrumentGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MusInstruments_GroupId",
                table: "MusInstruments",
                column: "GroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MusInstruments");

            migrationBuilder.DropTable(
                name: "MusInstrumentGroups");
        }
    }
}
