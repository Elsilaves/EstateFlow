using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EstateFlow.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddDomainEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meter_Properties_PropertyId",
                table: "Meter");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Meter",
                table: "Meter");

            migrationBuilder.DeleteData(
                table: "Meter",
                keyColumn: "Id",
                keyColumnType: "uuid",
                keyValue: new Guid("1f8fad5b-d9cb-469f-a165-70867728950e"));

            migrationBuilder.DeleteData(
                table: "Meter",
                keyColumn: "Id",
                keyColumnType: "uuid",
                keyValue: new Guid("3c9e6679-7425-40de-944b-e07456789012"));

            migrationBuilder.RenameTable(
                name: "Meter",
                newName: "Meters");

            migrationBuilder.RenameIndex(
                name: "IX_Meter_PropertyId",
                table: "Meters",
                newName: "IX_Meters_PropertyId");

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Meters",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProviderId",
                table: "Meters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ReadingPeriod",
                table: "Meters",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UtilityTypeId",
                table: "Meters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Meters",
                table: "Meters",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "MeterReadings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MesuredAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Value = table.Column<decimal>(type: "numeric(18,3)", precision: 18, scale: 3, nullable: false),
                    ReportedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ApprovedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PhotoPath = table.Column<string>(type: "text", nullable: true),
                    MeterId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeterReadings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeterReadings_Meters_MeterId",
                        column: x => x.MeterId,
                        principalTable: "Meters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tenants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UtilityTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UtilityTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rentals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    MonthlyRent = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    PropertyId = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rentals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rentals_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rentals_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Providers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: true),
                    UtilityTypeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Providers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Providers_UtilityTypes_UtilityTypeId",
                        column: x => x.UtilityTypeId,
                        principalTable: "UtilityTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Tenants",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "PhoneNumber" },
                values: new object[,]
                {
                    { new Guid("5f2504e0-4f89-41d3-9a0c-0305e82c3307"), "john.doe@example.com", "John", "Doe", null },
                    { new Guid("5f2504e0-4f89-41d3-9a0c-0305e82c3308"), "jane.smith@example.com", "Jane", "Smith", "+00 00 123 4567" }
                });

            migrationBuilder.InsertData(
                table: "UtilityTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Electricity" },
                    { 2, "Water" },
                    { 3, "Gas" }
                });

            migrationBuilder.InsertData(
                table: "Providers",
                columns: new[] { "Id", "Name", "Note", "UtilityTypeId" },
                values: new object[,]
                {
                    { 1, "Electricity Provider A", null, 1 },
                    { 2, "Water Provider B", null, 2 },
                    { 3, "Gas Provider C", null, 3 }
                });

            migrationBuilder.InsertData(
                table: "Rentals",
                columns: new[] { "Id", "EndDate", "MonthlyRent", "PropertyId", "StartDate", "TenantId" },
                values: new object[,]
                {
                    { new Guid("6f2504e0-4f89-41d3-9a0c-0305e82c3309"), new DateTime(2026, 8, 31, 0, 0, 0, 0, DateTimeKind.Utc), 120000m, new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"), new DateTime(2025, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("5f2504e0-4f89-41d3-9a0c-0305e82c3307") },
                    { new Guid("6f2504e0-4f89-41d3-9a0c-0305e82c3310"), null, 150000m, new Guid("7c9e6679-7425-40de-944b-e07456789012"), new DateTime(2026, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("5f2504e0-4f89-41d3-9a0c-0305e82c3308") }
                });

            migrationBuilder.InsertData(
                table: "Meters",
                columns: new[] { "Id", "Location", "Name", "Note", "PropertyId", "ProviderId", "ReadingPeriod", "SerialNumber", "UtilityTypeId" },
                values: new object[,]
                {
                    { new Guid("3f2504e0-4f89-41d3-9a0c-0305e82c3301"), "Living Room", "Electricity Meter", "Need to be validated in 2007", new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"), 1, "Monthly", "ELEC-001", 1 },
                    { new Guid("3f2504e0-4f89-41d3-9a0c-0305e82c3302"), "Kitchen", "Water Meter", "Need to be validated in 2007", new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"), 2, "Quarterly", "WATER-001", 2 },
                    { new Guid("3f2504e0-4f89-41d3-9a0c-0305e82c3303"), "Garage", "Gas Meter", "Need to be validated in 2007 ", new Guid("7c9e6679-7425-40de-944b-e07456789012"), 3, null, "GAS-001", 3 }
                });

            migrationBuilder.InsertData(
                table: "MeterReadings",
                columns: new[] { "Id", "ApprovedAt", "MesuredAt", "MeterId", "PhotoPath", "ReportedAt", "Value" },
                values: new object[,]
                {
                    { new Guid("4f2504e0-4f89-41d3-9a0c-0305e82c3304"), new DateTime(2026, 6, 1, 9, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 6, 1, 7, 40, 0, 0, DateTimeKind.Utc), new Guid("3f2504e0-4f89-41d3-9a0c-0305e82c3301"), "uploads/meter_readings/electricity_reading_2026-06-01.jpg", new DateTime(2026, 6, 1, 8, 0, 0, 0, DateTimeKind.Utc), 120.5m },
                    { new Guid("4f2504e0-4f89-41d3-9a0c-0305e82c3305"), new DateTime(2026, 6, 1, 9, 5, 0, 0, DateTimeKind.Utc), new DateTime(2026, 6, 1, 7, 45, 0, 0, DateTimeKind.Utc), new Guid("3f2504e0-4f89-41d3-9a0c-0305e82c3302"), "uploads/meter_readings/water_reading_2026-06-01.jpg", new DateTime(2026, 6, 1, 8, 5, 0, 0, DateTimeKind.Utc), 85.2m },
                    { new Guid("4f2504e0-4f89-41d3-9a0c-0305e82c3306"), new DateTime(2026, 6, 1, 11, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 6, 1, 10, 15, 0, 0, DateTimeKind.Utc), new Guid("3f2504e0-4f89-41d3-9a0c-0305e82c3303"), "uploads/meter_readings/gas_reading_2026-06-01.jpg", new DateTime(2026, 6, 1, 10, 30, 0, 0, DateTimeKind.Utc), 65.8m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Meters_ProviderId",
                table: "Meters",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_Meters_UtilityTypeId",
                table: "Meters",
                column: "UtilityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MeterReadings_MeterId",
                table: "MeterReadings",
                column: "MeterId");

            migrationBuilder.CreateIndex(
                name: "IX_Providers_UtilityTypeId",
                table: "Providers",
                column: "UtilityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_PropertyId",
                table: "Rentals",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_TenantId",
                table: "Rentals",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Meters_Properties_PropertyId",
                table: "Meters",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Meters_Providers_ProviderId",
                table: "Meters",
                column: "ProviderId",
                principalTable: "Providers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Meters_UtilityTypes_UtilityTypeId",
                table: "Meters",
                column: "UtilityTypeId",
                principalTable: "UtilityTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meters_Properties_PropertyId",
                table: "Meters");

            migrationBuilder.DropForeignKey(
                name: "FK_Meters_Providers_ProviderId",
                table: "Meters");

            migrationBuilder.DropForeignKey(
                name: "FK_Meters_UtilityTypes_UtilityTypeId",
                table: "Meters");

            migrationBuilder.DropTable(
                name: "MeterReadings");

            migrationBuilder.DropTable(
                name: "Providers");

            migrationBuilder.DropTable(
                name: "Rentals");

            migrationBuilder.DropTable(
                name: "UtilityTypes");

            migrationBuilder.DropTable(
                name: "Tenants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Meters",
                table: "Meters");

            migrationBuilder.DropIndex(
                name: "IX_Meters_ProviderId",
                table: "Meters");

            migrationBuilder.DropIndex(
                name: "IX_Meters_UtilityTypeId",
                table: "Meters");

            migrationBuilder.DeleteData(
                table: "Meters",
                keyColumn: "Id",
                keyValue: new Guid("3f2504e0-4f89-41d3-9a0c-0305e82c3301"));

            migrationBuilder.DeleteData(
                table: "Meters",
                keyColumn: "Id",
                keyValue: new Guid("3f2504e0-4f89-41d3-9a0c-0305e82c3302"));

            migrationBuilder.DeleteData(
                table: "Meters",
                keyColumn: "Id",
                keyValue: new Guid("3f2504e0-4f89-41d3-9a0c-0305e82c3303"));

            migrationBuilder.DropColumn(
                name: "Note",
                table: "Meters");

            migrationBuilder.DropColumn(
                name: "ProviderId",
                table: "Meters");

            migrationBuilder.DropColumn(
                name: "ReadingPeriod",
                table: "Meters");

            migrationBuilder.DropColumn(
                name: "UtilityTypeId",
                table: "Meters");

            migrationBuilder.RenameTable(
                name: "Meters",
                newName: "Meter");

            migrationBuilder.RenameIndex(
                name: "IX_Meters_PropertyId",
                table: "Meter",
                newName: "IX_Meter_PropertyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Meter",
                table: "Meter",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Meter",
                columns: new[] { "Id", "Location", "Name", "PropertyId", "SerialNumber" },
                values: new object[,]
                {
                    { new Guid("1f8fad5b-d9cb-469f-a165-70867728950e"), "Basement", "Electricity Meter", new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"), "ELEC123456" },
                    { new Guid("3c9e6679-7425-40de-944b-e07456789012"), "Utility Room", "Water Meter", new Guid("7c9e6679-7425-40de-944b-e07456789012"), "WATR654321" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Meter_Properties_PropertyId",
                table: "Meter",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
