using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ERP_HOSPT.Data.Migrations
{
    public partial class Addcuminterv : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CompanyId",
                table: "Patient",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyId",
                table: "Interview",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyId",
                table: "Diagnosis",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patient_CompanyId",
                table: "Patient",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Interview_CompanyId",
                table: "Interview",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Interview_Company_CompanyId",
                table: "Interview",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Patient_Company_CompanyId",
                table: "Patient",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Interview_Company_CompanyId",
                table: "Interview");

            migrationBuilder.DropForeignKey(
                name: "FK_Patient_Company_CompanyId",
                table: "Patient");

            migrationBuilder.DropIndex(
                name: "IX_Patient_CompanyId",
                table: "Patient");

            migrationBuilder.DropIndex(
                name: "IX_Interview_CompanyId",
                table: "Interview");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Interview");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Diagnosis");
        }
    }
}
