using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ERPDB.Models
{
    public partial class ERPB_DBContext : DbContext
    {
        public virtual DbSet<Analysis> Analysis { get; set; }
        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<Depart> Depart { get; set; }
        public virtual DbSet<Diagnosis> Diagnosis { get; set; }
        public virtual DbSet<Drug> Drug { get; set; }
        public virtual DbSet<Interview> Interview { get; set; }
        public virtual DbSet<Measure> Measure { get; set; }
        public virtual DbSet<Patient> Patient { get; set; }
        public virtual DbSet<Physician> Physician { get; set; }
        public virtual DbSet<Prescribtion> Prescribtion { get; set; }
        public virtual DbSet<Qualify> Qualify { get; set; }
        public virtual DbSet<RAnalysis> RAnalysis { get; set; }
        public virtual DbSet<Region> Region { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=LAPTOP-OVDOKS25;Database=ERPB_DB;User Id=erb; Password=1234;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Analysis>(entity =>
            {
                entity.Property(e => e.AName).HasColumnName("a_name");

                entity.Property(e => e.APric).HasColumnName("a_Pric");

                entity.Property(e => e.Stats)
                    .HasColumnName("stats")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.Property(e => e.CityId).HasColumnName("cityId");

                entity.Property(e => e.CitName)
                    .IsRequired()
                    .HasColumnName("cit_name");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.Property(e => e.CompanyId).ValueGeneratedNever();

                entity.Property(e => e.CompanName).HasColumnName("Compan_name");
            });

            modelBuilder.Entity<Depart>(entity =>
            {
                entity.HasKey(e => e.Departno);

                entity.Property(e => e.DeptName).HasColumnName("dept_name");
            });

            modelBuilder.Entity<Diagnosis>(entity =>
            {
                entity.HasIndex(e => e.InterviewId);

                entity.HasIndex(e => e.PatientId);

                entity.HasIndex(e => e.PhysicianId);

                entity.Property(e => e.DiagnosisDate).HasColumnName("Diagnosis_date");

                entity.Property(e => e.DrugDetail).HasColumnName("Drug_detail");

                entity.Property(e => e.InterviewId).HasColumnName("interviewId");

                entity.HasOne(d => d.Interview)
                    .WithMany(p => p.Diagnosis)
                    .HasForeignKey(d => d.InterviewId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Diagnosis)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Physician)
                    .WithMany(p => p.Diagnosis)
                    .HasForeignKey(d => d.PhysicianId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Drug>(entity =>
            {
                entity.Property(e => e.DName).HasColumnName("d_name");

                entity.Property(e => e.DPric).HasColumnName("d_pric");
            });

            modelBuilder.Entity<Interview>(entity =>
            {
                entity.HasIndex(e => e.PatientId);

                entity.HasIndex(e => e.PhysicianId);

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.InterviewId).HasColumnName("interviewId");

                entity.Property(e => e.InterDate).HasColumnName("inter_date");

                entity.Property(e => e.InterNotes).HasColumnName("inter_notes");

                entity.Property(e => e.InterType).HasColumnName("inter_type");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnName("userId");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Interview)
                    .HasForeignKey(d => d.PatientId);

                entity.HasOne(d => d.Physician)
                    .WithMany(p => p.Interview)
                    .HasForeignKey(d => d.PhysicianId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Interview)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Measure>(entity =>
            {
                entity.Property(e => e.MeasureName).HasColumnName("Measure_name");
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasIndex(e => e.CityId);

                entity.HasIndex(e => e.Patientname)
                    .HasName("NonClusteredIndex_Patient");

                entity.HasIndex(e => e.RegionId);

                entity.Property(e => e.CityId).HasColumnName("cityId");

                entity.Property(e => e.PaAddr)
                    .IsRequired()
                    .HasColumnName("pa_addr");

                entity.Property(e => e.PaData)
                    .IsRequired()
                    .HasColumnName("pa_data");

                entity.Property(e => e.PaEmail).HasColumnName("pa_email");

                entity.Property(e => e.PaJob)
                    .IsRequired()
                    .HasColumnName("pa_job");

                entity.Property(e => e.PaMobile)
                    .IsRequired()
                    .HasColumnName("pa_mobile");

                entity.Property(e => e.PaNat)
                    .IsRequired()
                    .HasColumnName("pa_nat");

                entity.Property(e => e.PaNote)
                    .IsRequired()
                    .HasColumnName("pa_note");

                entity.Property(e => e.PaPhone)
                    .IsRequired()
                    .HasColumnName("pa_phone");

                entity.Property(e => e.PaSex)
                    .IsRequired()
                    .HasColumnName("pa_sex");

                entity.Property(e => e.Patientname)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Patient)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.Patient)
                    .HasForeignKey(d => d.RegionId);
            });

            modelBuilder.Entity<Physician>(entity =>
            {
                entity.HasIndex(e => e.CityId);

                entity.HasIndex(e => e.Departno);

                entity.HasIndex(e => e.QualifyId);

                entity.HasIndex(e => e.RegionId);

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.CityId).HasColumnName("cityId");

                entity.Property(e => e.PhyAddr)
                    .IsRequired()
                    .HasColumnName("phy_addr");

                entity.Property(e => e.PhyBirth)
                    .IsRequired()
                    .HasColumnName("phy_birth");

                entity.Property(e => e.PhyEmil).HasColumnName("phy_emil");

                entity.Property(e => e.PhyName)
                    .IsRequired()
                    .HasColumnName("phy_name");

                entity.Property(e => e.PhyPhone).HasColumnName("phy_phone");

                entity.Property(e => e.PhySex)
                    .IsRequired()
                    .HasColumnName("phy_sex");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Physician)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.DepartnoNavigation)
                    .WithMany(p => p.Physician)
                    .HasForeignKey(d => d.Departno);

                entity.HasOne(d => d.Qualify)
                    .WithMany(p => p.Physician)
                    .HasForeignKey(d => d.QualifyId);

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.Physician)
                    .HasForeignKey(d => d.RegionId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Physician)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Prescribtion>(entity =>
            {
                entity.HasIndex(e => e.DrugId);

                entity.HasIndex(e => e.PatientId);

                entity.HasIndex(e => e.PhysicianId);

                entity.Property(e => e.PreDate).HasColumnName("Pre_date");

                entity.Property(e => e.PreDetail).HasColumnName("pre_detail");

                entity.HasOne(d => d.Drug)
                    .WithMany(p => p.Prescribtion)
                    .HasForeignKey(d => d.DrugId);

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Prescribtion)
                    .HasForeignKey(d => d.PatientId);

                entity.HasOne(d => d.Physician)
                    .WithMany(p => p.Prescribtion)
                    .HasForeignKey(d => d.PhysicianId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Qualify>(entity =>
            {
                entity.Property(e => e.QName).HasColumnName("q_name");
            });

            modelBuilder.Entity<RAnalysis>(entity =>
            {
                entity.ToTable("R_analysis");

                entity.HasIndex(e => e.AnalysisId);

                entity.HasIndex(e => e.InterviewId);

                entity.HasIndex(e => e.PatientId);

                entity.HasIndex(e => e.PhysicianId);

                entity.Property(e => e.RAnalysisId).HasColumnName("R_analysisId");

                entity.Property(e => e.InterviewId).HasColumnName("interviewId");

                entity.Property(e => e.RDate).HasColumnName("r_date");

                entity.Property(e => e.RDescribe).HasColumnName("r_describe");

                entity.Property(e => e.RResult).HasColumnName("r_result");

                entity.Property(e => e.State1).HasDefaultValueSql("((1))");

                entity.Property(e => e.Trboll).HasColumnName("trboll");

                entity.HasOne(d => d.Analysis)
                    .WithMany(p => p.RAnalysis)
                    .HasForeignKey(d => d.AnalysisId);

                entity.HasOne(d => d.Interview)
                    .WithMany(p => p.RAnalysis)
                    .HasForeignKey(d => d.InterviewId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.RAnalysis)
                    .HasForeignKey(d => d.PatientId);

                entity.HasOne(d => d.Physician)
                    .WithMany(p => p.RAnalysis)
                    .HasForeignKey(d => d.PhysicianId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Region>(entity =>
            {
                entity.HasIndex(e => e.CityId);

                entity.Property(e => e.CityId).HasColumnName("cityId");

                entity.Property(e => e.RegName)
                    .IsRequired()
                    .HasColumnName("reg_name");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Region)
                    .HasForeignKey(d => d.CityId);
            });
        }
    }
}
