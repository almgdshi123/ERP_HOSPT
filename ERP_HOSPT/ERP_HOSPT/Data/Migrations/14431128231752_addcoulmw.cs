using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ERP_HOSPT.Data.Migrations
{
    public partial class addcoulmw : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "userId",
                table: "Physician",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Physician_userId",
                table: "Physician",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Physician_AspNetUsers_userId",
                table: "Physician",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Physician_AspNetUsers_userId",
                table: "Physician");

            migrationBuilder.DropIndex(
                name: "IX_Physician_userId",
                table: "Physician");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "Physician");
        }
    }
}
