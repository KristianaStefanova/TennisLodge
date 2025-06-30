using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TennisLodge.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accommodations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Primary key for the Accommodation entity")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HostUserId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Foreign key to the user offering the accommodation"),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "City where the accommodation is located"),
                    Address = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false, comment: "Address or description of the accommodation"),
                    MaxGuests = table.Column<int>(type: "int", nullable: false, comment: "Maximum number of guests that can be hosted"),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false, defaultValue: true, comment: "Indicates if the accommodation is currently available"),
                    Notes = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true, comment: "Optional description or notes about the accommodation"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()", comment: "Date when the offer was created")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accommodations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accommodations_AspNetUsers_HostUserId",
                        column: x => x.HostUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Accommodation offered by a user for hosting players");

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Primary key identifier of the category")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false, comment: "Name of the category")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                },
                comment: "Category of tournaments");

            migrationBuilder.CreateTable(
                name: "PlayerProfiles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Primary key and foreign key to ApplicationUser"),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Player's date of birth"),
                    Nationality = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false, comment: "Player's nationality"),
                    Ranking = table.Column<int>(type: "int", nullable: true, comment: "Player's ranking, if available"),
                    PreferredSurface = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true, comment: "Player's preferred playing surface"),
                    DominantHand = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true, comment: "Player's dominant hand (left/right)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerProfiles", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_PlayerProfiles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Player-specific profile with athletic data");

            migrationBuilder.CreateTable(
                name: "Tournaments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Unique identifier for the tournament"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Official name of the tournament"),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false, comment: "Detailed description of the tournament"),
                    Location = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false, comment: "City or venue where the tournament takes place"),
                    Surface = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Surface type"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Optional URL for the tournament image"),
                    Organizer = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "Name of the organizer or organizing body"),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false, comment: "Start date of the tournament"),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false, comment: "End date of the tournament"),
                    PublisherId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Foreign key to the user who published the tournament"),
                    CategoryId = table.Column<int>(type: "int", nullable: false, comment: "Foreign key to the tournament category"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "Soft delete flag, true if tournament is deleted")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tournaments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tournaments_AspNetUsers_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tournaments_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Tournament in the system");

            migrationBuilder.CreateTable(
                name: "AccommodationRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Primary key of the accommodation request")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GuestUserId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Foreign key to the user requesting accommodation"),
                    TournamentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Foreign key to the tournament the request is for"),
                    NumberOfGuests = table.Column<int>(type: "int", nullable: false, comment: "Number of people (including the guest) needing accommodation"),
                    Notes = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true, comment: "Additional notes or requests from the guest"),
                    IsFulfilled = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "Indicates whether the request has been fulfilled or matched"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()", comment: "Date when the request was created")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccommodationRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccommodationRequests_AspNetUsers_GuestUserId",
                        column: x => x.GuestUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccommodationRequests_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Request from a user to be hosted during a tournament");

            migrationBuilder.CreateTable(
                name: "UserTournaments",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Foreign key to the user"),
                    TournamentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Foreign key to the tournament")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTournaments", x => new { x.UserId, x.TournamentId });
                    table.ForeignKey(
                        name: "FK_UserTournaments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTournaments_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Join table between users and tournaments");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "City", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "7699db7d-964f-4782-8209-d76562e0fece", 0, "Sofia", "b770a00b-f075-497e-a8d1-3da5a912993f", "admin@tennislodge.com", true, "Admin", "User", false, null, "ADMIN@TENNISLODGE.COM", "ADMIN@TENNISLODGE.COM", "AQAAAAIAAYagAAAAEN575mD7VbLbYdnYCGzvg5h5i/HSLjW+OLiwWfhwsJOUXx8x6pSN/XmiBss+rMpP4g==", null, false, "deb78574-95b2-47f1-b6f9-f1765c92e77a", false, "admin@tennislodge.com" });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "ATP 250" },
                    { 2, "Challenger" },
                    { 3, "ITF Futures" },
                    { 4, "National - Under 10" },
                    { 5, "National - Under 12" },
                    { 6, "National - Under 14" },
                    { 7, "Tennis Europe - Under 12" },
                    { 8, "Tennis Europe - Under 14" },
                    { 9, "Tennis Europe - Under 16" },
                    { 10, "ITF Juniors" }
                });

            migrationBuilder.InsertData(
                table: "Tournaments",
                columns: new[] { "Id", "CategoryId", "Description", "EndDate", "ImageUrl", "Location", "Name", "Organizer", "PublisherId", "StartDate", "Surface" },
                values: new object[,]
                {
                    { new Guid("0ceea4f9-b7bc-4d18-90cf-3b64b06c2a1f"), 8, "Tennis Europe international event held in Albena for top U14 talents.", new DateOnly(2025, 8, 18), "https://example.com/tennis-europe-albena.jpg", "Albena Resort Courts", "Tennis Europe U14 – Albena", "Tennis Europe / BTF", "7699db7d-964f-4782-8209-d76562e0fece", new DateOnly(2025, 8, 12), "Hard" },
                    { new Guid("2e3c6c45-315c-4f24-8803-4c9269ef9c7d"), 3, "A fun and competitive tournament held by the sea on synthetic surface.", new DateOnly(2025, 8, 10), "https://visit.varna.bg/images/news/tennis_varna.jpg", "Varna, Beach Courts", "Varna Beach Open", "Varna Sports Association", "7699db7d-964f-4782-8209-d76562e0fece", new DateOnly(2025, 8, 5), "Synthetic" },
                    { new Guid("7b6d0baf-2a39-41d7-bc2a-e5713b302baf"), 2, "Challenger-level clay court tournament attracting top Eastern European players.", new DateOnly(2025, 6, 16), "https://upload.wikimedia.org/wikipedia/commons/thumb/6/64/Plovdiv_tennis_court.jpg/800px-Plovdiv_tennis_court.jpg", "Plovdiv, Tennis Complex", "Plovdiv Clay Cup", "Plovdiv Tennis Club", "7699db7d-964f-4782-8209-d76562e0fece", new DateOnly(2025, 6, 10), "Clay" },
                    { new Guid("8f19c979-40c2-4cb8-8af0-d061456245bd"), 1, "Professional indoor hard court tournament held annually in Sofia, Bulgaria.", new DateOnly(2025, 10, 7), "https://www.tennis.bg/uploads/posts/2022-08/sofia-open.jpg", "Sofia, Arena Armeec", "Sofia Open", "Bulgarian Tennis Federation", "7699db7d-964f-4782-8209-d76562e0fece", new DateOnly(2025, 10, 1), "Hard (Indoor)" },
                    { new Guid("a5f6700a-3e08-430b-9462-2b2f61d31af2"), 6, "Elite tournament for U14 players across the country.", new DateOnly(2025, 7, 14), "https://example.com/blago-u14.jpg", "Blagoevgrad Tennis Club", "Blagoevgrad U14 Masters", "National Youth Tennis", "7699db7d-964f-4782-8209-d76562e0fece", new DateOnly(2025, 7, 10), "Clay" },
                    { new Guid("c909f34e-8126-42c5-9e65-8d9c0ff0db96"), 9, "International ITF Junior tournament held in Bulgaria's capital.", new DateOnly(2025, 11, 7), "https://example.com/itf-juniors-sofia.jpg", "Sofia, National Tennis Center", "ITF Juniors - Sofia", "ITF / Bulgarian Tennis Federation", "7699db7d-964f-4782-8209-d76562e0fece", new DateOnly(2025, 11, 1), "Hard" },
                    { new Guid("e260f1e1-b12b-4e2b-87c8-e2df1f388377"), 5, "National ranking tournament for players under 12 years old.", new DateOnly(2025, 4, 21), "https://example.com/stz-u12.jpg", "Stara Zagora Tennis Arena", "Stara Zagora U12 Cup", "Bulgarian Tennis Federation", "7699db7d-964f-4782-8209-d76562e0fece", new DateOnly(2025, 4, 18), "Hard" },
                    { new Guid("ea09c21c-62f5-4ee2-99bb-d63f682c5ee3"), 4, "National tournament for kids under 10, designed to encourage early development.", new DateOnly(2025, 5, 4), "https://example.com/burgas-u10.jpg", "Burgas Tennis Club", "Burgas U10 Open", "Burgas Youth Sports", "7699db7d-964f-4782-8209-d76562e0fece", new DateOnly(2025, 5, 2), "Clay" },
                    { new Guid("fe9804cb-0ae2-42b6-a1fd-d90c0bc880ec"), 7, "Annual national youth championship open to all categories.", new DateOnly(2025, 9, 7), "https://example.com/national-championship.jpg", "Sofia National Center", "National Youth Championship", "Bulgarian Tennis Federation", "7699db7d-964f-4782-8209-d76562e0fece", new DateOnly(2025, 9, 1), "Clay" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccommodationRequests_GuestUserId",
                table: "AccommodationRequests",
                column: "GuestUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AccommodationRequests_TournamentId",
                table: "AccommodationRequests",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_Accommodations_HostUserId",
                table: "Accommodations",
                column: "HostUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_CategoryId",
                table: "Tournaments",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_PublisherId",
                table: "Tournaments",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTournaments_TournamentId",
                table: "UserTournaments",
                column: "TournamentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccommodationRequests");

            migrationBuilder.DropTable(
                name: "Accommodations");

            migrationBuilder.DropTable(
                name: "PlayerProfiles");

            migrationBuilder.DropTable(
                name: "UserTournaments");

            migrationBuilder.DropTable(
                name: "Tournaments");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7699db7d-964f-4782-8209-d76562e0fece");
        }
    }
}
