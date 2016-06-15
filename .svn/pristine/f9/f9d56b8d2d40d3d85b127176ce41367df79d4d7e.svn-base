using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CaseReview.DataLayer.Models.Mapping
{
    public class CaseReviewWorkSheetMap : EntityTypeConfiguration<CaseReviewWorkSheet>
    {
        public CaseReviewWorkSheetMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ClientRef)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Reviewer)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CaseReviewWorkSheet");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.StaffID).HasColumnName("StaffID");
            this.Property(t => t.CaseReviewTypeID).HasColumnName("CaseReviewTypeID");
            this.Property(t => t.ClientRef).HasColumnName("ClientRef");
            this.Property(t => t.Reviewer).HasColumnName("Reviewer");
            this.Property(t => t.ReviewedDate).HasColumnName("ReviewedDate");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.IsCompleted).HasColumnName("IsCompleted");

            // Relationships
            this.HasRequired(t => t.CaseReviewType)
                .WithMany(t => t.CaseReviewWorkSheets)
                .HasForeignKey(d => d.CaseReviewTypeID);
            this.HasRequired(t => t.Staff)
                .WithMany(t => t.CaseReviewWorkSheets)
                .HasForeignKey(d => d.StaffID);

        }
    }
}
