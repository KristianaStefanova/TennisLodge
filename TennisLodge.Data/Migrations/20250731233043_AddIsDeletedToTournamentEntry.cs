using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TennisLodge.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDeletedToTournamentEntry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "TournamentEntries",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "Indicates whether the entry has been soft-deleted");

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 31, 23, 30, 42, 544, DateTimeKind.Utc).AddTicks(5947));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 31, 23, 30, 42, 544, DateTimeKind.Utc).AddTicks(5959));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 31, 23, 30, 42, 544, DateTimeKind.Utc).AddTicks(5963));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 31, 23, 30, 42, 544, DateTimeKind.Utc).AddTicks(5965));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 31, 23, 30, 42, 544, DateTimeKind.Utc).AddTicks(5968));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 31, 23, 30, 42, 544, DateTimeKind.Utc).AddTicks(5973));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 31, 23, 30, 42, 544, DateTimeKind.Utc).AddTicks(5975));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 31, 23, 30, 42, 544, DateTimeKind.Utc).AddTicks(5979));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 31, 23, 30, 42, 544, DateTimeKind.Utc).AddTicks(5982));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 31, 23, 30, 42, 544, DateTimeKind.Utc).AddTicks(5985));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 31, 23, 30, 42, 544, DateTimeKind.Utc).AddTicks(5988));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 31, 23, 30, 42, 544, DateTimeKind.Utc).AddTicks(5990));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 31, 23, 30, 42, 544, DateTimeKind.Utc).AddTicks(5992));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 31, 23, 30, 42, 544, DateTimeKind.Utc).AddTicks(5995));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 31, 23, 30, 42, 544, DateTimeKind.Utc).AddTicks(5998));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 31, 23, 30, 42, 544, DateTimeKind.Utc).AddTicks(6001));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 31, 23, 30, 42, 544, DateTimeKind.Utc).AddTicks(6003));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 31, 23, 30, 42, 544, DateTimeKind.Utc).AddTicks(6025));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 19,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 31, 23, 30, 42, 544, DateTimeKind.Utc).AddTicks(6028));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 20,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 31, 23, 30, 42, 544, DateTimeKind.Utc).AddTicks(6031));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 21,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 31, 23, 30, 42, 544, DateTimeKind.Utc).AddTicks(6033));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 22,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 31, 23, 30, 42, 544, DateTimeKind.Utc).AddTicks(6036));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 23,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 31, 23, 30, 42, 544, DateTimeKind.Utc).AddTicks(6038));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 24,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 31, 23, 30, 42, 544, DateTimeKind.Utc).AddTicks(6041));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7699db7d-964f-4782-8209-d76562e0fece",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e02c605d-8620-426a-af1e-f7b9d4920ef4", "AQAAAAIAAYagAAAAEOHd9o2u1OdUgi02YDZeIsRC5JU29vMzENadV+ZcpdPs2a4d1STbMR59FsBAW9owOQ==", "9cfcfcd0-e7d9-4f5f-a601-c4c7133f004a" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "TournamentEntries");

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 31, 22, 30, 46, 170, DateTimeKind.Utc).AddTicks(9502));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 31, 22, 30, 46, 170, DateTimeKind.Utc).AddTicks(9545));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 31, 22, 30, 46, 170, DateTimeKind.Utc).AddTicks(9555));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 31, 22, 30, 46, 170, DateTimeKind.Utc).AddTicks(9563));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 31, 22, 30, 46, 170, DateTimeKind.Utc).AddTicks(9570));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 31, 22, 30, 46, 170, DateTimeKind.Utc).AddTicks(9582));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 31, 22, 30, 46, 170, DateTimeKind.Utc).AddTicks(9589));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 31, 22, 30, 46, 170, DateTimeKind.Utc).AddTicks(9596));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 31, 22, 30, 46, 170, DateTimeKind.Utc).AddTicks(9604));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 31, 22, 30, 46, 170, DateTimeKind.Utc).AddTicks(9613));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 31, 22, 30, 46, 170, DateTimeKind.Utc).AddTicks(9623));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 31, 22, 30, 46, 170, DateTimeKind.Utc).AddTicks(9630));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 31, 22, 30, 46, 170, DateTimeKind.Utc).AddTicks(9640));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 31, 22, 30, 46, 170, DateTimeKind.Utc).AddTicks(9662));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 31, 22, 30, 46, 170, DateTimeKind.Utc).AddTicks(9669));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 31, 22, 30, 46, 170, DateTimeKind.Utc).AddTicks(9695));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 31, 22, 30, 46, 170, DateTimeKind.Utc).AddTicks(9702));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 31, 22, 30, 46, 170, DateTimeKind.Utc).AddTicks(9712));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 19,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 31, 22, 30, 46, 170, DateTimeKind.Utc).AddTicks(9726));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 20,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 31, 22, 30, 46, 170, DateTimeKind.Utc).AddTicks(9737));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 21,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 31, 22, 30, 46, 170, DateTimeKind.Utc).AddTicks(9744));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 22,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 31, 22, 30, 46, 170, DateTimeKind.Utc).AddTicks(9751));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 23,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 31, 22, 30, 46, 170, DateTimeKind.Utc).AddTicks(9759));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 24,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 31, 22, 30, 46, 170, DateTimeKind.Utc).AddTicks(9765));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7699db7d-964f-4782-8209-d76562e0fece",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "74ecdea5-2599-4e13-b6a5-13920011e766", "AQAAAAIAAYagAAAAEE8G4pJHeyBfc3V5t6IJIQZ0ROCAW4bGha54HkTm9c7alCY7YyCIMVkPf9zYJbSUlg==", "63627209-aec0-4bec-acb7-c68a23d5f3c0" });
        }
    }
}
