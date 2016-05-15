using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CaseReview.WebApp.Models
{
    public partial class CaseReviewWorkSheet
    {
        public CaseReviewWorkSheet()
        {
            this.Answers = new List<Answer>();
            this.StaffMembers = new List<SelectListItem>();
            this.StandardLines = new Collection<SelectListItem>();
            this.Types = new List<SelectListItem>();
            ReviewedDate = DateTime.Now;
            Reviewer = "Karen Jones";
        }

        public System.Guid ID { get; set; }
        public System.Guid StaffID { get; set; }
        public System.Guid StandardID { get; set; }

        [Required(ErrorMessage = "Client Ref is required.")]
        public string ClientRef { get; set; }

        [Required(ErrorMessage = "Reviewer is required.")]
        public string Reviewer { get; set; }

        public System.DateTime ReviewedDate { get; set; }
        public int Type { get; set; }
        public bool IsCompleted { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }
        public virtual Staff Staff { get; set; }

        public virtual ICollection<System.Web.Mvc.SelectListItem> StaffMembers { get; set; }
        public virtual ICollection<System.Web.Mvc.SelectListItem> StandardLines { get; set; }
        public virtual ICollection<System.Web.Mvc.SelectListItem> Types { get; set; }
    }
}
