using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobFairManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddedSlotTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Slot_AspNetUsers_CandidateId",
                table: "Slot");

            migrationBuilder.DropForeignKey(
                name: "FK_Slot_InterviewSchedules_InterviewScheduleId",
                table: "Slot");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Slot",
                table: "Slot");

            migrationBuilder.RenameTable(
                name: "Slot",
                newName: "Slots");

            migrationBuilder.RenameIndex(
                name: "IX_Slot_InterviewScheduleId",
                table: "Slots",
                newName: "IX_Slots_InterviewScheduleId");

            migrationBuilder.RenameIndex(
                name: "IX_Slot_CandidateId",
                table: "Slots",
                newName: "IX_Slots_CandidateId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Slots",
                table: "Slots",
                column: "Id");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Slots_AspNetUsers_CandidateId",
                table: "Slots");

            migrationBuilder.DropForeignKey(
                name: "FK_Slots_InterviewSchedules_InterviewScheduleId",
                table: "Slots");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Slots",
                table: "Slots");

            migrationBuilder.RenameTable(
                name: "Slots",
                newName: "Slot");

            migrationBuilder.RenameIndex(
                name: "IX_Slots_InterviewScheduleId",
                table: "Slot",
                newName: "IX_Slot_InterviewScheduleId");

            migrationBuilder.RenameIndex(
                name: "IX_Slots_CandidateId",
                table: "Slot",
                newName: "IX_Slot_CandidateId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Slot",
                table: "Slot",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Slot_AspNetUsers_CandidateId",
                table: "Slot",
                column: "CandidateId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Slot_InterviewSchedules_InterviewScheduleId",
                table: "Slot",
                column: "InterviewScheduleId",
                principalTable: "InterviewSchedules",
                principalColumn: "Id");
        }
    }
}
