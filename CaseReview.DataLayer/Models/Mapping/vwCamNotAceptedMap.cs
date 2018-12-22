using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CaseReview.DataLayer.Models.Mapping
{
    public class vwCamNotAceptedMap : EntityTypeConfiguration<vwCamNotAcepted>
    {
        public vwCamNotAceptedMap()
        {
            // Primary Key
            this.HasKey(t => t.Email);

            // Properties
            this.Property(t => t.Email)
                .IsRequired()
                .HasMaxLength(256);

            // Table & Column Mappings
            this.ToTable("vwCamNotAcepted");
            this.Property(t => t.CountNotAccepted).HasColumnName("CountNotAccepted");
            this.Property(t => t.Email).HasColumnName("Email");
        }
    }
}
