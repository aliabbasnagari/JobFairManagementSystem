using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobFairManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class NullableInterviewSchedule2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_InterviewSchedule_InterviewScheduleId",
                table: "AspNetUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_InterviewSchedule_InterviewScheduleId",
                table: "AspNetUsers",
                column: "InterviewScheduleId",
                principalTable: "InterviewSchedule",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_InterviewSchedule_InterviewScheduleId",
                table: "AspNetUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_InterviewSchedule_InterviewScheduleId",
                table: "AspNetUsers",
                column: "InterviewScheduleId",
                principalTable: "InterviewSchedule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
