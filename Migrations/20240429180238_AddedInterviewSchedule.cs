using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobFairManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddedInterviewSchedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InterviewScheduleId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "InterviewSchedule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterviewSchedule", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Slot",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Reserved = table.Column<bool>(type: "bit", nullable: false),
                    CandidateId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    InterviewScheduleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slot", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Slot_AspNetUsers_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Slot_InterviewSchedule_InterviewScheduleId",
                        column: x => x.InterviewScheduleId,
                        principalTable: "InterviewSchedule",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_InterviewScheduleId",
                table: "AspNetUsers",
                column: "InterviewScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Slot_CandidateId",
                table: "Slot",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_Slot_InterviewScheduleId",
                table: "Slot",
                column: "InterviewScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_InterviewSchedule_InterviewScheduleId",
                table: "AspNetUsers",
                column: "InterviewScheduleId",
                principalTable: "InterviewSchedule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_InterviewSchedule_InterviewScheduleId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Slot");

            migrationBuilder.DropTable(
                name: "InterviewSchedule");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_InterviewScheduleId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "InterviewScheduleId",
                table: "AspNetUsers");
        }
    }
}
