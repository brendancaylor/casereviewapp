using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CaseReview.BusinessLogic;

namespace CaseReview.WebApp.Controllers
{
    public class DataExtractController : Controller
    {
        // GET: DataExtract
        public ActionResult Index()
        {
            DataExtractLogic logic = new DataExtractLogic();
            return File(logic.DataExtractAll(), "application/zip", "ReportExport.zip");
            
        }
    }
}