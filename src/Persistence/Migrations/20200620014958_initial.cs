using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EventsCore.Persistence.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public partial class initial : Migration

    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventSeries",
                columns: table => new
                {
                    EventSeriesID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventSeries", x => x.EventSeriesID);
                });

            migrationBuilder.CreateTable(
                name: "EventTypes",
                columns: table => new
                {
                    EventTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventTypes", x => x.EventTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Ranks",
                columns: table => new
                {
                    RankID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Abbreviation = table.Column<string>(maxLength: 10, nullable: false),
                    FullName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ranks", x => x.RankID);
                });

            migrationBuilder.CreateTable(
                name: "UserRoleTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoleTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LDAPName = table.Column<string>(maxLength: 25, nullable: false),
                    BlueDeckId = table.Column<long>(maxLength: 10, nullable: false),
                    NameFactory_First = table.Column<string>(nullable: true),
                    NameFactory_Last = table.Column<string>(nullable: true),
                    IdNumber = table.Column<string>(maxLength: 20, nullable: false),
                    Email = table.Column<string>(maxLength: 50, nullable: false),
                    ContactNumber = table.Column<string>(maxLength: 10, nullable: true),
                    RankId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Ranks_RankId",
                        column: x => x.RankId,
                        principalTable: "Ranks",
                        principalColumn: "RankID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "ntext", nullable: false),
                    EventSeriesId = table.Column<int>(nullable: true),
                    EventTypeId = table.Column<int>(nullable: false),
                    OwnerId = table.Column<int>(nullable: false),
                    Address_Street = table.Column<string>(nullable: true),
                    Address_Suite = table.Column<string>(nullable: true),
                    Address_City = table.Column<string>(nullable: true),
                    Address_State = table.Column<string>(nullable: true),
                    Address_ZipCode = table.Column<string>(nullable: true),
                    Dates_StartDate = table.Column<DateTime>(nullable: true),
                    Dates_EndDate = table.Column<DateTime>(nullable: true),
                    Dates_RegistrationStartDate = table.Column<DateTime>(nullable: true),
                    Dates_RegistrationEndDate = table.Column<DateTime>(nullable: true),
                    Rules_MaxRegistrations = table.Column<long>(nullable: true),
                    Rules_MinRegistrations = table.Column<long>(nullable: true),
                    Rules_MaxStandbyRegistrations = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_EventSeries_EventSeriesId",
                        column: x => x.EventSeriesId,
                        principalTable: "EventSeries",
                        principalColumn: "EventSeriesID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Events_EventTypes_EventTypeId",
                        column: x => x.EventTypeId,
                        principalTable: "EventTypes",
                        principalColumn: "EventTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Events_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserRoleTypeId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRole_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_UserRoleTypes_UserRoleTypeId",
                        column: x => x.UserRoleTypeId,
                        principalTable: "UserRoleTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Module",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<int>(nullable: false),
                    ModuleName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    EventId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Module", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Module_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Module_Events_EventId1",
                        column: x => x.EventId1,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Registration",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Contact = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: false),
                    Registered = table.Column<DateTime>(nullable: false),
                    StatusChanged = table.Column<DateTime>(nullable: false),
                    EventId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Registration_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Registration_Events_EventId1",
                        column: x => x.EventId1,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Attendance",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<int>(nullable: false),
                    ModuleId = table.Column<int>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    Score = table.Column<double>(nullable: false),
                    Status = table.Column<string>(nullable: false),
                    EventId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attendance_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Attendance_Events_EventId1",
                        column: x => x.EventId1,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Attendance_Module_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Module",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "EventTypes",
                columns: new[] { "EventTypeID", "Name" },
                values: new object[,]
                {
                    { 1, "Training" },
                    { 2, "Overtime" },
                    { 3, "Assignment" }
                });

            migrationBuilder.InsertData(
                table: "Ranks",
                columns: new[] { "RankID", "Abbreviation", "FullName" },
                values: new object[,]
                {
                    { 1, "P/O", "Police Officer" },
                    { 2, "POFC", "Police Officer First Class" },
                    { 3, "Cpl.", "Corporal" },
                    { 4, "Sgt.", "Sergeant" },
                    { 5, "Lt.", "Lieutenant" },
                    { 6, "Capt.", "Captain" },
                    { 7, "Maj.", "Major" },
                    { 8, "D/Chief", "Deputy Chief" },
                    { 9, "A/Chief", "Assistant Chief" },
                    { 10, "Chief", "Chief of Police" }
                });

            migrationBuilder.InsertData(
                table: "UserRoleTypes",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Administrator" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BlueDeckId", "ContactNumber", "Email", "IdNumber", "LDAPName", "RankId", "NameFactory_First", "NameFactory_Last" },
                values: new object[] { 1, 1L, "3016483444", "jcs3082@hotmail.com", "3134", "jcs30", 5, "Jason", "Smith" });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "Id", "UserId", "UserRoleTypeId" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_EventId",
                table: "Attendance",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_EventId1",
                table: "Attendance",
                column: "EventId1");

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_ModuleId",
                table: "Attendance",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_EventSeriesId",
                table: "Events",
                column: "EventSeriesId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_EventTypeId",
                table: "Events",
                column: "EventTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_OwnerId",
                table: "Events",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Module_EventId",
                table: "Module",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Module_EventId1",
                table: "Module",
                column: "EventId1");

            migrationBuilder.CreateIndex(
                name: "IX_Registration_EventId",
                table: "Registration",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Registration_EventId1",
                table: "Registration",
                column: "EventId1");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_UserId",
                table: "UserRole",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_UserRoleTypeId",
                table: "UserRole",
                column: "UserRoleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RankId",
                table: "Users",
                column: "RankId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attendance");

            migrationBuilder.DropTable(
                name: "Registration");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "Module");

            migrationBuilder.DropTable(
                name: "UserRoleTypes");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "EventSeries");

            migrationBuilder.DropTable(
                name: "EventTypes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Ranks");
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
