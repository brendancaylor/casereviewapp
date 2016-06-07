using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaseReview.WebApp.Models
{
    public class CaseReviewTypeModel
    {

        public CaseReviewTypeModel()
        {
        }
        public int ID { get; set; }
        public string TypeName { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
    }
}