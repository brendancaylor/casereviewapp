using System;
using System.Collections.Generic;

namespace CaseReview.WebApp.Models
{
    public partial class Answer
    {
        public System.Guid ID { get; set; }
        public System.Guid CaseReviewWorkSheetID { get; set; }
        public System.Guid QuestionID { get; set; }
        public string Comments { get; set; }
        public Nullable<bool> Compliant { get; set; }
        //public virtual CaseReviewWorkSheet CaseReviewWorkSheet { get; set; }
        public virtual Question Question { get; set; }
        public bool Advisory { get; set; }
    }
}
