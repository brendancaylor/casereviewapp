using System;
using System.Collections.Generic;

namespace CaseReview.DataLayer.Models
{
    public partial class CaseReviewType
    {
        public CaseReviewType()
        {
            this.CaseReviewWorkSheets = new List<CaseReviewWorkSheet>();
            this.SectionCaseReviewTypes = new List<SectionCaseReviewType>();
        }

        public int ID { get; set; }
        public string TypeName { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<CaseReviewWorkSheet> CaseReviewWorkSheets { get; set; }
        public virtual ICollection<SectionCaseReviewType> SectionCaseReviewTypes { get; set; }
    }
}
