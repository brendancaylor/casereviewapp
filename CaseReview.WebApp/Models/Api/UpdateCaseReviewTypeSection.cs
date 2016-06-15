using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaseReview.WebApp.Models.Api
{
    public class UpdateCaseReviewTypeSection
    {
        public Guid CaseReviewTypeID { get; set; }
        public Guid SectionID { get; set; }
        public bool IsIncluded { get; set; }
    }
}