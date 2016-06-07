using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace CaseReview.DataLayer.Models.Mapping
{
    public class SectionCaseReviewTypeMap : EntityTypeConfiguration<SectionCaseReviewType>
    {
        public SectionCaseReviewTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            // Table & Column Mappings
            this.ToTable("SectionCaseReviewType");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.SectionID).HasColumnName("SectionID");
            this.Property(t => t.CaseReviewTypeID).HasColumnName("CaseReviewTypeID");

            // Relationships
            this.HasRequired(t => t.CaseReviewType)
                .WithMany(t => t.SectionCaseReviewTypes)
                .HasForeignKey(d => d.CaseReviewTypeID);
            this.HasRequired(t => t.Section)
                .WithMany(t => t.SectionCaseReviewTypes)
                .HasForeignKey(d => d.SectionID);

        }
    }
}
