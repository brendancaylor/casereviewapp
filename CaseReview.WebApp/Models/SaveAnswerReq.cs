using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaseReview.WebApp.Models
{
    public class SaveAnswerReq
    {
        public System.Guid id { get; set; }
        public string comments{ get; set; }
        public bool? compliant { get; set; }

    }
}