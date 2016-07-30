using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CaseReview.DataLayer.Models.Mapping
{
    public class vwStaffNonCompliantTotalMap : EntityTypeConfiguration<vwStaffNonCompliantTotal>
    {
        public vwStaffNonCompliantTotalMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ID});

            // Properties
            this.Property(t => t.FeedbackType)
                .HasMaxLength(50);

            this.Property(t => t.StaffFirstname)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.StaffSurname)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.SectionName)
                .IsRequired()
                .HasMaxLength(250);

            // Table & Column Mappings
            this.ToTable("vwStaffNonCompliantTotals");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.SumNonCompliant).HasColumnName("SumNonCompliant");
            this.Property(t => t.StaffID).HasColumnName("StaffID");
            this.Property(t => t.FeedbackType).HasColumnName("FeedbackType");
            this.Property(t => t.StaffFirstname).HasColumnName("StaffFirstname");
            this.Property(t => t.StaffSurname).HasColumnName("StaffSurname");
            this.Property(t => t.SectionName).HasColumnName("SectionName");
            this.Property(t => t.SectionID).HasColumnName("SectionID");
            this.Property(t => t.QuestionID).HasColumnName("QuestionID");
            this.Property(t => t.IsMandatory).HasColumnName("IsMandatory");
        }
    }
}
