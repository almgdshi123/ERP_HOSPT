using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ERP_HOSPT.Data.Migrations
{
    public partial class addculstate1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.AddColumn<bool>(
                name: "trboll",
                table: "R_analysis",
                nullable: true,
                defaultValue: true);

           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
          
            migrationBuilder.DropColumn(
                name: "trboll",
                table: "R_analysis");

          
        }
    }
}
