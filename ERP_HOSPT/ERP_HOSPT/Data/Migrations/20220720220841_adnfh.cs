using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ERP_HOSPT.Data.Migrations
{
    public partial class adnfh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CompanyId",
                table: "Physician",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Physician_CompanyId",
                table: "Physician",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Physician_Company_CompanyId",
                table: "Physician",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Physician_Company_CompanyId",
                table: "Physician");

            migrationBuilder.DropIndex(
                name: "IX_Physician_CompanyId",
                table: "Physician");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Physician");
        }
    }
}
