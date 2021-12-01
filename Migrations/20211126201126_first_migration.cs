using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UnicdaPlatform.Migrations
{
    public partial class first_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Batch",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<string>(nullable: true),
                    BatchId = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    TotalAmount = table.Column<decimal>(nullable: false),
                    TotalAmountWithTax = table.Column<decimal>(nullable: false),
                    TotalReturnAmount = table.Column<decimal>(nullable: false),
                    TotalReturnAmountWithTax = table.Column<decimal>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Batch", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Career",
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
                    table.PrimaryKey("PK_Career", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CareerPensum",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<string>(nullable: true),
                    CareerId = table.Column<string>(nullable: true),
                    MatterId = table.Column<string>(nullable: true),
                    Period = table.Column<int>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CareerPensum", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CareerUserPensum",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(maxLength: 50, nullable: true),
                    CareerPensumId = table.Column<int>(maxLength: 50, nullable: false),
                    SessionCode = table.Column<string>(maxLength: 10, nullable: true),
                    PeriodCycle = table.Column<int>(nullable: false),
                    PeriodYear = table.Column<int>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    UserIdTeacher = table.Column<string>(maxLength: 50, nullable: true),
                    Credit = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CareerUserPensum", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CareerUserPensumDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CareerUserPensumId = table.Column<int>(nullable: false),
                    FirstTest = table.Column<decimal>(nullable: false),
                    SecondTest = table.Column<decimal>(nullable: false),
                    Practice = table.Column<decimal>(nullable: false),
                    FinalTest = table.Column<decimal>(nullable: false),
                    Total = table.Column<decimal>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CareerUserPensumDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CareerUserTeacherPensum",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(maxLength: 50, nullable: true),
                    CareerPensumId = table.Column<int>(nullable: false),
                    SessionCode = table.Column<string>(maxLength: 10, nullable: true),
                    PeriodCycle = table.Column<int>(nullable: false),
                    PeriodYear = table.Column<int>(nullable: false),
                    ClassRoom = table.Column<string>(maxLength: 20, nullable: true),
                    Day = table.Column<int>(nullable: false),
                    TimeToIn = table.Column<DateTime>(nullable: false),
                    TimeToOut = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CareerUserTeacherPensum", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<string>(maxLength: 36, nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    VatNumber = table.Column<string>(maxLength: 30, nullable: true),
                    CompanyName = table.Column<string>(maxLength: 100, nullable: true),
                    State = table.Column<int>(nullable: false),
                    Country = table.Column<int>(nullable: false),
                    Address = table.Column<string>(maxLength: 300, nullable: true),
                    Email = table.Column<string>(maxLength: 100, nullable: true),
                    Phone = table.Column<string>(maxLength: 30, nullable: true),
                    Logo = table.Column<string>(nullable: true),
                    Currency = table.Column<decimal>(nullable: false),
                    License = table.Column<string>(nullable: true),
                    DGIITax = table.Column<decimal>(nullable: false),
                    ReceiptId = table.Column<int>(nullable: false),
                    EmailSMTP = table.Column<string>(nullable: true),
                    PasswordSMTP = table.Column<string>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Complaints",
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
                    table.PrimaryKey("PK_Complaints", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroupPermission",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupId = table.Column<string>(maxLength: 36, nullable: false),
                    PermissionId = table.Column<string>(maxLength: 36, nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupPermission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Matter",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<string>(maxLength: 50, nullable: true),
                    MatterId = table.Column<string>(maxLength: 20, nullable: true),
                    Description = table.Column<string>(nullable: true),
                    PreMatterId = table.Column<string>(maxLength: 20, nullable: true),
                    Credit = table.Column<int>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matter", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NcfHistory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<string>(maxLength: 36, nullable: false),
                    ReceiptId = table.Column<string>(maxLength: 20, nullable: false),
                    NcfType = table.Column<int>(nullable: false),
                    NcfNumber = table.Column<string>(maxLength: 20, nullable: false),
                    ReturnReceiptId = table.Column<string>(maxLength: 20, nullable: false),
                    ReturnNcfNumber = table.Column<string>(maxLength: 20, nullable: false),
                    VatNumber = table.Column<string>(maxLength: 30, nullable: false),
                    Company = table.Column<string>(maxLength: 100, nullable: false),
                    TotalAmount = table.Column<decimal>(nullable: false),
                    TotalAmountWithTax = table.Column<decimal>(nullable: false),
                    TotalTax = table.Column<decimal>(nullable: false),
                    CurrencyChange = table.Column<decimal>(nullable: false),
                    TaxExempt = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NcfHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NcfSequenceDetail",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MasterId = table.Column<string>(maxLength: 36, nullable: true),
                    CompanyId = table.Column<string>(maxLength: 36, nullable: true),
                    NcfId = table.Column<int>(nullable: false),
                    Serie = table.Column<string>(maxLength: 3, nullable: false),
                    SeqStart = table.Column<int>(nullable: false),
                    SeqEnd = table.Column<int>(nullable: false),
                    SeqNext = table.Column<int>(nullable: false),
                    DateStart = table.Column<DateTime>(nullable: false),
                    DateEnd = table.Column<DateTime>(nullable: false),
                    SeqStatus = table.Column<int>(nullable: false),
                    DGIIDescription = table.Column<string>(maxLength: 50, nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NcfSequenceDetail", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NcfType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NcfId = table.Column<int>(nullable: false),
                    IsDefaultSale = table.Column<bool>(nullable: false),
                    IsDefaultCreditMemo = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(maxLength: 50, nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NcfType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permission",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PermissionId = table.Column<string>(maxLength: 36, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RequestUserChangeCareer",
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
                    table.PrimaryKey("PK_RequestUserChangeCareer", x => x.Id);
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
                name: "Transaction",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<string>(nullable: true),
                    ReceiptId = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    IsReturn = table.Column<bool>(nullable: false),
                    TotalAmount = table.Column<decimal>(nullable: false),
                    TotalAmountWithTax = table.Column<decimal>(nullable: false),
                    TotalTax = table.Column<decimal>(nullable: false),
                    TaxExempt = table.Column<bool>(nullable: false),
                    BatchClose = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransactionsDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<string>(nullable: true),
                    ReceiptId = table.Column<string>(nullable: true),
                    IsReturn = table.Column<bool>(nullable: false),
                    SessionCode = table.Column<string>(nullable: true),
                    PeriodCycle = table.Column<int>(nullable: false),
                    PeriodYear = table.Column<int>(nullable: false),
                    TotalAmount = table.Column<decimal>(nullable: false),
                    TotalAmountWithTax = table.Column<decimal>(nullable: false),
                    TotalTax = table.Column<decimal>(nullable: false),
                    TaxExempt = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionsDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MasterId = table.Column<string>(maxLength: 36, nullable: true),
                    CompanyId = table.Column<string>(maxLength: 36, nullable: true),
                    GroupId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(maxLength: 20, nullable: false),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    Address = table.Column<string>(maxLength: 300, nullable: true),
                    Gender = table.Column<int>(nullable: false),
                    CareerId = table.Column<string>(maxLength: 36, nullable: true),
                    VatNumber = table.Column<string>(nullable: true),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    Phone = table.Column<string>(maxLength: 30, nullable: false),
                    Average = table.Column<decimal>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserGroup",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupId = table.Column<string>(maxLength: 36, nullable: true),
                    CompanyId = table.Column<string>(maxLength: 36, nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserMatter",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<string>(maxLength: 50, nullable: true),
                    UserId = table.Column<string>(maxLength: 50, nullable: true),
                    CareerPensumId = table.Column<int>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMatter", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
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
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
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
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
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
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
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
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(nullable: true)
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
                name: "IX_Batch_Id",
                table: "Batch",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Career_Id",
                table: "Career",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CareerPensum_Id",
                table: "CareerPensum",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CareerUserPensum_Id",
                table: "CareerUserPensum",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CareerUserPensumDetails_Id",
                table: "CareerUserPensumDetails",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CareerUserTeacherPensum_Id",
                table: "CareerUserTeacherPensum",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Company_Id",
                table: "Company",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Complaints_Id",
                table: "Complaints",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_GroupPermission_Id",
                table: "GroupPermission",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Matter_Id",
                table: "Matter",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_NcfHistory_Id",
                table: "NcfHistory",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_NcfSequenceDetail_Id",
                table: "NcfSequenceDetail",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_NcfType_NcfId",
                table: "NcfType",
                column: "NcfId");

            migrationBuilder.CreateIndex(
                name: "IX_Permission_Id",
                table: "Permission",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_RequestUserChangeCareer_Id",
                table: "RequestUserChangeCareer",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_RequestUserMatter_Id",
                table: "RequestUserMatter",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_Id",
                table: "Transaction",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionsDetails_Id",
                table: "TransactionsDetails",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_User_Id",
                table: "User",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroup_Id",
                table: "UserGroup",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserMatter_Id",
                table: "UserMatter",
                column: "Id");
        }

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
                name: "Batch");

            migrationBuilder.DropTable(
                name: "Career");

            migrationBuilder.DropTable(
                name: "CareerPensum");

            migrationBuilder.DropTable(
                name: "CareerUserPensum");

            migrationBuilder.DropTable(
                name: "CareerUserPensumDetails");

            migrationBuilder.DropTable(
                name: "CareerUserTeacherPensum");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "Complaints");

            migrationBuilder.DropTable(
                name: "GroupPermission");

            migrationBuilder.DropTable(
                name: "Matter");

            migrationBuilder.DropTable(
                name: "NcfHistory");

            migrationBuilder.DropTable(
                name: "NcfSequenceDetail");

            migrationBuilder.DropTable(
                name: "NcfType");

            migrationBuilder.DropTable(
                name: "Permission");

            migrationBuilder.DropTable(
                name: "RequestUserChangeCareer");

            migrationBuilder.DropTable(
                name: "RequestUserMatter");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "TransactionsDetails");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "UserGroup");

            migrationBuilder.DropTable(
                name: "UserMatter");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
