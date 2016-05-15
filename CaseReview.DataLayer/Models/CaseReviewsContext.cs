using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using CaseReview.DataLayer.Models.Mapping;

namespace CaseReview.DataLayer.Models
{
    public partial class CaseReviewsContext : DbContext
    {
        static CaseReviewsContext()
        {
            Database.SetInitializer<CaseReviewsContext>(null);
        }

        public CaseReviewsContext()
            : base("Name=CaseReviewsContext")
        {
        }

        public DbSet<Answer> Answers { get; set; }
        public DbSet<AspNetRole> AspNetRoles { get; set; }
        public DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public DbSet<AspNetUser> AspNetUsers { get; set; }
        public DbSet<CaseReviewWorkSheet> CaseReviewWorkSheets { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<StandardLine> StandardLines { get; set; }
        public DbSet<vwNonCompliant> vwNonCompliants { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AnswerMap());
            modelBuilder.Configurations.Add(new AspNetRoleMap());
            modelBuilder.Configurations.Add(new AspNetUserClaimMap());
            modelBuilder.Configurations.Add(new AspNetUserLoginMap());
            modelBuilder.Configurations.Add(new AspNetUserMap());
            modelBuilder.Configurations.Add(new CaseReviewWorkSheetMap());
            modelBuilder.Configurations.Add(new QuestionMap());
            modelBuilder.Configurations.Add(new SectionMap());
            modelBuilder.Configurations.Add(new StaffMap());
            modelBuilder.Configurations.Add(new StandardLineMap());
            modelBuilder.Configurations.Add(new vwNonCompliantMap());
        }
    }
}
