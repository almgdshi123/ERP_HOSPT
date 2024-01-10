using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ERP_HOSPT.Data.Migrations
{
    public partial class _test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Analysis",
                columns: table => new
                {
                    AnalysisId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    a_Pric = table.Column<string>(nullable: true),
                    a_name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Analysis", x => x.AnalysisId);
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    cityId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    cit_name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.cityId);
                });

            migrationBuilder.CreateTable(
                name: "Depart",
                columns: table => new
                {
                    Departno = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    dept_name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Depart", x => x.Departno);
                });

            migrationBuilder.CreateTable(
                name: "Drug",
                columns: table => new
                {
                    DrugId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    d_name = table.Column<string>(nullable: true),
                    d_pric = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drug", x => x.DrugId);
                });

            migrationBuilder.CreateTable(
                name: "Qualify",
                columns: table => new
                {
                    QualifyId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    q_name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Qualify", x => x.QualifyId);
                });

            migrationBuilder.CreateTable(
                name: "Region",
                columns: table => new
                {
                    RegionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    cityId = table.Column<int>(nullable: false),
                    reg_name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Region", x => x.RegionId);
                    table.ForeignKey(
                        name: "FK_Region_City_cityId",
                        column: x => x.cityId,
                        principalTable: "City",
                        principalColumn: "cityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Patient",
                columns: table => new
                {
                    PatientId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Patientname = table.Column<string>(nullable: false),
                    RegionId = table.Column<int>(nullable: false),
                    cityId = table.Column<int>(nullable: false),
                    pa_addr = table.Column<string>(nullable: false),
                    pa_data = table.Column<string>(nullable: false),
                    pa_email = table.Column<string>(nullable: true),
                    pa_job = table.Column<string>(nullable: false),
                    pa_mobile = table.Column<string>(nullable: false),
                    pa_nat = table.Column<string>(nullable: false),
                    pa_note = table.Column<string>(nullable: false),
                    pa_phone = table.Column<string>(nullable: false),
                    pa_sex = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient", x => x.PatientId);
                    table.ForeignKey(
                        name: "FK_Patient_Region_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Region",
                        principalColumn: "RegionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Patient_City_cityId",
                        column: x => x.cityId,
                        principalTable: "City",
                        principalColumn: "cityId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Physician",
                columns: table => new
                {
                    PhysicianId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Departno = table.Column<int>(nullable: false),
                    QualifyId = table.Column<int>(nullable: false),
                    RegionId = table.Column<int>(nullable: false),
                    cityId = table.Column<int>(nullable: false),
                    phy_addr = table.Column<string>(nullable: false),
                    phy_birth = table.Column<string>(nullable: false),
                    phy_emil = table.Column<string>(nullable: true),
                    phy_name = table.Column<string>(nullable: false),
                    phy_phone = table.Column<string>(nullable: true),
                    phy_sex = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Physician", x => x.PhysicianId);
                    table.ForeignKey(
                        name: "FK_Physician_Depart_Departno",
                        column: x => x.Departno,
                        principalTable: "Depart",
                        principalColumn: "Departno",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Physician_Qualify_QualifyId",
                        column: x => x.QualifyId,
                        principalTable: "Qualify",
                        principalColumn: "QualifyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Physician_Region_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Region",
                        principalColumn: "RegionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Physician_City_cityId",
                        column: x => x.cityId,
                        principalTable: "City",
                        principalColumn: "cityId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Interview",
                columns: table => new
                {
                    interviewId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PatientId = table.Column<int>(nullable: false),
                    PhysicianId = table.Column<int>(nullable: false),
                    inter_date = table.Column<string>(nullable: true),
                    inter_notes = table.Column<string>(nullable: true),
                    inter_type = table.Column<string>(nullable: true),
                    userId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interview", x => x.interviewId);
                    table.ForeignKey(
                        name: "FK_Interview_Patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Interview_Physician_PhysicianId",
                        column: x => x.PhysicianId,
                        principalTable: "Physician",
                        principalColumn: "PhysicianId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Interview_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Prescribtion",
                columns: table => new
                {
                    PrescribtionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Dig = table.Column<string>(nullable: true),
                    DrugId = table.Column<int>(nullable: false),
                    PatientId = table.Column<int>(nullable: false),
                    PhysicianId = table.Column<int>(nullable: false),
                    Pre_date = table.Column<string>(nullable: true),
                    pre_detail = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescribtion", x => x.PrescribtionId);
                    table.ForeignKey(
                        name: "FK_Prescribtion_Drug_DrugId",
                        column: x => x.DrugId,
                        principalTable: "Drug",
                        principalColumn: "DrugId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prescribtion_Patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prescribtion_Physician_PhysicianId",
                        column: x => x.PhysicianId,
                        principalTable: "Physician",
                        principalColumn: "PhysicianId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "R_analysis",
                columns: table => new
                {
                    R_analysisId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AnalysisId = table.Column<int>(nullable: false),
                    PatientId = table.Column<int>(nullable: false),
                    PhysicianId = table.Column<int>(nullable: false),
                    interviewId = table.Column<int>(nullable: false),
                    r_date = table.Column<string>(nullable: true),
                    r_describe = table.Column<string>(nullable: true),
                    r_result = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_R_analysis", x => x.R_analysisId);
                    table.ForeignKey(
                        name: "FK_R_analysis_Analysis_AnalysisId",
                        column: x => x.AnalysisId,
                        principalTable: "Analysis",
                        principalColumn: "AnalysisId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_R_analysis_Patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_R_analysis_Physician_PhysicianId",
                        column: x => x.PhysicianId,
                        principalTable: "Physician",
                        principalColumn: "PhysicianId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_R_analysis_Interview_interviewId",
                        column: x => x.interviewId,
                        principalTable: "Interview",
                        principalColumn: "interviewId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Interview_PatientId",
                table: "Interview",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Interview_PhysicianId",
                table: "Interview",
                column: "PhysicianId");

            migrationBuilder.CreateIndex(
                name: "IX_Interview_userId",
                table: "Interview",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_RegionId",
                table: "Patient",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_cityId",
                table: "Patient",
                column: "cityId");

            migrationBuilder.CreateIndex(
                name: "IX_Physician_Departno",
                table: "Physician",
                column: "Departno");

            migrationBuilder.CreateIndex(
                name: "IX_Physician_QualifyId",
                table: "Physician",
                column: "QualifyId");

            migrationBuilder.CreateIndex(
                name: "IX_Physician_RegionId",
                table: "Physician",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Physician_cityId",
                table: "Physician",
                column: "cityId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescribtion_DrugId",
                table: "Prescribtion",
                column: "DrugId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescribtion_PatientId",
                table: "Prescribtion",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescribtion_PhysicianId",
                table: "Prescribtion",
                column: "PhysicianId");

            migrationBuilder.CreateIndex(
                name: "IX_R_analysis_AnalysisId",
                table: "R_analysis",
                column: "AnalysisId");

            migrationBuilder.CreateIndex(
                name: "IX_R_analysis_PatientId",
                table: "R_analysis",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_R_analysis_PhysicianId",
                table: "R_analysis",
                column: "PhysicianId");

            migrationBuilder.CreateIndex(
                name: "IX_R_analysis_interviewId",
                table: "R_analysis",
                column: "interviewId");

            migrationBuilder.CreateIndex(
                name: "IX_Region_cityId",
                table: "Region",
                column: "cityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Prescribtion");

            migrationBuilder.DropTable(
                name: "R_analysis");

            migrationBuilder.DropTable(
                name: "Drug");

            migrationBuilder.DropTable(
                name: "Analysis");

            migrationBuilder.DropTable(
                name: "Interview");

            migrationBuilder.DropTable(
                name: "Patient");

            migrationBuilder.DropTable(
                name: "Physician");

            migrationBuilder.DropTable(
                name: "Depart");

            migrationBuilder.DropTable(
                name: "Qualify");

            migrationBuilder.DropTable(
                name: "Region");

            migrationBuilder.DropTable(
                name: "City");
        }
    }
}
