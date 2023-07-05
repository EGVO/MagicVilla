using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilla_API.Migrations
{
    /// <inheritdoc />
    public partial class ControlarNullables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreationDate", "UpdateDate" },
                values: new object[] { new DateTime(2023, 6, 20, 8, 37, 18, 974, DateTimeKind.Local).AddTicks(6166), new DateTime(2023, 6, 20, 8, 37, 18, 974, DateTimeKind.Local).AddTicks(6185) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreationDate", "UpdateDate" },
                values: new object[] { new DateTime(2023, 6, 20, 8, 37, 18, 974, DateTimeKind.Local).AddTicks(6188), new DateTime(2023, 6, 20, 8, 37, 18, 974, DateTimeKind.Local).AddTicks(6189) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreationDate", "UpdateDate" },
                values: new object[] { new DateTime(2023, 6, 14, 18, 38, 36, 911, DateTimeKind.Local).AddTicks(6742), new DateTime(2023, 6, 14, 18, 38, 36, 911, DateTimeKind.Local).AddTicks(6766) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreationDate", "UpdateDate" },
                values: new object[] { new DateTime(2023, 6, 14, 18, 38, 36, 911, DateTimeKind.Local).AddTicks(6773), new DateTime(2023, 6, 14, 18, 38, 36, 911, DateTimeKind.Local).AddTicks(6775) });
        }
    }
}
