using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CaseReview.DataLayer.Models.Mapping
{
    public class StaffMap : EntityTypeConfiguration<Staff>
    {
        public StaffMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.StaffFirstname)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.StaffSurname)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Email)
                .IsRequired()
                .HasMaxLength(256);

            // Table & Column Mappings
            this.ToTable("Staff");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.StaffFirstname).HasColumnName("StaffFirstname");
            this.Property(t => t.StaffSurname).HasColumnName("StaffSurname");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
        }
    }
}
