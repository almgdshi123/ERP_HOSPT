using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ERP_HOSPT.Data.Migrations
{
    public partial class addclDiagnsis2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.CreateTable(
                name: "Diagnosis",
                columns: table => new
                {
                    DiagnosisId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Diagnosis_date = table.Column<string>(nullable: true),
                    Dig = table.Column<string>(nullable: true),
                    Drug = table.Column<string>(nullable: true),
                    Drug_detail = table.Column<string>(nullable: true),
                    PatientId = table.Column<int>(nullable: false),
                    PhysicianId = table.Column<int>(nullable: false),
                    interviewId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diagnosis", x => x.DiagnosisId);
                    table.ForeignKey(
                        name: "FK_Diagnosis_Patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Diagnosis_Physician_PhysicianId",
                        column: x => x.PhysicianId,
                        principalTable: "Physician",
                        principalColumn: "PhysicianId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Diagnosis_Interview_interviewId",
                        column: x => x.interviewId,
                        principalTable: "Interview",
                        principalColumn: "interviewId",
                        onDelete: ReferentialAction.NoAction);
                });

           

            migrationBuilder.CreateIndex(
                name: "IX_Diagnosis_PatientId",
                table: "Diagnosis",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Diagnosis_PhysicianId",
                table: "Diagnosis",
                column: "PhysicianId");

            migrationBuilder.CreateIndex(
                name: "IX_Diagnosis_interviewId",
                table: "Diagnosis",
                column: "interviewId");

           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Physician_AspNetUsers_userId",
                table: "Physician");

            migrationBuilder.DropTable(
                name: "Diagnosis");


          
        }
    }
}
