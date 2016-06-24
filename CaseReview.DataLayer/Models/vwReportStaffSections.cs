using System;
using System.Collections.Generic;

namespace CaseReview.DataLayer.Models
{
    public partial class vwReportStaffSection
    {
        public System.Guid ID { get; set; }
        public int ReviewedDateMonth { get; set; }
        public int ReviewedDateYear { get; set; }
        public int ReviewedDateInt { get; set; }
        public int SumCompliant { get; set; }
        public int SumNonCompliant { get; set; }
        public System.Guid SectionID { get; set; }
        public string SectionName { get; set; }
        public System.Guid StaffID { get; set; }
        public string StaffFirstname { get; set; }
        public string StaffSurname { get; set; }
    }
}
