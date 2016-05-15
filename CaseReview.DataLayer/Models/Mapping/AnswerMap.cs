using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CaseReview.DataLayer.Models.Mapping
{
    public class AnswerMap : EntityTypeConfiguration<Answer>
    {
        public AnswerMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.Comments)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Answer");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.CaseReviewWorkSheetID).HasColumnName("CaseReviewWorkSheetID");
            this.Property(t => t.QuestionID).HasColumnName("QuestionID");
            this.Property(t => t.Comments).HasColumnName("Comments");
            this.Property(t => t.Compliant).HasColumnName("Compliant");

            // Relationships
            this.HasRequired(t => t.CaseReviewWorkSheet)
                .WithMany(t => t.Answers)
                .HasForeignKey(d => d.CaseReviewWorkSheetID);
            this.HasRequired(t => t.Question)
                .WithMany(t => t.Answers)
                .HasForeignKey(d => d.QuestionID);

        }
    }
}
