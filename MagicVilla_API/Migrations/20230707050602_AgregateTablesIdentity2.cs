using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilla_API.Migrations
{
    /// <inheritdoc />
    public partial class AgregateTablesIdentity2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreationDate", "UpdateDate" },
                values: new object[] { new DateTime(2023, 7, 7, 0, 6, 2, 148, DateTimeKind.Local).AddTicks(4744), new DateTime(2023, 7, 7, 0, 6, 2, 148, DateTimeKind.Local).AddTicks(4755) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreationDate", "UpdateDate" },
                values: new object[] { new DateTime(2023, 7, 7, 0, 6, 2, 148, DateTimeKind.Local).AddTicks(4757), new DateTime(2023, 7, 7, 0, 6, 2, 148, DateTimeKind.Local).AddTicks(4758) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreationDate", "UpdateDate" },
                values: new object[] { new DateTime(2023, 7, 6, 23, 56, 18, 126, DateTimeKind.Local).AddTicks(1777), new DateTime(2023, 7, 6, 23, 56, 18, 126, DateTimeKind.Local).AddTicks(1788) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreationDate", "UpdateDate" },
                values: new object[] { new DateTime(2023, 7, 6, 23, 56, 18, 126, DateTimeKind.Local).AddTicks(1790), new DateTime(2023, 7, 6, 23, 56, 18, 126, DateTimeKind.Local).AddTicks(1791) });
        }
    }
}
