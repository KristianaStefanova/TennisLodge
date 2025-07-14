using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TennisLodge.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOfQueryFilter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7699db7d-964f-4782-8209-d76562e0fece",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4d2d24cc-4fd3-4e75-a94f-194ae5778614", "AQAAAAIAAYagAAAAEA+H/9CsJXDgvDVR9SSq9VLj64RDFsxGb5Fmeg6JTrUeDG4ACXV4g7oQ8EN82HyeJA==", "48cbe3f6-a5ba-4c94-be4d-04244d9b25ce" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7699db7d-964f-4782-8209-d76562e0fece",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3005770e-63e1-4bf2-bfad-daf79b3c2abc", "AQAAAAIAAYagAAAAEPrKcgCklpp30Wz6JHKPGae38D3XIrGaJtO3y8owhYzrX6JYYjd2VCJbPwntMQigPQ==", "a391e11b-5bef-49fe-ba6c-867af54722e6" });
        }
    }
}
