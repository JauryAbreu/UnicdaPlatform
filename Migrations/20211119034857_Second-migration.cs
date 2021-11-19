using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UnicdaPlatform.Migrations
{
    public partial class Secondmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carrier",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<string>(nullable: true),
                    CareerId = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carrier", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarrierPensum",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<string>(nullable: true),
                    CareerId = table.Column<string>(nullable: true),
                    CareerPensumId = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    PreCareerPensumId = table.Column<string>(nullable: true),
                    Credit = table.Column<int>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarrierPensum", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarrierUserPensum",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    CareerPensumId = table.Column<string>(nullable: true),
                    SessionCode = table.Column<string>(nullable: true),
                    PeriodCycle = table.Column<int>(nullable: false),
                    PeriodYear = table.Column<int>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    UserIdTeacher = table.Column<string>(nullable: true),
                    Credit = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarrierUserPensum", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarrierUserPensumDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    CareerPensumId = table.Column<string>(nullable: true),
                    SessionCode = table.Column<string>(nullable: true),
                    PeriodCycle = table.Column<int>(nullable: false),
                    PeriodYear = table.Column<int>(nullable: false),
                    FirstTest = table.Column<decimal>(nullable: false),
                    SecondTest = table.Column<decimal>(nullable: false),
                    Practice = table.Column<decimal>(nullable: false),
                    FinalTest = table.Column<decimal>(nullable: false),
                    UserIdTeacher = table.Column<string>(nullable: true),
                    Credit = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarrierUserPensumDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarrierUserTeacherPensum",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    CareerPensumId = table.Column<string>(nullable: true),
                    SessionCode = table.Column<string>(nullable: true),
                    PeriodCycle = table.Column<int>(nullable: false),
                    PeriodYear = table.Column<int>(nullable: false),
                    ClassRoom = table.Column<string>(nullable: true),
                    Day = table.Column<int>(nullable: false),
                    TimeToIn = table.Column<DateTime>(nullable: false),
                    TimeToOut = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarrierUserTeacherPensum", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RequestUserChangeCarrier",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    UserResponseId = table.Column<string>(nullable: true),
                    ResponseComment = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestUserChangeCarrier", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RequestUserMatter",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    CareerPensumId = table.Column<string>(nullable: true),
                    SessionCode = table.Column<string>(nullable: true),
                    PeriodCycle = table.Column<int>(nullable: false),
                    PeriodYear = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    UserResponseId = table.Column<string>(nullable: true),
                    ResponseComment = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestUserMatter", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserMatter",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<string>(nullable: true),
                    CareerPensumId = table.Column<string>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMatter", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Carrier_Id",
                table: "Carrier",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CarrierPensum_Id",
                table: "CarrierPensum",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CarrierUserPensum_Id",
                table: "CarrierUserPensum",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CarrierUserPensumDetails_Id",
                table: "CarrierUserPensumDetails",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CarrierUserTeacherPensum_Id",
                table: "CarrierUserTeacherPensum",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_RequestUserChangeCarrier_Id",
                table: "RequestUserChangeCarrier",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_RequestUserMatter_Id",
                table: "RequestUserMatter",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserMatter_Id",
                table: "UserMatter",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Carrier");

            migrationBuilder.DropTable(
                name: "CarrierPensum");

            migrationBuilder.DropTable(
                name: "CarrierUserPensum");

            migrationBuilder.DropTable(
                name: "CarrierUserPensumDetails");

            migrationBuilder.DropTable(
                name: "CarrierUserTeacherPensum");

            migrationBuilder.DropTable(
                name: "RequestUserChangeCarrier");

            migrationBuilder.DropTable(
                name: "RequestUserMatter");

            migrationBuilder.DropTable(
                name: "UserMatter");
        }
    }
}
