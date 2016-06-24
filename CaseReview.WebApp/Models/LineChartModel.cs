using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaseReview.WebApp.Models
{
    public class LineChartModel
    {
        public LineChartModel()
        {
            Labels = new List<string>();
            Data = new List<int>();
        }
        public string label { get; set; }
        public string fillColor { get; set; }
        public string strokeColor { get; set; }
        public string pointColor { get; set; }
        public string pointStrokeColor { get; set; }
        public string pointHighlightFill { get; set; }
        public string pointHighlightStroke { get; set; }

        public string CanvasId { get; set; }
        public List<string> Labels { get; set; }
        public List<int> Data { get; set; }        

    }


}