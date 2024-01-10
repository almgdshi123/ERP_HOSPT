using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ERP_HOSPT.Models;
using System.ComponentModel.DataAnnotations;

namespace ERP_HOSPT.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
        public DbSet<City> City { get; set; }
        public DbSet<Region> Region { get; set; }
        public DbSet<Qualify> Qualify { get; set; }
        public DbSet<Physician> Physician { get; set; }
        public DbSet<Depart> Depart { get; set; }
        public DbSet<Patient> Patient { get; set; }
        public DbSet<Analysis> Analysis { get; set; }
        public DbSet<Drug> Drug { get; set; }
        public DbSet<R_analysis> R_analysis { get; set; }
        public DbSet<interview> Interview { get; set; }
        public DbSet<Diagnosis> Diagnosis { get; set; }
        public DbSet<Prescribtion> Prescribtion { get; set; }
        public DbSet<Measure> Measure { get; set; }
        public DbSet<Company> Company { get; set; }

    }
    public class City
    {

        [Key]


        [Display(Name = " رقم المدينه")]
        public int cityId { get; set; }
        [Required]

        [Display(Name = " اسم المدينه")]
        public string cit_name { get; set; }
        public List<Region> Regions { get; set; }
        public List<Physician> Physicians { get; set; }//pk
        public List<Patient> Patients { get; set; }//pk
        public City()
        {
            this.cityId = 0;
            this.cit_name = "";

        }
    }
    public class Region
    {
        [Key]

        [Display(Name = " رقم المنطقة")]
        public int RegionId { get; set; }
        [Required]

        [Display(Name = " اسم المنطقة")]
        public string reg_name { get; set; }
        [Display(Name = " اسم المدينه")]
        public int cityId { get; set; }
        [Display(Name = " اسم المدينه")]
        public City city { get; set; }
        public List<Physician> Physicians { get; set; }//pk
        public List<Patient> Patients { get; set; }//pk
        public Region()
        {
            this.RegionId = 0;
            this.reg_name = "";

        }

    }

    public class Qualify
    {
        [Key]
        [Display(Name = " رقم المؤهل")]
        public int QualifyId { get; set; }
        [Display(Name = " اسم المؤهل")]
        public string q_name { get; set; }


        public List<Physician> Physicians { get; set; }//pk

        public Qualify()
        {
            this.QualifyId = 0;
            this.q_name = "";

        }

    }


    public class Physician
    {
        [Key]
        [Display(Name = " رقم الطبيب")]
        public int PhysicianId { get; set; }
        [Required]
        [Display(Name = "اسم الطبيب")]

        public string phy_name { get; set; }
       
        [Display(Name = "تاريخ الميلاد")]
        [DataType(DataType.Date)]
        public string phy_birth { get; set; }
       
        [Display(Name = "النوع")]
        public string phy_sex { get; set; }
        
        [Display(Name = "العنوان")]
        public string phy_addr { get; set; }
        [Display(Name = "رقم الجوال")]
        public string phy_phone { get; set; }
        [Display(Name = "البريد الاكتروني")]
        public string phy_emil { get; set; }
        [Display(Name = "اسم القسم")]
        public int Departno { get; set; }
        [Display(Name = " اسم المؤهل")]
        public int QualifyId { get; set; }
        [Display(Name = "اسم المدينه")]
        public int cityId { get; set; }
        [Display(Name = "اسم المنطقه")]
        public int RegionId { get; set; }
        [Display(Name = "اسم المدينه")]
        public City city { get; set; }
        [Display(Name = "اسم المؤهل")]
        public Qualify Qualify { get; set; }

        [Display(Name = "اسم القسم")]
        public Depart Depart { get; set; }
        [Display(Name = "اسم المنطقه")]
        public string CompanyId { get; set; }
       
        public Region Region { get; set; }

        [Display(Name = " رقم المستخدم")]
        public string userId { get; set; }
        public List<R_analysis> R_analysiss { get; set; }
        public List<interview> interviews { get; set; }//pk
        public List<Prescribtion> Prescribtions { get; set; }
        public ApplicationUser user { get; set; }
        public Company Company { get; set; }

        public Physician()
        {
            this.PhysicianId = 0;
            this.phy_name = "";
            this.phy_birth = "";
            this.phy_sex = "";
            this.phy_addr = "";
            this.phy_phone = "";
            this.phy_emil = "";



        }

    }
    public class Depart
    {
        [Key]

        public int Departno { get; set; }
        [Display(Name = " اسم القسم")]
        public string dept_name { get; set; }

        public List<Physician> Physicians { get; set; }//pk
        public Depart()
        {
            this.Departno = 0;
            this.dept_name = "";
        }
    }
    public class Patient
    {
        [Key]
        public int PatientId { get; set; }
        [Required]

        [Display(Name = "اسم المريض")]
        public string Patientname { get; set; }
       
        [Display(Name = "الرقم الوطني")]
        public string pa_nat { get; set; }
        

        [Display(Name = "العنوان")]
        public string pa_addr { get; set; }
       

        [Display(Name = "المهنة")]
        public string pa_job { get; set; }
       

        [Display(Name = "تاريخ الميلاد")]
        [DataType(DataType.Date)]
        public string pa_data { get; set; }
       

        [Display(Name = " النوع")]
        public string pa_sex { get; set; }
        

        [Display(Name = " الموبايل")]
        public string pa_mobile { get; set; }
        

        [Display(Name = " الهاتف")]
        public string pa_phone { get; set; }
        //[Required]

        [Display(Name = " البريد الاكتروني")]
        public string pa_email { get; set; }
       
        [Display(Name = " ملاحظه")]
        public string pa_note { get; set; }
        [Display(Name = " اسم المدينه")]
        public int cityId { get; set; }
        [Display(Name = " اسم المنطقه")]
        public int RegionId { get; set; }
        [Display(Name = " اسم المدينه")]
        public City city { get; set; }
        public string CompanyId { get; set; }
        [Display(Name = " اسم المنطقه")]
        public Region Region { get; set; }
        public List<R_analysis> R_analysiss { get; set; }
        public List<interview> interviews { get; set; }//pk
        public List<Prescribtion> Prescribtions { get; set; }
       
        public Company Company { get; set; }
        public Patient()
        {
            this.PatientId = 0;
            this.Patientname = "";
            this.pa_nat = "";
            this.pa_addr = "";
            this.pa_job = "";
            this.pa_data = "";
            this.pa_sex = "";
            this.pa_mobile = "";
            this.pa_phone = "";
            this.pa_email = "";
            this.pa_note = "";
        }


    }
    public class Analysis
    {
        [Key]
        [Display(Name = " اسم التحليل")]
        public int AnalysisId { get; set; }
        [Display(Name = " اسم التحليل")]
        public string a_name { get; set; }
        [Display(Name = " التكلفه")]
        public string a_Pric { get; set; }
       
        public List<R_analysis> R_analysiss { get; set; }

        public Analysis()
        {
            this.AnalysisId = 0;
            this.a_name = "";
            this.a_Pric = "";
        
        }
    }
   

    public class Drug
    {
        [Key]
        public int DrugId { get; set; }
        [Display(Name = " اسم الدواء")]
        public string d_name { get; set; }
        public string d_pric { get; set; }

        public List<Prescribtion> Prescribtions { get; set; }
        public Drug()
        {
            this.DrugId = 0;
            this.d_name = "";
            this.d_pric = "";


        }
    }


    public class Company
    {
        [Key]
        public string CompanyId { get; set; }
       
        public string Compan_name { get; set; }
        public int State { get; set; }

        public List<ApplicationUser> ApplicationUser { get; set; }
        public List<Patient> Patient { get; set; }
        public List<interview> interview { get; set; }
        public List<Physician> Physician { get; set; }
        public Company()
        {
            this.CompanyId = "";
            this.Compan_name = "";
            this.State = 0;


        }
    }



    public class Measure
    {
        [Key]
        public int MeasureId { get; set; }
        public string Measure_name { get; set; }



        public Measure()
        {
            this.MeasureId = 0;
            this.Measure_name = "";


        }
    }

    public class R_analysis
    {
        [Key]
        public int R_analysisId { get; set; }
        [Display(Name = " اسم المريض")]
        public int PatientId { get; set; }
        [Display(Name = "  اسم التحليل")]
        public int AnalysisId { get; set; }
        [Display(Name = " اسم الطبيب")]
        public int PhysicianId { get; set; }
        [Display(Name = " اسم المقابله")]
        public int interviewId { get; set; }

        [Display(Name = " تاريخ التحليل")]
        public string r_date { get; set; }
        [Display(Name = " نتيجه التحليل")]
        public string r_result { get; set; }
        [Display(Name = "ملاحظه")]

        public string r_describe { get; set; }

        public bool trboll { get; set; }


        public bool State1 { get; set; }

        public Patient patient { get; set; }
        public Analysis analysis { get; set; }
        public Physician physician { get; set; }
        public interview interviews { get; set; }


        public R_analysis()
        {
            this.R_analysisId = 0;
            this.r_date = "";
            this.r_result = "";
            this.r_describe = "";


        }
    }

    public class interview
    {
        [Key]
        [Display(Name = " رقم المقابله")]
        public int interviewId { get; set; }
        [Display(Name = " نوع المقابله")]
        public string inter_type { get; set; }

        [Display(Name = " تاريخ المقابله")]
        [DataType(DataType.Date)]
        public string inter_date { get; set; }
        [Display(Name = "  ملاحظه")]
        public string inter_notes { get; set; }
      
        public bool State { get; set; }
        public bool Stateinterview { get; set; }
        [Display(Name = " اسم المستخدم")]
        public string userId { get; set; }
        [Display(Name = " اسم المريض")]
        public int PatientId { get; set; }
        [Display(Name = " اسم الطبيب")]
        public int PhysicianId { get; set; }
        [Display(Name = " اسم المريض")]
        public Patient patient { get; set; }
        [Display(Name = " اسم الطبيب")]
        public Physician physician { get; set; }
        [Display(Name = " اسم المستخدم")]
        public ApplicationUser user { get; set; }
        public string CompanyId { get; set; }


        public Company Company { get; set; }


        public interview()
        {
            this.interviewId = 0;
            this.inter_type = "";
            this.inter_date = "";
            this.inter_notes = "";


        }
    }



    public class Diagnosis
    {
        [Key]
        [Display(Name = " رقم الوصفه")]
        public int DiagnosisId { get; set; }
        [Display(Name = " التشخيص")]
        public string Dig { get; set; }
        [Display(Name = " اسم الطبيب")]
        public int PhysicianId { get; set; }
        [Display(Name = " اسم المريض")]
        public int PatientId { get; set; }

        [Display(Name = " تاريخ الوصفه")]
        public string Diagnosis_date { get; set; }
       
        [Display(Name = " اسم الدواء")]
        public string Drug { get; set; }
        [Display(Name = " الجرعه")]
        public string Drug_detail { get; set; }
        [Display(Name = " رقم المقابله")]
        public int interviewId { get; set; }
        public string CompanyId { get; set; }

        [Display(Name = " اسم المريض")]
        public Patient patient { get; set; }
       
       
        [Display(Name = " اسم الطبيب")]
        public Physician physician { get; set; }
       
        public interview interviews { get; set; }

        public Diagnosis()
        {
            this.DiagnosisId = 0;
           
            this.Dig = "";
            this.Diagnosis_date = "";
            this.Drug = "";
            this.Drug_detail = "";

        }
    }








    public class Prescribtion
    {
        [Key]
        [Display(Name = " رقم الوصفه")]
        public int PrescribtionId { get; set; }
        [Display(Name = " اسم الطبيب")]
        public int PhysicianId { get; set; }
        [Display(Name = " اسم المريض")]
        public int PatientId { get; set; }

        [Display(Name = " تاريخ الوصفه")]
        public string Pre_date { get; set; }
        [Display(Name = " التشخيص")]
        public string Dig { get; set; }
        [Display(Name = " اسم الدواء")]
        public int DrugId { get; set; }
        [Display(Name = " الجرعه")]
        public string pre_detail { get; set; }


        [Display(Name = " اسم المريض")]
        public Patient patient { get; set; }
        [Display(Name = " اسم الدواء")]
        public Drug drug { get; set; }
        [Display(Name = " اسم الطبيب")]
        public Physician physician { get; set; }


        public Prescribtion()
        {
            this.PrescribtionId = 0;
            this.Pre_date = "";
            this.Dig = "";
            this.pre_detail = "";


        }
    }





}
