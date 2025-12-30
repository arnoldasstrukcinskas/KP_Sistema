using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KP_Sistema.DATA.Migrations
{
    /// <inheritdoc />
    public partial class FixTaskEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UtilityTasks_Communities_CommunityId",
                table: "UtilityTasks");

            migrationBuilder.AlterColumn<int>(
                name: "CommunityId",
                table: "UtilityTasks",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_UtilityTasks_Communities_CommunityId",
                table: "UtilityTasks",
                column: "CommunityId",
                principalTable: "Communities",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UtilityTasks_Communities_CommunityId",
                table: "UtilityTasks");

            migrationBuilder.AlterColumn<int>(
                name: "CommunityId",
                table: "UtilityTasks",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UtilityTasks_Communities_CommunityId",
                table: "UtilityTasks",
                column: "CommunityId",
                principalTable: "Communities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
