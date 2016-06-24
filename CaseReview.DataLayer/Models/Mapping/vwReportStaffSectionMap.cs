using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CaseReview.DataLayer.Models.Mapping
{
    public class vwReportStaffSectionMap : EntityTypeConfiguration<vwReportStaffSection>
    {
        public vwReportStaffSectionMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ID});

            // Properties
            this.Property(t => t.SectionName)
                .IsRequired()
                .HasMaxLength(250);

            this.Property(t => t.StaffFirstname)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.StaffSurname)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("vwReportStaffSections");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.ReviewedDateMonth).HasColumnName("ReviewedDateMonth");
            this.Property(t => t.ReviewedDateYear).HasColumnName("ReviewedDateYear");
            this.Property(t => t.ReviewedDateInt).HasColumnName("ReviewedDateInt");
            this.Property(t => t.SumCompliant).HasColumnName("SumCompliant");
            this.Property(t => t.SumNonCompliant).HasColumnName("SumNonCompliant");
            this.Property(t => t.SectionID).HasColumnName("SectionID");
            this.Property(t => t.SectionName).HasColumnName("SectionName");
            this.Property(t => t.StaffID).HasColumnName("StaffID");
            this.Property(t => t.StaffFirstname).HasColumnName("StaffFirstname");
            this.Property(t => t.StaffSurname).HasColumnName("StaffSurname");
        }
    }
}
