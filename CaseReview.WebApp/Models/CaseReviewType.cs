using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaseReview.WebApp.Models
{
    public class CaseReviewType
    {
        public CaseReviewType()
        {
            this.SectionCaseReviewTypes = new List<SectionCaseReviewType>();
        }

        public Guid CaseReviewTypeID { get; set; }
        public string TypeName { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<SectionCaseReviewType> SectionCaseReviewTypes { get; set; }
    }
}