using System;
using System.Collections.Generic;

namespace CaseReview.DataLayer.Models
{
    public partial class SectionCaseReviewType
    {
        public System.Guid ID { get; set; }
        public System.Guid SectionID { get; set; }
        public System.Guid CaseReviewTypeID { get; set; }
        public virtual CaseReviewType CaseReviewType { get; set; }
        public virtual Section Section { get; set; }
    }
}
