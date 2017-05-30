using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MetaTesina.Data.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.CreateTable(
                name: "Asset",
                columns: table => new
                {
                    AssetID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AssetDescription = table.Column<string>(maxLength: 250, nullable: false),
                    AssetName = table.Column<string>(maxLength: 20, nullable: false),
                    AssetPath = table.Column<string>(maxLength: 250, nullable: false),
                    AssetTypeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asset", x => x.AssetID);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CategoryDescription = table.Column<string>(maxLength: 250, nullable: false),
                    CategoryName = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "Article",
                columns: table => new
                {
                    ArticleID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ArticleContent = table.Column<string>(maxLength: 10000, nullable: false),
                    ArticleCreateDate = table.Column<DateTime>(nullable: false),
                    ArticleDescription = table.Column<string>(maxLength: 250, nullable: false),
                    ArticleLinkImgID = table.Column<int>(nullable: false),
                    ArticleMainImgID = table.Column<int>(nullable: false),
                    ArticleModifyDate = table.Column<DateTime>(nullable: false),
                    ArticleTitle = table.Column<string>(maxLength: 40, nullable: false),
                    AuthorID = table.Column<int>(nullable: false),
                    CategoryID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Article", x => x.ArticleID);
                    table.ForeignKey(
                        name: "FK_Article_Asset_ArticleLinkImgID",
                        column: x => x.ArticleLinkImgID,
                        principalTable: "Asset",
                        principalColumn: "AssetID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Article_Asset_ArticleMainImgID",
                        column: x => x.ArticleMainImgID,
                        principalTable: "Asset",
                        principalColumn: "AssetID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Article_Category_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Category",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Article_ArticleLinkImgID",
                table: "Article",
                column: "ArticleLinkImgID");

            migrationBuilder.CreateIndex(
                name: "IX_Article_ArticleMainImgID",
                table: "Article",
                column: "ArticleMainImgID");

            migrationBuilder.CreateIndex(
                name: "IX_Article_CategoryID",
                table: "Article",
                column: "CategoryID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Article");

            migrationBuilder.DropTable(
                name: "Asset");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName");
        }
    }
}
