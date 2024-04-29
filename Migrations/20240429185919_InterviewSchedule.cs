using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobFairManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class InterviewSchedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_InterviewSchedule_InterviewScheduleId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Slot_InterviewSchedule_InterviewScheduleId",
                table: "Slot");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InterviewSchedule",
                table: "InterviewSchedule");

            migrationBuilder.RenameTable(
                name: "InterviewSchedule",
                newName: "InterviewSchedules");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InterviewSchedules",
                table: "InterviewSchedules",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_InterviewSchedules_InterviewScheduleId",
                table: "AspNetUsers",
                column: "InterviewScheduleId",
                principalTable: "InterviewSchedules",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Slot_InterviewSchedules_InterviewScheduleId",
                table: "Slot",
                column: "InterviewScheduleId",
                principalTable: "InterviewSchedules",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_InterviewSchedules_InterviewScheduleId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Slot_InterviewSchedules_InterviewScheduleId",
                table: "Slot");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InterviewSchedules",
                table: "InterviewSchedules");

            migrationBuilder.RenameTable(
                name: "InterviewSchedules",
                newName: "InterviewSchedule");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InterviewSchedule",
                table: "InterviewSchedule",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_InterviewSchedule_InterviewScheduleId",
                table: "AspNetUsers",
                column: "InterviewScheduleId",
                principalTable: "InterviewSchedule",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Slot_InterviewSchedule_InterviewScheduleId",
                table: "Slot",
                column: "InterviewScheduleId",
                principalTable: "InterviewSchedule",
                principalColumn: "Id");
        }
    }
}
