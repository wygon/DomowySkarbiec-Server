using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TukanTomek.Server.Migrations
{
    /// <inheritdoc />
    public partial class UserFamilyRoleMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FamilyRole",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FamilyRole",
                table: "Users");
        }
    }
}
