﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaseReview.WebApp.Models
{
    public class PieChartModel
    {
        public PieChartModel()
        {
            Labels = new List<string>();
            Data = new List<int>();
        }
        public string Title { get; set; }
        public string CanvasId { get; set; }
        public List<string> Labels { get; set; }
        public List<int> Data { get; set; }
    }
    
    
}