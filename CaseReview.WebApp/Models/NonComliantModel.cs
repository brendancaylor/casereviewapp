using CaseReview.DataLayer.Models;
using PagedList.Mvc;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.Mvc;

namespace CaseReview.WebApp.Models
{
    public class NonCompliantModel
    {
        public NonCompliantModel()
        {
            this.StandardLines = new Collection<SelectListItem>();
        }

        public PagedList.IPagedList<NonCompliant> NonCompliants;
        public ICollection<System.Web.Mvc.SelectListItem> StandardLines { get; set; }
        public string StandardLine { get; set; }
    }
}