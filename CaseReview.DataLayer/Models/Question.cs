using System;
using System.Collections.Generic;

namespace CaseReview.DataLayer.Models
{
    public partial class Question
    {
        public Question()
        {
            this.Answers = new List<Answer>();
        }

        public System.Guid ID { get; set; }
        public System.Guid SectionID { get; set; }
        public string QuestionName { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
        public virtual Section Section { get; set; }
    }
}
