using System;
using System.Collections.Generic;

namespace CaseReview.DataLayer.Models
{
    public partial class vwStaffNonCompliantTotal
    {
        public Nullable<System.Guid> ID { get; set; }
        public Nullable<int> SumNonCompliant { get; set; }
        public System.Guid StaffID { get; set; }
        public string FeedbackType { get; set; }
        public string StaffFirstname { get; set; }
        public string StaffSurname { get; set; }
        public string SectionName { get; set; }
        public System.Guid SectionID { get; set; }
        public System.Guid QuestionID { get; set; }
        public Nullable<bool> IsMandatory { get; set; }
    }
}
