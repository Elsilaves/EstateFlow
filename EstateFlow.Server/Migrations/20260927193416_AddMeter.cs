using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EstateFlow.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddMeter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Meter",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: false),
                    SerialNumber = table.Column<string>(type: "text", nullable: false),
                    PropertyId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Meter_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Meter",
                columns: new[] { "Id", "Location", "Name", "PropertyId", "SerialNumber" },
                values: new object[,]
                {
                    { new Guid("1f8fad5b-d9cb-469f-a165-70867728950e"), "Basement", "Electricity Meter", new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"), "ELEC123456" },
                    { new Guid("3c9e6679-7425-40de-944b-e07456789012"), "Utility Room", "Water Meter", new Guid("7c9e6679-7425-40de-944b-e07456789012"), "WATR654321" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Meter_PropertyId",
                table: "Meter",
                column: "PropertyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Meter");
        }
    }
}
