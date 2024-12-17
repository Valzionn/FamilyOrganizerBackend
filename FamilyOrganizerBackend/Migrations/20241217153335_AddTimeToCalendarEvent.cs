using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FamilyOrganizerBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddTimeToCalendarEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "Time",
                table: "CalendarEvents",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Time",
                table: "CalendarEvents");
        }
    }
}
