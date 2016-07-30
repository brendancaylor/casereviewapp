using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaseReview.WebApp.Models
{
    public class SaveAnswerReq
    {
        public System.Guid id { get; set; }
        public string comments { get; set; }
        public bool? compliant { get; set; }

    }

    public class SaveAnswersReq
    {
        public List<System.Guid> ids { get; set; }
        public string comments { get; set; }
        public bool? compliant { get; set; }

    }

    public class GetNcTotalsReq
    {
        public Guid StaffID { get; set; }
        public Guid QuestionID { get; set; }
    }

    public class SaveFbReq
    {
        public Guid ID { get; set; }
        public string Feedback { get; set; }
        public string FeedbackType { get; set; }
    }

}