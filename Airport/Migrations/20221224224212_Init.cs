using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Airport.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Planes",
                columns: table => new
                {
                    PlaneName = table.Column<string>(type: "TEXT", nullable: false),
                    Destination = table.Column<string>(type: "TEXT", nullable: false),
                    StationId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planes", x => x.PlaneName);
                });

            migrationBuilder.CreateTable(
                name: "Stations",
                columns: table => new
                {
                    StationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PlaneName = table.Column<string>(type: "TEXT", nullable: true),
                    StationName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stations", x => x.StationId);
                    table.ForeignKey(
                        name: "FK_Stations_Planes_PlaneName",
                        column: x => x.PlaneName,
                        principalTable: "Planes",
                        principalColumn: "PlaneName");
                });

            migrationBuilder.InsertData(
                table: "Stations",
                columns: new[] { "StationId", "PlaneName", "StationName" },
                values: new object[,]
                {
                    { 1, null, "Landing Stage 1" },
                    { 2, null, "Landing Stage 2" },
                    { 3, null, "Landing Stage 3" },
                    { 4, null, "Runway" },
                    { 5, null, "Arrival Path" },
                    { 6, null, "Terminal 1" },
                    { 7, null, "Terminal 2" },
                    { 8, null, "Departure Path" },
                    { 9, null, "Take Off" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Planes_StationId",
                table: "Planes",
                column: "StationId");

            migrationBuilder.CreateIndex(
                name: "IX_Stations_PlaneName",
                table: "Stations",
                column: "PlaneName",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Planes_Stations_StationId",
                table: "Planes",
                column: "StationId",
                principalTable: "Stations",
                principalColumn: "StationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Planes_Stations_StationId",
                table: "Planes");

            migrationBuilder.DropTable(
                name: "Stations");

            migrationBuilder.DropTable(
                name: "Planes");
        }
    }
}
