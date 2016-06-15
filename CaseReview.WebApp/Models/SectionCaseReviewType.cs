using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaseReview.WebApp.Models
{
    public class SectionCaseReviewType
    {
        public System.Guid SectionID { get; set; }
        public string SectionName { get; set; }
        public bool IsIncluded { get; set; }
    }
}