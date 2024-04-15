using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LIbrary.Migrations
{
    /// <inheritdoc />
    public partial class First : Migration
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
                    Discriminator = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                });

            migrationBuilder.CreateTable(
                name: "Author",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Author", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BorrowItemStatus",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BorrowItemStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FineStatus",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false),
                    fineId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FineStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReviewRating",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    review = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    rating = table.Column<int>(type: "int", nullable: true),
                    borrowItemId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewRating", x => x.Id);
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
                name: "Fine",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NumberOfDays = table.Column<int>(type: "int", nullable: false),
                    borrowItemId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fineStatusId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fine_FineStatus_fineStatusId",
                        column: x => x.fineStatusId,
                        principalTable: "FineStatus",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dateOfCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    coverUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    price = table.Column<int>(type: "int", nullable: false),
                    authorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    genreId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Book_Author_authorId",
                        column: x => x.authorId,
                        principalTable: "Author",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Book_Genre_genreId",
                        column: x => x.genreId,
                        principalTable: "Genre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookCopy",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    bookId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookCopy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookCopy_Book_bookId",
                        column: x => x.bookId,
                        principalTable: "Book",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BorrowItem",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    startDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    endDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    supposedEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    bookCopyId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    readerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    borrowItemStatusId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    reviewRatingId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    fineId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BorrowItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BorrowItem_AspNetUsers_readerId",
                        column: x => x.readerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BorrowItem_BookCopy_bookCopyId",
                        column: x => x.bookCopyId,
                        principalTable: "BookCopy",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BorrowItem_BorrowItemStatus_borrowItemStatusId",
                        column: x => x.borrowItemStatusId,
                        principalTable: "BorrowItemStatus",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BorrowItem_Fine_fineId",
                        column: x => x.fineId,
                        principalTable: "Fine",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BorrowItem_ReviewRating_reviewRatingId",
                        column: x => x.reviewRatingId,
                        principalTable: "ReviewRating",
                        principalColumn: "Id");
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
                name: "IX_Book_authorId",
                table: "Book",
                column: "authorId");

            migrationBuilder.CreateIndex(
                name: "IX_Book_genreId",
                table: "Book",
                column: "genreId");

            migrationBuilder.CreateIndex(
                name: "IX_BookCopy_bookId",
                table: "BookCopy",
                column: "bookId");

            migrationBuilder.CreateIndex(
                name: "IX_BorrowItem_bookCopyId",
                table: "BorrowItem",
                column: "bookCopyId");

            migrationBuilder.CreateIndex(
                name: "IX_BorrowItem_borrowItemStatusId",
                table: "BorrowItem",
                column: "borrowItemStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_BorrowItem_fineId",
                table: "BorrowItem",
                column: "fineId",
                unique: true,
                filter: "[fineId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BorrowItem_readerId",
                table: "BorrowItem",
                column: "readerId");

            migrationBuilder.CreateIndex(
                name: "IX_BorrowItem_reviewRatingId",
                table: "BorrowItem",
                column: "reviewRatingId",
                unique: true,
                filter: "[reviewRatingId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Fine_fineStatusId",
                table: "Fine",
                column: "fineStatusId",
                unique: true,
                filter: "[fineStatusId] IS NOT NULL");
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
                name: "BorrowItem");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "BookCopy");

            migrationBuilder.DropTable(
                name: "BorrowItemStatus");

            migrationBuilder.DropTable(
                name: "Fine");

            migrationBuilder.DropTable(
                name: "ReviewRating");

            migrationBuilder.DropTable(
                name: "Book");

            migrationBuilder.DropTable(
                name: "FineStatus");

            migrationBuilder.DropTable(
                name: "Author");

            migrationBuilder.DropTable(
                name: "Genre");
        }
    }
}
