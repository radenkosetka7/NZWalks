using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDataForDifficultiresandRegions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0215db79-fab9-4f35-bb0e-d1024a495603"), "Moderate" },
                    { new Guid("6fd20390-1c46-4521-998e-59f6d5deb7dd"), "Hard" },
                    { new Guid("b5767e41-a322-4a3a-9e45-9be5b889fb51"), "Easy" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("5bfb536b-b3ac-435e-99e0-246bdfb2e98f"), "WGN", "Wellington", null },
                    { new Guid("767d81d0-b660-4318-9e18-8b36e21c7912"), "AKL", "Auckland", null },
                    { new Guid("ae091cb5-4549-4939-a0f4-8cb5138aef0c"), "NTL", "Nothland", null },
                    { new Guid("aedc2547-5877-4324-9663-8c3cddccfbaf"), "NSN", "Nelson", null },
                    { new Guid("d8822eb4-6db0-4419-a067-4865a509eb5a"), "STL", "Southland", null },
                    { new Guid("ebaf65d4-1751-46b9-a3c9-b96ecfbb1b2a"), "BOP", "Bay Of Plenty", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("0215db79-fab9-4f35-bb0e-d1024a495603"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("6fd20390-1c46-4521-998e-59f6d5deb7dd"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("b5767e41-a322-4a3a-9e45-9be5b889fb51"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("5bfb536b-b3ac-435e-99e0-246bdfb2e98f"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("767d81d0-b660-4318-9e18-8b36e21c7912"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("ae091cb5-4549-4939-a0f4-8cb5138aef0c"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("aedc2547-5877-4324-9663-8c3cddccfbaf"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("d8822eb4-6db0-4419-a067-4865a509eb5a"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("ebaf65d4-1751-46b9-a3c9-b96ecfbb1b2a"));
        }
    }
}
