using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TennisLodge.Data.Migrations
{
    /// <inheritdoc />
    public partial class ExtendAccommodationEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Accommodations",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "City where the accommodation is located",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "City where the accommodation is located");

            migrationBuilder.AddColumn<DateTime>(
                name: "AvailableFrom",
                table: "Accommodations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "Start of availability window");

            migrationBuilder.AddColumn<DateTime>(
                name: "AvailableTo",
                table: "Accommodations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "End of availability window");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7699db7d-964f-4782-8209-d76562e0fece",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "64f62977-a642-4d44-96bd-2cefcbd1545d", "AQAAAAIAAYagAAAAEHBGMh4LcB3pV5f4U9u7ndkH9nAcgNoQSq+VaGoKBhbvqmHEkB8I2bLig6JjrVVKsg==", "596f7cec-2e52-4ad1-907b-4533861558db" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvailableFrom",
                table: "Accommodations");

            migrationBuilder.DropColumn(
                name: "AvailableTo",
                table: "Accommodations");

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Accommodations",
                type: "nvarchar(max)",
                nullable: false,
                comment: "City where the accommodation is located",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldComment: "City where the accommodation is located");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7699db7d-964f-4782-8209-d76562e0fece",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3b8e348b-6f81-4464-b718-6c532d1b0e81", "AQAAAAIAAYagAAAAEB8cHYKyD+1IQRsarQnH0B4/3LkjFt4D8snuy/tMtwJBPI9hIYVDkAscwV8domfgtA==", "1cb04558-e9b5-408d-a832-c03bbbcd7913" });
        }
    }
}
