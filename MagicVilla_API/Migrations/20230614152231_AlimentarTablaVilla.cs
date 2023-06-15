using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilla_API.Migrations
{
    /// <inheritdoc />
    public partial class AlimentarTablaVilla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenity", "CreationDate", "Detail", "Name", "Occupants", "Rate", "SquareMeter", "UpdateDate", "UrlImage" },
                values: new object[,]
                {
                    { 1, "", new DateTime(2023, 6, 14, 10, 22, 31, 153, DateTimeKind.Local).AddTicks(2490), "Detalle de la Villa...", "Villa Real", 5, 200.0, 50, new DateTime(2023, 6, 14, 10, 22, 31, 153, DateTimeKind.Local).AddTicks(2507), "" },
                    { 2, "", new DateTime(2023, 6, 14, 10, 22, 31, 153, DateTimeKind.Local).AddTicks(2511), "Detalle de la Villa...", "Premium Vista a la Piscina", 4, 150.0, 40, new DateTime(2023, 6, 14, 10, 22, 31, 153, DateTimeKind.Local).AddTicks(2512), "" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
