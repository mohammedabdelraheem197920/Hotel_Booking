using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructuer.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
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
                    NationalID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AgeCategory = table.Column<int>(type: "int", nullable: false),
                    isOldClient = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hotels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotels", x => x.Id);
                });

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
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                name: "Branches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HotelID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Branches_Hotels_HotelID",
                        column: x => x.HotelID,
                        principalTable: "Hotels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CheckInDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckOutDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumberOfRooms = table.Column<int>(type: "int", nullable: false),
                    DiscountApplied = table.Column<bool>(type: "bit", nullable: false),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    NumberOfAdults = table.Column<int>(type: "int", nullable: false),
                    NumberOfChildren = table.Column<int>(type: "int", nullable: false),
                    IsBooked = table.Column<bool>(type: "bit", nullable: false),
                    BookingId = table.Column<int>(type: "int", nullable: true),
                    BranchID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rooms_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Rooms_Branches_BranchID",
                        column: x => x.BranchID,
                        principalTable: "Branches",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Hotels",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Hotel A" },
                    { 2, "Hotel B" },
                    { 3, "Hotel C" },
                    { 4, "Hotel D" },
                    { 5, "Hotel E" }
                });

            migrationBuilder.InsertData(
                table: "Branches",
                columns: new[] { "Id", "City", "Country", "HotelID", "PostalCode", "State", "Street" },
                values: new object[,]
                {
                    { 1, "City1", "Country1", 1, "12345", "State1", "Street1" },
                    { 2, "City2", "Country1", 1, "12346", "State1", "Street2" },
                    { 3, "City3", "Country1", 1, "12347", "State1", "Street3" },
                    { 4, "City1", "Country2", 2, "22345", "State2", "Street4" },
                    { 5, "City2", "Country2", 2, "22346", "State2", "Street5" },
                    { 6, "City3", "Country2", 2, "22347", "State2", "Street6" },
                    { 7, "City1", "Country3", 3, "32345", "State3", "Street7" },
                    { 8, "City2", "Country3", 3, "32346", "State3", "Street8" },
                    { 9, "City3", "Country3", 3, "32347", "State3", "Street9" },
                    { 10, "City1", "Country4", 4, "42345", "State4", "Street10" },
                    { 11, "City2", "Country4", 4, "42346", "State4", "Street11" },
                    { 12, "City3", "Country4", 4, "42347", "State4", "Street12" },
                    { 13, "City1", "Country5", 5, "52345", "State5", "Street13" },
                    { 14, "City2", "Country5", 5, "52346", "State5", "Street14" },
                    { 15, "City3", "Country5", 5, "52347", "State5", "Street15" }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "BookingId", "BranchID", "IsBooked", "NumberOfAdults", "NumberOfChildren", "Type" },
                values: new object[,]
                {
                    { 1, null, 1, false, 0, 0, 0 },
                    { 2, null, 1, false, 0, 0, 0 },
                    { 3, null, 2, false, 0, 0, 0 },
                    { 4, null, 2, false, 0, 0, 0 },
                    { 5, null, 3, false, 0, 0, 0 },
                    { 6, null, 3, false, 0, 0, 0 },
                    { 7, null, 4, false, 0, 0, 0 },
                    { 8, null, 4, false, 0, 0, 1 },
                    { 9, null, 5, false, 0, 0, 2 },
                    { 10, null, 5, false, 0, 0, 0 },
                    { 11, null, 6, false, 0, 0, 1 },
                    { 12, null, 6, false, 0, 0, 2 },
                    { 13, null, 7, false, 0, 0, 0 },
                    { 14, null, 7, false, 0, 0, 1 },
                    { 15, null, 8, false, 0, 0, 2 },
                    { 16, null, 8, false, 0, 0, 0 },
                    { 17, null, 9, false, 0, 0, 1 },
                    { 18, null, 9, false, 0, 0, 2 },
                    { 19, null, 10, false, 0, 0, 0 },
                    { 20, null, 10, false, 0, 0, 1 },
                    { 21, null, 1, false, 0, 0, 1 },
                    { 22, null, 1, false, 0, 0, 1 },
                    { 23, null, 1, false, 0, 0, 2 },
                    { 24, null, 2, false, 0, 0, 1 },
                    { 25, null, 2, false, 0, 0, 1 },
                    { 26, null, 2, false, 0, 0, 2 },
                    { 27, null, 3, false, 0, 0, 1 },
                    { 28, null, 3, false, 0, 0, 2 },
                    { 29, null, 4, false, 0, 0, 0 },
                    { 30, null, 4, false, 0, 0, 1 },
                    { 31, null, 4, false, 0, 0, 2 },
                    { 32, null, 5, false, 0, 0, 0 },
                    { 33, null, 5, false, 0, 0, 1 },
                    { 34, null, 5, false, 0, 0, 1 },
                    { 35, null, 6, false, 0, 0, 1 },
                    { 36, null, 6, false, 0, 0, 0 },
                    { 37, null, 6, false, 0, 0, 0 },
                    { 38, null, 7, false, 0, 0, 1 },
                    { 39, null, 7, false, 0, 0, 0 },
                    { 40, null, 7, false, 0, 0, 2 },
                    { 41, null, 8, false, 0, 0, 0 },
                    { 42, null, 8, false, 0, 0, 1 },
                    { 43, null, 8, false, 0, 0, 1 },
                    { 44, null, 9, false, 0, 0, 1 },
                    { 45, null, 9, false, 0, 0, 0 },
                    { 46, null, 9, false, 0, 0, 0 },
                    { 47, null, 10, false, 0, 0, 0 },
                    { 48, null, 10, false, 0, 0, 1 },
                    { 49, null, 10, false, 0, 0, 2 },
                    { 50, null, 11, false, 0, 0, 1 },
                    { 51, null, 11, false, 0, 0, 2 },
                    { 52, null, 11, false, 0, 0, 1 },
                    { 53, null, 11, false, 0, 0, 0 },
                    { 54, null, 11, false, 0, 0, 0 },
                    { 55, null, 12, false, 0, 0, 1 },
                    { 56, null, 12, false, 0, 0, 2 },
                    { 57, null, 12, false, 0, 0, 1 },
                    { 58, null, 12, false, 0, 0, 0 },
                    { 59, null, 12, false, 0, 0, 0 },
                    { 60, null, 13, false, 0, 0, 1 },
                    { 61, null, 13, false, 0, 0, 2 },
                    { 62, null, 13, false, 0, 0, 1 },
                    { 63, null, 13, false, 0, 0, 0 },
                    { 64, null, 13, false, 0, 0, 0 },
                    { 65, null, 14, false, 0, 0, 1 },
                    { 66, null, 14, false, 0, 0, 2 },
                    { 67, null, 14, false, 0, 0, 1 },
                    { 68, null, 14, false, 0, 0, 0 },
                    { 69, null, 14, false, 0, 0, 0 },
                    { 70, null, 15, false, 0, 0, 1 },
                    { 71, null, 15, false, 0, 0, 2 },
                    { 72, null, 15, false, 0, 0, 1 },
                    { 73, null, 15, false, 0, 0, 0 },
                    { 74, null, 15, false, 0, 0, 0 },
                    { 75, null, 3, false, 0, 0, 1 }
                });

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
                name: "IX_Bookings_BranchId",
                table: "Bookings",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_UserId",
                table: "Bookings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Branches_HotelID",
                table: "Branches",
                column: "HotelID");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_BookingId",
                table: "Rooms",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_BranchID",
                table: "Rooms",
                column: "BranchID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Branches");

            migrationBuilder.DropTable(
                name: "Hotels");
        }
    }
}
