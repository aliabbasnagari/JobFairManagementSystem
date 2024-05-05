using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobFairManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class FeedBackUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_AspNetUsers_FromUserId",
                table: "Feedbacks");

            migrationBuilder.AlterColumn<string>(
                name: "FromUserId",
                table: "Feedbacks",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_AspNetUsers_FromUserId",
                table: "Feedbacks",
                column: "FromUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_AspNetUsers_FromUserId",
                table: "Feedbacks");

            migrationBuilder.AlterColumn<string>(
                name: "FromUserId",
                table: "Feedbacks",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_AspNetUsers_FromUserId",
                table: "Feedbacks",
                column: "FromUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
