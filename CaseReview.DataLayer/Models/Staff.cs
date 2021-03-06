using System;
using System.Collections.Generic;

namespace CaseReview.DataLayer.Models
{
    public partial class Staff
    {
        public Staff()
        {
            this.CaseReviewWorkSheets = new List<CaseReviewWorkSheet>();
        }

        public System.Guid ID { get; set; }
        public string StaffFirstname { get; set; }
        public string StaffSurname { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<CaseReviewWorkSheet> CaseReviewWorkSheets { get; set; }
    }
}
