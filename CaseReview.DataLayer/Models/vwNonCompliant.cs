using System;
using System.Collections.Generic;

namespace CaseReview.DataLayer.Models
{
    public partial class vwNonCompliant
    {
        public Nullable<System.Guid> ID { get; set; }
        public string ClientRef { get; set; }
        public Nullable<int> Month { get; set; }
        public Nullable<int> Year { get; set; }
        public string SectionName { get; set; }
        public string QuestionName { get; set; }
        public string StaffSurname { get; set; }
        public int SectionOrder { get; set; }
        public int QuestionOrder { get; set; }
    }
}
