using System;
using System.Collections.Generic;

namespace CaseReview.DataLayer.Models
{
    public partial class Section
    {
        public Section()
        {
            this.Questions = new List<Question>();
        }

        public System.Guid ID { get; set; }
        public string SectionName { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
    }
}
