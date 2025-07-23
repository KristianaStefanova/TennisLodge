using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TennisLodge.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialAccommodations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Accommodations",
                columns: new[] { "Id", "Address", "AvailableFrom", "AvailableTo", "City", "CreatedOn", "DeletedOn", "HostUserId", "IsAvailable", "IsDeleted", "MaxGuests", "Notes" },
                values: new object[,]
                {
                    { 1, "булевард Цариградско шосе 101, Sofia", new DateTime(2026, 1, 5, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Sofia", new DateTime(2025, 7, 23, 18, 29, 47, 287, DateTimeKind.Utc).AddTicks(4947), null, "31b0fae9-597d-46ce-95be-8c6e51cccf92", true, false, 3, "Подходящо за млади спортисти с придружители." },
                    { 2, "ул. Васил Левски 15, Plovdiv", new DateTime(2026, 1, 7, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Plovdiv", new DateTime(2025, 7, 23, 18, 29, 47, 287, DateTimeKind.Utc).AddTicks(4956), null, "31b0fae9-597d-46ce-95be-8c6e51cccf92", true, false, 1, "Самостоятелна стая с баня, идеална за по-големи спортисти." },
                    { 3, "булевард Княгиня Мария Луиза 45, Varna", new DateTime(2026, 1, 5, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Varna", new DateTime(2025, 7, 23, 18, 29, 47, 287, DateTimeKind.Utc).AddTicks(4958), null, "fb39c1e3-3f7e-4f69-b0c7-ab7d0854b902", true, false, 2, "Намира се близо до спортен комплекс, с Wi-Fi и кухня." },
                    { 4, "ул. Христо Ботев 22, Burgas", new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 12, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Burgas", new DateTime(2025, 7, 23, 18, 29, 47, 287, DateTimeKind.Utc).AddTicks(4961), null, "fb39c1e3-3f7e-4f69-b0c7-ab7d0854b902", true, false, 4, "Идеално за семейства и треньори, близо до плажа." },
                    { 5, "ул. Съборна 7, Ruse", new DateTime(2025, 11, 26, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Ruse", new DateTime(2025, 7, 23, 18, 29, 47, 287, DateTimeKind.Utc).AddTicks(4963), null, "fb39c1e3-3f7e-4f69-b0c7-ab7d0854b902", true, false, 1, "Удобно за индивидуални спортисти." },
                    { 6, "ул. Капитан Райчо 12, Sofia", new DateTime(2025, 11, 5, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 11, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Sofia", new DateTime(2025, 7, 23, 18, 29, 47, 287, DateTimeKind.Utc).AddTicks(4966), null, "556cff19-6dd1-4005-836d-420f8b3877f8", true, false, 2, "Близо до метро и спортен център." },
                    { 7, "булевард Руски 78, Plovdiv", new DateTime(2025, 7, 27, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 8, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Plovdiv", new DateTime(2025, 7, 23, 18, 29, 47, 287, DateTimeKind.Utc).AddTicks(4968), null, "556cff19-6dd1-4005-836d-420f8b3877f8", true, false, 3, "Кухня и паркинг на разположение." },
                    { 8, "ул. Гладстон 30, Varna", new DateTime(2025, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 9, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Varna", new DateTime(2025, 7, 23, 18, 29, 47, 287, DateTimeKind.Utc).AddTicks(4970), null, "31b0fae9-597d-46ce-95be-8c6e51cccf92", true, false, 1, "Спокоен район, без домашни любимци." },
                    { 9, "ул. Иван Вазов 44, Burgas", new DateTime(2025, 8, 20, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 9, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Burgas", new DateTime(2025, 7, 23, 18, 29, 47, 287, DateTimeKind.Utc).AddTicks(4972), null, "31b0fae9-597d-46ce-95be-8c6e51cccf92", true, false, 2, "Близо до плажа и спортни съоръжения." },
                    { 10, "ул. Гео Милев 55, Ruse", new DateTime(2025, 7, 25, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 8, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Ruse", new DateTime(2025, 7, 23, 18, 29, 47, 287, DateTimeKind.Utc).AddTicks(4975), null, "072d4f63-2658-489b-a874-1e32d50b8837", true, false, 1, "Идеално за индивидуални състезатели." },
                    { 11, "ул. Христо Ботев 22, Stara Zagora", new DateTime(2025, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 8, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Stara Zagora", new DateTime(2025, 7, 23, 18, 29, 47, 287, DateTimeKind.Utc).AddTicks(4977), null, "072d4f63-2658-489b-a874-1e32d50b8837", true, false, 3, "Идеално за семейства с малки деца." },
                    { 12, "ул. Васил Левски 15, Pleven", new DateTime(2025, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 9, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Pleven", new DateTime(2025, 7, 23, 18, 29, 47, 287, DateTimeKind.Utc).AddTicks(4980), null, "716bc195-efc6-4637-943a-a3a80f086e65", true, false, 2, "Тих район, удобен за тренировки." },
                    { 13, "булевард Княгиня Мария Луиза 45, Sliven", new DateTime(2025, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 9, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Sliven", new DateTime(2025, 7, 23, 18, 29, 47, 287, DateTimeKind.Utc).AddTicks(4982), null, "716bc195-efc6-4637-943a-a3a80f086e65", true, false, 1, "Подходящо за самостоятелни спортисти." },
                    { 14, "ул. Съборна 7, Dobrich", new DateTime(2025, 8, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Utc), "Dobrich", new DateTime(2025, 7, 23, 18, 29, 47, 287, DateTimeKind.Utc).AddTicks(4984), null, "fb39c1e3-3f7e-4f69-b0c7-ab7d0854b902", true, false, 2, "Близо до спортна зала и фитнес." },
                    { 15, "ул. Васил Кънчов 12, Blagoevgrad", new DateTime(2025, 11, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Blagoevgrad", new DateTime(2025, 7, 23, 18, 29, 47, 287, DateTimeKind.Utc).AddTicks(4986), null, "fb39c1e3-3f7e-4f69-b0c7-ab7d0854b902", true, false, 2, "Уютен апартамент близо до центъра." },
                    { 16, "бул. Цар Освободител 34, Veliko Tarnovo", new DateTime(2025, 11, 18, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 11, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Veliko Tarnovo", new DateTime(2025, 7, 23, 18, 29, 47, 287, DateTimeKind.Utc).AddTicks(4987), null, "fb39c1e3-3f7e-4f69-b0c7-ab7d0854b902", true, false, 3, "Просторен дом с прекрасна гледка." },
                    { 17, "ул. Христо Смирненски 7, Gabrovo", new DateTime(2025, 12, 10, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 12, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Gabrovo", new DateTime(2025, 7, 23, 18, 29, 47, 287, DateTimeKind.Utc).AddTicks(4989), null, "ede3325f-5350-496e-b5d5-339823b8d5b1", true, false, 1, "Тихо място, подходящо за индивидуални спортисти." }
                });

            migrationBuilder.InsertData(
                table: "Accommodations",
                columns: new[] { "Id", "Address", "AvailableFrom", "AvailableTo", "City", "CreatedOn", "DeletedOn", "HostUserId", "IsDeleted", "MaxGuests", "Notes" },
                values: new object[] { 18, "ул. Гео Милев 19, Pazardzhik", new DateTime(2025, 10, 25, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 10, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Pazardzhik", new DateTime(2025, 7, 23, 18, 29, 47, 287, DateTimeKind.Utc).AddTicks(4992), null, "ede3325f-5350-496e-b5d5-339823b8d5b1", false, 4, "Голяма къща с двор, подходяща за групи." });

            migrationBuilder.InsertData(
                table: "Accommodations",
                columns: new[] { "Id", "Address", "AvailableFrom", "AvailableTo", "City", "CreatedOn", "DeletedOn", "HostUserId", "IsAvailable", "IsDeleted", "MaxGuests", "Notes" },
                values: new object[,]
                {
                    { 19, "ул. Ст. Караджа 10, Haskovo", new DateTime(2025, 10, 10, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 10, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Haskovo", new DateTime(2025, 7, 23, 18, 29, 47, 287, DateTimeKind.Utc).AddTicks(4994), null, "9dbe7540-f8cd-43c7-b2df-f3947135d8bf", true, false, 2, "Добре обзаведен апартамент, близо до спортна зала." },
                    { 20, "ул. Ген. Скобелев 25, Dobrich", new DateTime(2025, 9, 1, 13, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 9, 10, 22, 0, 0, 0, DateTimeKind.Utc), "Dobrich", new DateTime(2025, 7, 23, 18, 29, 47, 287, DateTimeKind.Utc).AddTicks(4995), null, "9dbe7540-f8cd-43c7-b2df-f3947135d8bf", true, false, 1, "Самостоятелна стая с достъп до кухня." },
                    { 21, "ул. Розова Долина 8, Kazanlak", new DateTime(2025, 10, 20, 13, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 10, 28, 22, 0, 0, 0, DateTimeKind.Utc), "Kazanlak", new DateTime(2025, 7, 23, 18, 29, 47, 287, DateTimeKind.Utc).AddTicks(4997), null, "763aebd4-e72a-41d7-bd9e-7ecbc0dba1a8", true, false, 3, "Тих квартал, удобен за тренировки и отдих." }
                });

            migrationBuilder.InsertData(
                table: "Accommodations",
                columns: new[] { "Id", "Address", "AvailableFrom", "AvailableTo", "City", "CreatedOn", "DeletedOn", "HostUserId", "IsDeleted", "MaxGuests", "Notes" },
                values: new object[] { 22, "ул. Съборна 3, Shumen", new DateTime(2025, 8, 25, 18, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 8, 30, 15, 0, 0, 0, DateTimeKind.Utc), "Shumen", new DateTime(2025, 7, 23, 18, 29, 47, 287, DateTimeKind.Utc).AddTicks(4999), null, "763aebd4-e72a-41d7-bd9e-7ecbc0dba1a8", false, 2, "Добре оборудвана къща близо до центъра." });

            migrationBuilder.InsertData(
                table: "Accommodations",
                columns: new[] { "Id", "Address", "AvailableFrom", "AvailableTo", "City", "CreatedOn", "DeletedOn", "HostUserId", "IsAvailable", "IsDeleted", "MaxGuests", "Notes" },
                values: new object[,]
                {
                    { 23, "бул. България 17, Targovishte", new DateTime(2026, 1, 20, 13, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 10, 28, 15, 0, 0, 0, DateTimeKind.Utc), "Targovishte", new DateTime(2025, 7, 23, 18, 29, 47, 287, DateTimeKind.Utc).AddTicks(5001), null, "004c1a89-ede7-471f-ac81-93c6a9d8b0d8", true, false, 1, "Самостоятелна стая с добър достъп до транспорт." },
                    { 24, "ул. Пирин 14, Montana", new DateTime(2025, 8, 5, 12, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 8, 25, 12, 0, 0, 0, DateTimeKind.Utc), "Montana", new DateTime(2025, 7, 23, 18, 29, 47, 287, DateTimeKind.Utc).AddTicks(5003), null, "004c1a89-ede7-471f-ac81-93c6a9d8b0d8", true, false, 3, "Просторен апартамент, подходящ за групи." }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7699db7d-964f-4782-8209-d76562e0fece",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2b8f24c8-fef6-4f06-bd1d-b9faca6f580c", "AQAAAAIAAYagAAAAEDF2HSfu0T8KtCIwBUZa1DrAVFPh0rzHDtZtGUjFcnl08WXyBgS9DZYrXpsUQiMz2w==", "d4bdac8a-e315-41a6-8b5b-a0e6a08a1005" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7699db7d-964f-4782-8209-d76562e0fece",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "96880a2d-07b2-4ef5-908f-b05fecf20edc", "AQAAAAIAAYagAAAAEPdqkLKfY/nrVHGHZsHyqXtYcwPO9zZqYi9ef3BVR4lljsZ+tbFjaxTRR3JU6dX6QQ==", "30ecf751-8c37-4e60-a31c-16f6aaee64df" });
        }
    }
}
