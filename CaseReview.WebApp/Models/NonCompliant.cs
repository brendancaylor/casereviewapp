﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaseReview.WebApp.Models
{
    public class NonCompliant
    {
        public string ClientRef { get; set; }
        public int Count { get; set; }
        public Nullable<int> Month { get; set; }
        public Nullable<int> Year { get; set; }
        public string SectionName { get; set; }
        public string QuestionName { get; set; }
        public string StaffSurname { get; set; }
        public int SectionOrder { get; set; }
        public int QuestionOrder { get; set; }
    }
}