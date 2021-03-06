﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CaseReview.DataLayer.Models.Mapping
{
    public class CaseReviewTypeMap : EntityTypeConfiguration<CaseReviewType>
    {
        public CaseReviewTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.TypeName)
                .IsRequired()
                .HasMaxLength(250);

            // Table & Column Mappings
            this.ToTable("CaseReviewType");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.TypeName).HasColumnName("TypeName");
            this.Property(t => t.DisplayOrder).HasColumnName("DisplayOrder");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
        }
    }
}
