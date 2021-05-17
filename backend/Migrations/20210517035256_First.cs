using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace backend.Migrations
{
    public partial class First : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Colors",
                columns: table => new
                {
                    colorId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: false),
                    updatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colors", x => x.colorId);
                });

            migrationBuilder.CreateTable(
                name: "FurnitureTypes",
                columns: table => new
                {
                    typeId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: false),
                    updatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FurnitureTypes", x => x.typeId);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    s3ImageId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    url = table.Column<string>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: false),
                    updatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.s3ImageId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    userId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    firstName = table.Column<string>(nullable: true),
                    lastName = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    oauthSubject = table.Column<string>(nullable: true),
                    oauthIssuer = table.Column<string>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: false),
                    updatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.userId);
                });

            migrationBuilder.CreateTable(
                name: "Furniture",
                columns: table => new
                {
                    furnitureId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    typeId = table.Column<int>(nullable: false),
                    height = table.Column<decimal>(nullable: false),
                    length = table.Column<decimal>(nullable: false),
                    width = table.Column<decimal>(nullable: false),
                    estimatedWeight = table.Column<int>(nullable: false),
                    priceFloor = table.Column<decimal>(nullable: false),
                    priceCeiling = table.Column<decimal>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: false),
                    updatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Furniture", x => x.furnitureId);
                    table.ForeignKey(
                        name: "FK_Furniture_FurnitureTypes_typeId",
                        column: x => x.typeId,
                        principalTable: "FurnitureTypes",
                        principalColumn: "typeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Appraisals",
                columns: table => new
                {
                    appraisalId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    userId = table.Column<int>(nullable: false),
                    phoneNumber = table.Column<int>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    height = table.Column<decimal>(nullable: false),
                    length = table.Column<decimal>(nullable: false),
                    width = table.Column<decimal>(nullable: false),
                    sturdy = table.Column<bool>(nullable: false),
                    estimatedWeight = table.Column<int>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: false),
                    updatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appraisals", x => x.appraisalId);
                    table.ForeignKey(
                        name: "FK_Appraisals_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FurnitureHasColors",
                columns: table => new
                {
                    furnitureHasColorId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    furnitureId = table.Column<int>(nullable: false),
                    colorId = table.Column<int>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: false),
                    updatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FurnitureHasColors", x => x.furnitureHasColorId);
                    table.ForeignKey(
                        name: "FK_FurnitureHasColors_Colors_colorId",
                        column: x => x.colorId,
                        principalTable: "Colors",
                        principalColumn: "colorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FurnitureHasColors_Furniture_furnitureId",
                        column: x => x.furnitureId,
                        principalTable: "Furniture",
                        principalColumn: "furnitureId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FurnitureImages",
                columns: table => new
                {
                    imageId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    s3ImageId = table.Column<int>(nullable: false),
                    furnitureId = table.Column<int>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: false),
                    updatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FurnitureImages", x => x.imageId);
                    table.ForeignKey(
                        name: "FK_FurnitureImages_Furniture_furnitureId",
                        column: x => x.furnitureId,
                        principalTable: "Furniture",
                        principalColumn: "furnitureId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FurnitureImages_Images_s3ImageId",
                        column: x => x.s3ImageId,
                        principalTable: "Images",
                        principalColumn: "s3ImageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    saleId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    furnitureId = table.Column<int>(nullable: false),
                    finalPrice = table.Column<decimal>(nullable: false),
                    dateSold = table.Column<DateTime>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.saleId);
                    table.ForeignKey(
                        name: "FK_Sales_Furniture_furnitureId",
                        column: x => x.furnitureId,
                        principalTable: "Furniture",
                        principalColumn: "furnitureId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppraisalImage",
                columns: table => new
                {
                    imageId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    s3ImageId = table.Column<int>(nullable: false),
                    furnitureId = table.Column<int>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: false),
                    updatedAt = table.Column<DateTime>(nullable: false),
                    appraisalId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppraisalImage", x => x.imageId);
                    table.ForeignKey(
                        name: "FK_AppraisalImage_Appraisals_appraisalId",
                        column: x => x.appraisalId,
                        principalTable: "Appraisals",
                        principalColumn: "appraisalId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppraisalImage_Furniture_furnitureId",
                        column: x => x.furnitureId,
                        principalTable: "Furniture",
                        principalColumn: "furnitureId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppraisalImage_Images_s3ImageId",
                        column: x => x.s3ImageId,
                        principalTable: "Images",
                        principalColumn: "s3ImageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppraisalImage_appraisalId",
                table: "AppraisalImage",
                column: "appraisalId");

            migrationBuilder.CreateIndex(
                name: "IX_AppraisalImage_furnitureId",
                table: "AppraisalImage",
                column: "furnitureId");

            migrationBuilder.CreateIndex(
                name: "IX_AppraisalImage_s3ImageId",
                table: "AppraisalImage",
                column: "s3ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Appraisals_userId",
                table: "Appraisals",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Furniture_typeId",
                table: "Furniture",
                column: "typeId");

            migrationBuilder.CreateIndex(
                name: "IX_FurnitureHasColors_colorId",
                table: "FurnitureHasColors",
                column: "colorId");

            migrationBuilder.CreateIndex(
                name: "IX_FurnitureHasColors_furnitureId",
                table: "FurnitureHasColors",
                column: "furnitureId");

            migrationBuilder.CreateIndex(
                name: "IX_FurnitureImages_furnitureId",
                table: "FurnitureImages",
                column: "furnitureId");

            migrationBuilder.CreateIndex(
                name: "IX_FurnitureImages_s3ImageId",
                table: "FurnitureImages",
                column: "s3ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_furnitureId",
                table: "Sales",
                column: "furnitureId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppraisalImage");

            migrationBuilder.DropTable(
                name: "FurnitureHasColors");

            migrationBuilder.DropTable(
                name: "FurnitureImages");

            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropTable(
                name: "Appraisals");

            migrationBuilder.DropTable(
                name: "Colors");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Furniture");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "FurnitureTypes");
        }
    }
}
