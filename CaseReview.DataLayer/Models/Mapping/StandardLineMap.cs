using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CaseReview.DataLayer.Models.Mapping
{
    public class StandardLineMap : EntityTypeConfiguration<StandardLine>
    {
        public StandardLineMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Line)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("StandardLine");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Line).HasColumnName("Line");
            this.Property(t => t.LineType).HasColumnName("LineType");
        }
    }
}
