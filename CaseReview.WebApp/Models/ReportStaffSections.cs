using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaseReview.WebApp.Models
{
    public class ReportStaffSections
    {
        public ReportStaffSections()
        {
            ReportItems = new List<ReportItem>();
            staff = new List<ListItem>();
            sections = new List<ListItem>();

            staffIds = new List<Guid>();
            sectionIds = new List<Guid>();

            from = new DateTime(DateTime.Now.AddMonths(-6).Year, DateTime.Now.AddMonths(-6).Month, 1);
            to = DateTime.Now;
            isDisplayByStaffFirst = true;
            allStaff = false;
            allSections = false;

            isStaffCombined = true;
            isSectionsCombined = true;

        }
        public List<ReportItem> ReportItems { get; set; }

        public DateTime from { get; set; }
        public DateTime to { get; set; }
        public bool isDisplayByStaffFirst { get; set; }

        public bool allStaff { get; set; }
        public bool allSections { get; set; }

        public bool isStaffCombined { get; set; }
        public bool isSectionsCombined { get; set; }

        public List<Guid> staffIds { get; set; }
        public List<Guid> sectionIds { get; set; }
        public List<ListItem> staff { get; set; }
        public List<ListItem> sections { get; set; }

        public string ReportItemTitle { get; set; }

    }

    public class ListItem
    {
        public Guid value { get; set; }
        public string text { get; set; }
    }

    public class ReportItem
    {
        public string ReportItemTitle { get; set; }
        public PieChartModel PieChart { get; set; }
        public LineChartModel LineChartPercNo { get; set; }
        public LineChartModel LineChartNumberAnswered { get; set; }
    }
    
}