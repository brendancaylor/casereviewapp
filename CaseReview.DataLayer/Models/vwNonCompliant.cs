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
        public int SectionOrder { get; set; }

        public Guid StaffID { get; set; }
        public string StaffSurname { get; set; }
        
        public Guid QuestionID { get; set; }
        public string QuestionName { get; set; }
        public int QuestionOrder { get; set; }

        public Nullable<bool> IsMandatory { get; set; }
        public Nullable<int> Risk { get; set; }

        public string Comments { get; set; }
        public string Feedback { get; set; }
        public string FeedbackType { get; set; }
        public bool HasFeedback { get; set; }
        
    }
}
