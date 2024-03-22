using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace learnenglish.webui.Migrations
{
    /// <inheritdoc />
    public partial class AddingProfileUrlColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfileUrl",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfileUrl",
                table: "AspNetUsers");
        }
    }
}
