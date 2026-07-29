using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Learning.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFieldsParameters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsLocked",
                table: "Sections",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<string>(
                name: "TitleVideoUrl",
                table: "InvtensivesVideos",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsLocked",
                table: "Invtensives",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<string>(
                name: "TitleVideoUrl",
                table: "ExamVideos",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsLocked",
                table: "ExamExplanations",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsLocked",
                table: "Courses",
                type: "bit",
                nullable: false,
                defaultValue: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLocked",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "TitleVideoUrl",
                table: "InvtensivesVideos");

            migrationBuilder.DropColumn(
                name: "IsLocked",
                table: "Invtensives");

            migrationBuilder.DropColumn(
                name: "TitleVideoUrl",
                table: "ExamVideos");

            migrationBuilder.DropColumn(
                name: "IsLocked",
                table: "ExamExplanations");

            migrationBuilder.DropColumn(
                name: "IsLocked",
                table: "Courses");
        }
    }
}
