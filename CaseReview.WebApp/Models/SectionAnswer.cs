using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CaseReview.WebApp.Models
{
    public partial class SectionAnswer
    {
        public SectionAnswer()
        {
            this.Answers = new List<Answer>();
        }

        public System.Guid ID { get; set; }
        public string SectionName { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }
    }
}