using Microsoft.EntityFrameworkCore.Migrations;

namespace backend.Migrations
{
    public partial class AppraisalImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppraisalImage_Appraisals_appraisalId",
                table: "AppraisalImage");

            migrationBuilder.DropForeignKey(
                name: "FK_AppraisalImage_Furniture_furnitureId",
                table: "AppraisalImage");

            migrationBuilder.DropForeignKey(
                name: "FK_AppraisalImage_Images_s3ImageId",
                table: "AppraisalImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppraisalImage",
                table: "AppraisalImage");

            migrationBuilder.RenameTable(
                name: "AppraisalImage",
                newName: "AppraisalImages");

            migrationBuilder.RenameIndex(
                name: "IX_AppraisalImage_s3ImageId",
                table: "AppraisalImages",
                newName: "IX_AppraisalImages_s3ImageId");

            migrationBuilder.RenameIndex(
                name: "IX_AppraisalImage_furnitureId",
                table: "AppraisalImages",
                newName: "IX_AppraisalImages_furnitureId");

            migrationBuilder.RenameIndex(
                name: "IX_AppraisalImage_appraisalId",
                table: "AppraisalImages",
                newName: "IX_AppraisalImages_appraisalId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppraisalImages",
                table: "AppraisalImages",
                column: "imageId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppraisalImages_Appraisals_appraisalId",
                table: "AppraisalImages",
                column: "appraisalId",
                principalTable: "Appraisals",
                principalColumn: "appraisalId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppraisalImages_Furniture_furnitureId",
                table: "AppraisalImages",
                column: "furnitureId",
                principalTable: "Furniture",
                principalColumn: "furnitureId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppraisalImages_Images_s3ImageId",
                table: "AppraisalImages",
                column: "s3ImageId",
                principalTable: "Images",
                principalColumn: "s3ImageId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppraisalImages_Appraisals_appraisalId",
                table: "AppraisalImages");

            migrationBuilder.DropForeignKey(
                name: "FK_AppraisalImages_Furniture_furnitureId",
                table: "AppraisalImages");

            migrationBuilder.DropForeignKey(
                name: "FK_AppraisalImages_Images_s3ImageId",
                table: "AppraisalImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppraisalImages",
                table: "AppraisalImages");

            migrationBuilder.RenameTable(
                name: "AppraisalImages",
                newName: "AppraisalImage");

            migrationBuilder.RenameIndex(
                name: "IX_AppraisalImages_s3ImageId",
                table: "AppraisalImage",
                newName: "IX_AppraisalImage_s3ImageId");

            migrationBuilder.RenameIndex(
                name: "IX_AppraisalImages_furnitureId",
                table: "AppraisalImage",
                newName: "IX_AppraisalImage_furnitureId");

            migrationBuilder.RenameIndex(
                name: "IX_AppraisalImages_appraisalId",
                table: "AppraisalImage",
                newName: "IX_AppraisalImage_appraisalId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppraisalImage",
                table: "AppraisalImage",
                column: "imageId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppraisalImage_Appraisals_appraisalId",
                table: "AppraisalImage",
                column: "appraisalId",
                principalTable: "Appraisals",
                principalColumn: "appraisalId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppraisalImage_Furniture_furnitureId",
                table: "AppraisalImage",
                column: "furnitureId",
                principalTable: "Furniture",
                principalColumn: "furnitureId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppraisalImage_Images_s3ImageId",
                table: "AppraisalImage",
                column: "s3ImageId",
                principalTable: "Images",
                principalColumn: "s3ImageId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
