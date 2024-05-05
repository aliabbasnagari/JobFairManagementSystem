using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobFairManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddProjectSchedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_InterviewSchedules_InterviewScheduleId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Slots_AspNetUsers_CandidateId",
                table: "Slots");

            migrationBuilder.DropForeignKey(
                name: "FK_Slots_InterviewSchedules_InterviewScheduleId",
                table: "Slots");

            migrationBuilder.DropTable(
                name: "InterviewSchedules");

            migrationBuilder.RenameColumn(
                name: "InterviewScheduleId",
                table: "Slots",
                newName: "ScheduleId");

            migrationBuilder.RenameColumn(
                name: "CandidateId",
                table: "Slots",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Slots_InterviewScheduleId",
                table: "Slots",
                newName: "IX_Slots_ScheduleId");

            migrationBuilder.RenameIndex(
                name: "IX_Slots_CandidateId",
                table: "Slots",
                newName: "IX_Slots_UserId");

            migrationBuilder.AddColumn<int>(
                name: "ProjectScheduleId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Schedule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedule", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ProjectScheduleId",
                table: "AspNetUsers",
                column: "ProjectScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Schedule_InterviewScheduleId",
                table: "AspNetUsers",
                column: "InterviewScheduleId",
                principalTable: "Schedule",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Schedule_ProjectScheduleId",
                table: "AspNetUsers",
                column: "ProjectScheduleId",
                principalTable: "Schedule",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Slots_AspNetUsers_UserId",
                table: "Slots",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Slots_Schedule_ScheduleId",
                table: "Slots",
                column: "ScheduleId",
                principalTable: "Schedule",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Schedule_InterviewScheduleId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Schedule_ProjectScheduleId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Slots_AspNetUsers_UserId",
                table: "Slots");

            migrationBuilder.DropForeignKey(
                name: "FK_Slots_Schedule_ScheduleId",
                table: "Slots");

            migrationBuilder.DropTable(
                name: "Schedule");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ProjectScheduleId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ProjectScheduleId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Slots",
                newName: "CandidateId");

            migrationBuilder.RenameColumn(
                name: "ScheduleId",
                table: "Slots",
                newName: "InterviewScheduleId");

            migrationBuilder.RenameIndex(
                name: "IX_Slots_UserId",
                table: "Slots",
                newName: "IX_Slots_CandidateId");

            migrationBuilder.RenameIndex(
                name: "IX_Slots_ScheduleId",
                table: "Slots",
                newName: "IX_Slots_InterviewScheduleId");

            migrationBuilder.CreateTable(
                name: "InterviewSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterviewSchedules", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_InterviewSchedules_InterviewScheduleId",
                table: "AspNetUsers",
                column: "InterviewScheduleId",
                principalTable: "InterviewSchedules",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Slots_AspNetUsers_CandidateId",
                table: "Slots",
                column: "CandidateId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Slots_InterviewSchedules_InterviewScheduleId",
                table: "Slots",
                column: "InterviewScheduleId",
                principalTable: "InterviewSchedules",
                principalColumn: "Id");
        }
    }
}
