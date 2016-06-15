using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CaseReview.DataLayer.Models.Mapping
{
    public class vwNonCompliantMap : EntityTypeConfiguration<vwNonCompliant>
    {
        public vwNonCompliantMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ID});

            // Properties
            this.Property(t => t.ClientRef)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.SectionName)
                .IsRequired()
                .HasMaxLength(250);

            this.Property(t => t.QuestionName)
                .IsRequired();
            
            this.Property(t => t.StaffSurname)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.SectionOrder)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.QuestionOrder)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Comments)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("vwNonCompliant");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.ClientRef).HasColumnName("ClientRef");
            this.Property(t => t.Month).HasColumnName("Month");
            this.Property(t => t.Year).HasColumnName("Year");
            this.Property(t => t.SectionName).HasColumnName("SectionName");
            this.Property(t => t.QuestionName).HasColumnName("QuestionName");
            this.Property(t => t.StaffSurname).HasColumnName("StaffSurname");
            this.Property(t => t.SectionOrder).HasColumnName("SectionOrder");
            this.Property(t => t.QuestionOrder).HasColumnName("QuestionOrder");
            this.Property(t => t.Comments).HasColumnName("Comments");
        }
    }
}
