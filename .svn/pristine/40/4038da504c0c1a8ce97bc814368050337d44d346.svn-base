using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.Mvc;

namespace CaseReview.WebApp.Models
{
    public partial class Question
    {
        public Question()
        {
            this.Answers = new List<Answer>();
            this.Sections = new Collection<SelectListItem>();
        }

        public System.Guid ID { get; set; }
        public System.Guid SectionID { get; set; }
        public string QuestionName { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
        public virtual Section Section { get; set; }

        public virtual ICollection<System.Web.Mvc.SelectListItem> Sections { get; set; }
    }
}
