using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TennisLodge.Data.Migrations
{
    /// <inheritdoc />
    public partial class MakeAccommodationSoftDeletable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Accommodations",
                type: "datetime2",
                nullable: true,
                comment: "Date when the accommodation was soft-deleted");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Accommodations",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "Indicates if the accommodation has been soft-deleted");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7699db7d-964f-4782-8209-d76562e0fece",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "96880a2d-07b2-4ef5-908f-b05fecf20edc", "AQAAAAIAAYagAAAAEPdqkLKfY/nrVHGHZsHyqXtYcwPO9zZqYi9ef3BVR4lljsZ+tbFjaxTRR3JU6dX6QQ==", "30ecf751-8c37-4e60-a31c-16f6aaee64df" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Accommodations");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Accommodations");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7699db7d-964f-4782-8209-d76562e0fece",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "64f62977-a642-4d44-96bd-2cefcbd1545d", "AQAAAAIAAYagAAAAEHBGMh4LcB3pV5f4U9u7ndkH9nAcgNoQSq+VaGoKBhbvqmHEkB8I2bLig6JjrVVKsg==", "596f7cec-2e52-4ad1-907b-4533861558db" });
        }
    }
}
