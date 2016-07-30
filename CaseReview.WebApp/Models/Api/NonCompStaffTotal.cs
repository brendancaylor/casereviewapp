using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaseReview.WebApp.Models.Api
{
    public class NonCompStaffTotal
    {
        public int Total { get; set; }
        public bool IsMandatory { get; set; }
        public bool IsThisQuestion { get; set; }
        public string FeedbackType { get; set; }
    }
}