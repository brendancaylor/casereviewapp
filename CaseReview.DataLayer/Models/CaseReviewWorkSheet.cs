using System;
using System.Collections.Generic;

namespace CaseReview.DataLayer.Models
{
    public partial class CaseReviewWorkSheet
    {
        public CaseReviewWorkSheet()
        {
            this.Answers = new List<Answer>();
        }

        public System.Guid ID { get; set; }
        public System.Guid StaffID { get; set; }
        public int CaseReviewTypeID { get; set; }
        public string ClientRef { get; set; }
        public string Reviewer { get; set; }
        public System.DateTime ReviewedDate { get; set; }
        public int Type { get; set; }
        public bool IsCompleted { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
        public virtual CaseReviewType CaseReviewType { get; set; }
        public virtual Staff Staff { get; set; }
    }
}
