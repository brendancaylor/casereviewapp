using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CaseReview.DataLayer.Models.Mapping
{
    public class QuestionMap : EntityTypeConfiguration<Question>
    {
        public QuestionMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.QuestionName)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Question");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.SectionID).HasColumnName("SectionID");
            this.Property(t => t.QuestionName).HasColumnName("QuestionName");
            this.Property(t => t.DisplayOrder).HasColumnName("DisplayOrder");
            this.Property(t => t.IsActive).HasColumnName("IsActive");

            // Relationships
            this.HasRequired(t => t.Section)
                .WithMany(t => t.Questions)
                .HasForeignKey(d => d.SectionID);

        }
    }
}
