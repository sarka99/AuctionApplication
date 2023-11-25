using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuctionApplication.Migrations
{
    /// <inheritdoc />
    public partial class AuctionDb_UserName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "AuctionDbs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AuctionDbs",
                keyColumn: "Id",
                keyValue: -1,
                columns: new[] { "Deadline", "UserName" },
                values: new object[] { new DateTime(2023, 11, 5, 23, 28, 36, 333, DateTimeKind.Local).AddTicks(6583), "sarka0159@gmail.com" });

            migrationBuilder.UpdateData(
                table: "AuctionDbs",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Deadline", "UserName" },
                values: new object[] { new DateTime(2023, 11, 5, 23, 28, 36, 333, DateTimeKind.Local).AddTicks(6666), "josef.nayyak@gmail.com" });

            migrationBuilder.UpdateData(
                table: "BidDbs",
                keyColumn: "Id",
                keyValue: -2,
                column: "BidTime",
                value: new DateTime(2023, 11, 5, 23, 28, 36, 333, DateTimeKind.Local).AddTicks(6687));

            migrationBuilder.UpdateData(
                table: "BidDbs",
                keyColumn: "Id",
                keyValue: -1,
                column: "BidTime",
                value: new DateTime(2023, 11, 5, 23, 28, 36, 333, DateTimeKind.Local).AddTicks(6679));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "AuctionDbs");

            migrationBuilder.UpdateData(
                table: "AuctionDbs",
                keyColumn: "Id",
                keyValue: -1,
                column: "Deadline",
                value: new DateTime(2023, 11, 4, 23, 1, 43, 415, DateTimeKind.Local).AddTicks(2565));

            migrationBuilder.UpdateData(
                table: "AuctionDbs",
                keyColumn: "Id",
                keyValue: 2,
                column: "Deadline",
                value: new DateTime(2023, 11, 4, 23, 1, 43, 415, DateTimeKind.Local).AddTicks(2643));

            migrationBuilder.UpdateData(
                table: "BidDbs",
                keyColumn: "Id",
                keyValue: -2,
                column: "BidTime",
                value: new DateTime(2023, 11, 4, 23, 1, 43, 415, DateTimeKind.Local).AddTicks(2655));

            migrationBuilder.UpdateData(
                table: "BidDbs",
                keyColumn: "Id",
                keyValue: -1,
                column: "BidTime",
                value: new DateTime(2023, 11, 4, 23, 1, 43, 415, DateTimeKind.Local).AddTicks(2652));
        }
    }
}
