using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Manager.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Roles",
                table: "User",
                type: "VARCHAR(15)",
                nullable: false,
                defaultValue: "",
                collation: "utf8mb4_0900_ai_ci")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<bool>(
                name: "concluded",
                table: "Tasks",
                type: "TINYINT(1)",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "TINYINT(1)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Roles",
                table: "User");

            migrationBuilder.AlterColumn<bool>(
                name: "concluded",
                table: "Tasks",
                type: "TINYINT(1)",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "TINYINT(1)",
                oldNullable: true);
        }
    }
}
