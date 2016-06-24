using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseReview.BusinessObjects
{
    public class ReportStaffSections
    {
        public ReportStaffSections()
        {
            ReportItems = new List<ReportItem>();
        }
        public List<ReportItem> ReportItems { get; set; }
    }

    public class ReportItem
    {
        public ReportItem()
        {
            PieChart = new ChartModel();
            LineChartNumberAnswered = new ChartModel();
            LineChartNumberNo = new ChartModel();

        }
        public string Title { get; set; }
        public ChartModel PieChart { get; set; }
        public ChartModel LineChartNumberNo { get; set; }
        public ChartModel LineChartNumberAnswered { get; set; }
    }
    
    public class ChartModel
    {
        public ChartModel()
        {
            DataItems = new List<DataItem>();
        }
        public List<DataItem> DataItems { get; set; }
    }

    public class DataItem
    {
        public int Order { get; set; }
        public string Label { get; set; }
        public int Data { get; set; }
    }
}
