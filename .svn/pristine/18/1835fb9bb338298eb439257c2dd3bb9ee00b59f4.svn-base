using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using CaseReview.BusinessLogic;
using CaseReview.WebApp.Models;

namespace CaseReview.WebApp.Controllers
{
    public class CaseReviewTypesController : Controller
    {
        private GenericLogic<DataLayer.Models.CaseReviewType>  logic = new GenericLogic<DataLayer.Models.CaseReviewType>();
        // GET: CaseReviewTypes
        public ActionResult Index()
        {
            var data = logic.GetAll();
            var model = data.OrderBy(o => o.DisplayOrder).ToList().Select(x => new Models.CaseReviewTypeModel
            {
                DisplayOrder = x.DisplayOrder,
                ID = x.ID,
                IsActive = x.IsActive,
                TypeName = x.TypeName
            });
            //new <Models.CaseReviewTypeModel>();

            var jsonSerializer = new JavaScriptSerializer();
            var json = jsonSerializer.Serialize(model);
            ViewBag.Json = json;
            return View();
        }
    }
}