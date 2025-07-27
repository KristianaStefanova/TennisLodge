using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TennisLodge.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTournamentEntry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TournamentEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Primary key of the tournament entry")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Foreign key to the player (ApplicationUser)"),
                    TournamentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Foreign key to the tournament"),
                    RegisteredOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()", comment: "Date when the player registered for the tournament")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TournamentEntries_AspNetUsers_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TournamentEntries_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Represents a player's participation in a tournament");

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 27, 18, 12, 43, 644, DateTimeKind.Utc).AddTicks(258));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 27, 18, 12, 43, 644, DateTimeKind.Utc).AddTicks(275));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 27, 18, 12, 43, 644, DateTimeKind.Utc).AddTicks(280));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 27, 18, 12, 43, 644, DateTimeKind.Utc).AddTicks(284));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 27, 18, 12, 43, 644, DateTimeKind.Utc).AddTicks(290));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 27, 18, 12, 43, 644, DateTimeKind.Utc).AddTicks(296));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 27, 18, 12, 43, 644, DateTimeKind.Utc).AddTicks(300));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 27, 18, 12, 43, 644, DateTimeKind.Utc).AddTicks(303));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 27, 18, 12, 43, 644, DateTimeKind.Utc).AddTicks(306));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 27, 18, 12, 43, 644, DateTimeKind.Utc).AddTicks(311));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 27, 18, 12, 43, 644, DateTimeKind.Utc).AddTicks(314));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 27, 18, 12, 43, 644, DateTimeKind.Utc).AddTicks(320));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 27, 18, 12, 43, 644, DateTimeKind.Utc).AddTicks(323));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 27, 18, 12, 43, 644, DateTimeKind.Utc).AddTicks(327));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 27, 18, 12, 43, 644, DateTimeKind.Utc).AddTicks(330));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 27, 18, 12, 43, 644, DateTimeKind.Utc).AddTicks(333));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 27, 18, 12, 43, 644, DateTimeKind.Utc).AddTicks(337));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 27, 18, 12, 43, 644, DateTimeKind.Utc).AddTicks(341));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 19,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 27, 18, 12, 43, 644, DateTimeKind.Utc).AddTicks(344));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 20,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 27, 18, 12, 43, 644, DateTimeKind.Utc).AddTicks(348));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 21,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 27, 18, 12, 43, 644, DateTimeKind.Utc).AddTicks(351));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 22,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 27, 18, 12, 43, 644, DateTimeKind.Utc).AddTicks(354));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 23,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 27, 18, 12, 43, 644, DateTimeKind.Utc).AddTicks(357));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 24,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 27, 18, 12, 43, 644, DateTimeKind.Utc).AddTicks(361));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7699db7d-964f-4782-8209-d76562e0fece",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "59dad657-9064-49bb-be08-d42a1970be5c", "AQAAAAIAAYagAAAAEFcE5u8NJPlccBirxD+C9YhlBHAvCM/WcMU9zYrjgQn1QUe+kVL7FlyE29EumuqLNg==", "d643ad7f-a071-4d15-901a-9ae3da3fd1ff" });

            migrationBuilder.CreateIndex(
                name: "IX_TournamentEntries_PlayerId",
                table: "TournamentEntries",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentEntries_TournamentId",
                table: "TournamentEntries",
                column: "TournamentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TournamentEntries");

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 23, 18, 29, 47, 287, DateTimeKind.Utc).AddTicks(4947));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 23, 18, 29, 47, 287, DateTimeKind.Utc).AddTicks(4956));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 23, 18, 29, 47, 287, DateTimeKind.Utc).AddTicks(4958));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 23, 18, 29, 47, 287, DateTimeKind.Utc).AddTicks(4961));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 23, 18, 29, 47, 287, DateTimeKind.Utc).AddTicks(4963));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 23, 18, 29, 47, 287, DateTimeKind.Utc).AddTicks(4966));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 23, 18, 29, 47, 287, DateTimeKind.Utc).AddTicks(4968));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 23, 18, 29, 47, 287, DateTimeKind.Utc).AddTicks(4970));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 23, 18, 29, 47, 287, DateTimeKind.Utc).AddTicks(4972));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 23, 18, 29, 47, 287, DateTimeKind.Utc).AddTicks(4975));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 23, 18, 29, 47, 287, DateTimeKind.Utc).AddTicks(4977));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 23, 18, 29, 47, 287, DateTimeKind.Utc).AddTicks(4980));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 23, 18, 29, 47, 287, DateTimeKind.Utc).AddTicks(4982));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 23, 18, 29, 47, 287, DateTimeKind.Utc).AddTicks(4984));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 23, 18, 29, 47, 287, DateTimeKind.Utc).AddTicks(4986));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 23, 18, 29, 47, 287, DateTimeKind.Utc).AddTicks(4987));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 23, 18, 29, 47, 287, DateTimeKind.Utc).AddTicks(4989));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 23, 18, 29, 47, 287, DateTimeKind.Utc).AddTicks(4992));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 19,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 23, 18, 29, 47, 287, DateTimeKind.Utc).AddTicks(4994));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 20,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 23, 18, 29, 47, 287, DateTimeKind.Utc).AddTicks(4995));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 21,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 23, 18, 29, 47, 287, DateTimeKind.Utc).AddTicks(4997));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 22,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 23, 18, 29, 47, 287, DateTimeKind.Utc).AddTicks(4999));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 23,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 23, 18, 29, 47, 287, DateTimeKind.Utc).AddTicks(5001));

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 24,
                column: "CreatedOn",
                value: new DateTime(2025, 7, 23, 18, 29, 47, 287, DateTimeKind.Utc).AddTicks(5003));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7699db7d-964f-4782-8209-d76562e0fece",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2b8f24c8-fef6-4f06-bd1d-b9faca6f580c", "AQAAAAIAAYagAAAAEDF2HSfu0T8KtCIwBUZa1DrAVFPh0rzHDtZtGUjFcnl08WXyBgS9DZYrXpsUQiMz2w==", "d4bdac8a-e315-41a6-8b5b-a0e6a08a1005" });
        }
    }
}
