using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TennisLodge.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateWithIdentityAndSeeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "User's first name"),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "User's last name"),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "User's city of residence"),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                },
                comment: "Extended Identity user with additional profile data");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Primary key identifier of the category")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false, comment: "Name of the category")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                },
                comment: "Category of tournaments");

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "Official name of the tournament"),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false, comment: "Detailed description of the tournament"),
                    Location = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "City or venue where the tournament takes place"),
                    Surface = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "Surface type"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Optional URL for the tournament image"),
                    Organizer = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Name of the organizer or organizing body"),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false, comment: "Start date of the tournament"),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false, comment: "End date of the tournament"),
                    PublisherId = table.Column<string>(type: "nvarchar(450)", nullable: true, comment: "Foreign key to the user who published the tournament"),
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
                        name: "FK_Tournaments_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
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
                values: new object[] { "7699db7d-964f-4782-8209-d76562e0fece", 0, "Sofia", "90a17f19-a9e1-4360-88b2-4de593c2ee83", "admin@tennislodge.com", true, "Admin", "User", false, null, "ADMIN@TENNISLODGE.COM", "ADMIN@TENNISLODGE.COM", "AQAAAAIAAYagAAAAEMJ8E1bHXniODU4nDI+eix0O/ch6HzSc/+yH05X0T+GYdog3U5p3JMRpxRWRMYJXRw==", null, false, "e3f14a8e-b996-4b15-ba1c-360ef12cf1e1", false, "admin@tennislodge.com" });

            migrationBuilder.InsertData(
                table: "Categories",
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
                    { new Guid("0ceea4f9-b7bc-4d18-90cf-3b64b06c2a1f"), 8, "Tennis Europe international event held in Albena for top U14 talents.", new DateOnly(2025, 8, 18), "/images/TennisEurope.jpg", "Albena Resort Courts", "Tennis Europe U14 – Albena", "Tennis Europe / BTF", "7699db7d-964f-4782-8209-d76562e0fece", new DateOnly(2025, 8, 12), "Hard" },
                    { new Guid("7b6d0baf-2a39-41d7-bc2a-e5713b302baf"), 2, "Challenger-level clay court tournament attracting top Eastern European players.", new DateOnly(2025, 6, 16), "/images/ChallengerTour.jpg", "Plovdiv, Tennis Complex", "Plovdiv Clay Cup", "Plovdiv Tennis Club", "7699db7d-964f-4782-8209-d76562e0fece", new DateOnly(2025, 6, 10), "Clay" },
                    { new Guid("8f19c979-40c2-4cb8-8af0-d061456245bd"), 1, "Professional indoor hard court tournament held annually in Sofia, Bulgaria.", new DateOnly(2025, 10, 7), "/images/atp250.jpg", "Sofia, Arena Armeec", "Sofia Open", "Bulgarian Tennis Federation", "7699db7d-964f-4782-8209-d76562e0fece", new DateOnly(2025, 10, 1), "Hard (Indoor)" },
                    { new Guid("90bb0d0a-20ae-4952-804b-bb9e3bcecb18"), 7, "Team-based tournament for the best clubs nationwide.", new DateOnly(2025, 9, 20), "/images/BulgarianFederation.jpg", "Sofia Tennis Center", "National Juniors Teams Cup", "Bulgarian Tennis Federation", "7699db7d-964f-4782-8209-d76562e0fece", new DateOnly(2025, 9, 15), "Clay" },
                    { new Guid("a1f0c8c7-f4b3-4eb9-82e3-4f8cbf2066ee"), 1, "ATP 250 series tournament on the Black Sea coast.", new DateOnly(2025, 10, 21), "/images/atp250.jpg", "Varna Tennis Club", "ATP 250 Varna", "Bulgarian Tennis Federation", "7699db7d-964f-4782-8209-d76562e0fece", new DateOnly(2025, 10, 15), "Hard (Outdoor)" },
                    { new Guid("a5f6700a-3e08-430b-9462-2b2f61d31af2"), 6, "Elite tournament for U14 players across the country.", new DateOnly(2025, 7, 14), "/images/BulgarianFederation.jpg", "Blagoevgrad Tennis Club", "Blagoevgrad U14 Masters", "National Youth Tennis", "7699db7d-964f-4782-8209-d76562e0fece", new DateOnly(2025, 7, 10), "Clay" },
                    { new Guid("a9f5d6c3-7d5a-4a47-95ed-31080b39a04b"), 8, "U16 Tennis Europe event hosted at the Black Sea coast.", new DateOnly(2025, 8, 31), "/images/TennisEurope.jpg", "Varna Tennis Arena", "Tennis Europe U16 – Varna", "Tennis Europe / BTF", "7699db7d-964f-4782-8209-d76562e0fece", new DateOnly(2025, 8, 25), "Hard" },
                    { new Guid("b5d3462f-7f51-4382-b57c-cc1464a68f1f"), 2, "Challenger Tour event played on indoor hard court.", new DateOnly(2025, 6, 26), "/images/ChallengerTour.jpg", "Rousse Arena", "Challenger Rousse Open", "Rousse Tennis Association", "7699db7d-964f-4782-8209-d76562e0fece", new DateOnly(2025, 6, 20), "Hard (Indoor)" },
                    { new Guid("c909f34e-8126-42c5-9e65-8d9c0ff0db96"), 9, "International ITF Junior tournament held in Bulgaria's capital.", new DateOnly(2025, 11, 7), "/images/ITFJuniors.jpg", "Sofia, National Tennis Center", "ITF Juniors - Sofia", "ITF / Bulgarian Tennis Federation", "7699db7d-964f-4782-8209-d76562e0fece", new DateOnly(2025, 11, 1), "Hard" },
                    { new Guid("d27a295f-2b7c-4fc1-9113-89672539e12c"), 9, "High-level ITF juniors event held in Plovdiv.", new DateOnly(2025, 11, 18), "/images/ITFJuniors.jpg", "Plovdiv Tennis Arena", "ITF Juniors - Plovdiv", "ITF / Bulgarian Tennis Federation", "7699db7d-964f-4782-8209-d76562e0fece", new DateOnly(2025, 11, 12), "Clay" },
                    { new Guid("dff35c76-7d20-4203-aaa1-7cb5117fd9f7"), 4, "Introductory tournament for players under 10.", new DateOnly(2025, 5, 17), "/images/BulgarianFederation.jpg", "Veliko Tarnovo Tennis Park", "Kidz Clay Open - Veliko Tarnovo", "Youth Tennis Bulgaria", "7699db7d-964f-4782-8209-d76562e0fece", new DateOnly(2025, 5, 15), "Clay" },
                    { new Guid("e0c9381f-f3ff-48cc-8719-30a4c649844a"), 5, "Official U12 tournament on clay courts.", new DateOnly(2025, 4, 28), "/images/BulgarianFederation.jpg", "Dobrich Tennis Club", "U12 Green Cup – Dobrich", "Dobrich Tennis Foundation", "7699db7d-964f-4782-8209-d76562e0fece", new DateOnly(2025, 4, 25), "Clay" },
                    { new Guid("e260f1e1-b12b-4e2b-87c8-e2df1f388377"), 5, "National ranking tournament for players under 12 years old.", new DateOnly(2025, 4, 21), "/images/BulgarianFederation.jpg", "Stara Zagora Tennis Arena", "Stara Zagora U12 Cup", "Bulgarian Tennis Federation", "7699db7d-964f-4782-8209-d76562e0fece", new DateOnly(2025, 4, 18), "Hard" },
                    { new Guid("ea09c21c-62f5-4ee2-99bb-d63f682c5ee3"), 4, "National tournament for kids under 10, designed to encourage early development.", new DateOnly(2025, 5, 4), "/images/BulgarianFederation.jpg", "Burgas Tennis Club", "Burgas U10 Open", "Burgas Youth Sports", "7699db7d-964f-4782-8209-d76562e0fece", new DateOnly(2025, 5, 2), "Clay" },
                    { new Guid("f540e688-b37a-4061-9bc4-0d4b1bdbd1e6"), 6, "Important regional tournament for U14 talents.", new DateOnly(2025, 7, 9), "/images/BulgarianFederation.jpg", "Samokov Tennis Arena", "Samokov U14 Cup", "Samokov Tennis League", "7699db7d-964f-4782-8209-d76562e0fece", new DateOnly(2025, 7, 5), "Hard" },
                    { new Guid("fe9804cb-0ae2-42b6-a1fd-d90c0bc880ec"), 7, "Annual national youth championship open to all categories.", new DateOnly(2025, 9, 7), "/images/BulgarianFederation.jpg", "Sofia National Center", "National Youth Championship", "Bulgarian Tennis Federation", "7699db7d-964f-4782-8209-d76562e0fece", new DateOnly(2025, 9, 1), "Clay" }
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
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

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
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "PlayerProfiles");

            migrationBuilder.DropTable(
                name: "UserTournaments");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Tournaments");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
