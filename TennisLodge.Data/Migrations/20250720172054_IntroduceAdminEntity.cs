using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TennisLodge.Data.Migrations
{
    /// <inheritdoc />
    public partial class IntroduceAdminEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdminId",
                table: "Tournaments",
                type: "nvarchar(450)",
                nullable: true,
                comment: "Tournament's admin");

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Admin identifier"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Admin's user entity")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Admins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Admin user in the system");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7699db7d-964f-4782-8209-d76562e0fece",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3b8e348b-6f81-4464-b718-6c532d1b0e81", "AQAAAAIAAYagAAAAEB8cHYKyD+1IQRsarQnH0B4/3LkjFt4D8snuy/tMtwJBPI9hIYVDkAscwV8domfgtA==", "1cb04558-e9b5-408d-a832-c03bbbcd7913" });

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: new Guid("0ceea4f9-b7bc-4d18-90cf-3b64b06c2a1f"),
                column: "AdminId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: new Guid("7b6d0baf-2a39-41d7-bc2a-e5713b302baf"),
                column: "AdminId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: new Guid("8f19c979-40c2-4cb8-8af0-d061456245bd"),
                column: "AdminId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: new Guid("90bb0d0a-20ae-4952-804b-bb9e3bcecb18"),
                column: "AdminId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: new Guid("a1f0c8c7-f4b3-4eb9-82e3-4f8cbf2066ee"),
                column: "AdminId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: new Guid("a5f6700a-3e08-430b-9462-2b2f61d31af2"),
                column: "AdminId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: new Guid("a9f5d6c3-7d5a-4a47-95ed-31080b39a04b"),
                column: "AdminId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: new Guid("b5d3462f-7f51-4382-b57c-cc1464a68f1f"),
                column: "AdminId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: new Guid("c909f34e-8126-42c5-9e65-8d9c0ff0db96"),
                column: "AdminId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: new Guid("d27a295f-2b7c-4fc1-9113-89672539e12c"),
                column: "AdminId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: new Guid("dff35c76-7d20-4203-aaa1-7cb5117fd9f7"),
                column: "AdminId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: new Guid("e0c9381f-f3ff-48cc-8719-30a4c649844a"),
                column: "AdminId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: new Guid("e260f1e1-b12b-4e2b-87c8-e2df1f388377"),
                column: "AdminId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: new Guid("ea09c21c-62f5-4ee2-99bb-d63f682c5ee3"),
                column: "AdminId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: new Guid("f540e688-b37a-4061-9bc4-0d4b1bdbd1e6"),
                column: "AdminId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: new Guid("fe9804cb-0ae2-42b6-a1fd-d90c0bc880ec"),
                column: "AdminId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_AdminId",
                table: "Tournaments",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Admins_UserId",
                table: "Admins",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tournaments_Admins_AdminId",
                table: "Tournaments",
                column: "AdminId",
                principalTable: "Admins",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tournaments_Admins_AdminId",
                table: "Tournaments");

            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropIndex(
                name: "IX_Tournaments_AdminId",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "Tournaments");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7699db7d-964f-4782-8209-d76562e0fece",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4d2d24cc-4fd3-4e75-a94f-194ae5778614", "AQAAAAIAAYagAAAAEA+H/9CsJXDgvDVR9SSq9VLj64RDFsxGb5Fmeg6JTrUeDG4ACXV4g7oQ8EN82HyeJA==", "48cbe3f6-a5ba-4c94-be4d-04244d9b25ce" });
        }
    }
}
