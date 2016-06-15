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
    [Authorize(Roles = "Admin")]
    public class AdminListController : Controller
    {
        private GenericLogic<DataLayer.Models.CaseReviewType> crtLogic = new GenericLogic<DataLayer.Models.CaseReviewType>();
        private GenericLogic<DataLayer.Models.Section> secLogic = new GenericLogic<DataLayer.Models.Section>();
        private GenericLogic<DataLayer.Models.SectionCaseReviewType> scrtLogic = new GenericLogic<DataLayer.Models.SectionCaseReviewType>();
        private GeneralLogic genLogic = new GeneralLogic();

        // GET: CaseReviewTypes

        public ActionResult CaseReviewTypes()
        {
            var data = crtLogic.GetAll();
            var model = data.OrderBy(o => o.DisplayOrder).ToList().Select(x => new Models.BaseModel
            {
                DisplayOrder = x.DisplayOrder,
                ID = x.ID,
                IsActive = x.IsActive,
                Name = x.TypeName
            });


            var jsonSerializer = new JavaScriptSerializer();
            var json = jsonSerializer.Serialize(model);
            ViewBag.Json = json;
            string rootUrl = Url.Content("~/");

            ViewBag.UrlApiAdd = rootUrl + "api/adminlist/addCaseType";
            ViewBag.UrlApiUpdate = rootUrl + "api/adminlist/updateCaseType";
            ViewBag.UrlApiReorder = rootUrl + "api/adminlist/reorderCaseTypes";
            ViewBag.Title = "Case Review Types";
            return View("Index");
        }

        public ActionResult Sections()
        {
            ViewBag.Title = "Sections";
            var data = secLogic.GetAll();
            var model = data.OrderBy(o => o.DisplayOrder).ToList().Select(x => new Models.BaseModel
            {
                DisplayOrder = x.DisplayOrder,
                ID = x.ID,
                IsActive = x.IsActive,
                Name = x.SectionName
            });


            var jsonSerializer = new JavaScriptSerializer();
            var json = jsonSerializer.Serialize(model);
            ViewBag.Json = json;
            string rootUrl = Url.Content("~/");

            ViewBag.UrlApiAdd = rootUrl + "api/adminlist/addSection";
            ViewBag.UrlApiUpdate = rootUrl + "api/adminlist/updateSection";
            ViewBag.UrlApiReorder = rootUrl + "api/adminlist/reorderSections";

            return View("Index");
        }

        public ActionResult Questions()
        {
            var data = genLogic.GetAllSectionsAndQuestions();
            var model = data.OrderBy(o => o.DisplayOrder).ToList().Select(x => new Models.Section
            {
                ID = x.ID,
                DisplayOrder = x.DisplayOrder,
                SectionName = x.SectionName,
                IsActive = x.IsActive,
                Questions = x.Questions.OrderBy(o => o.DisplayOrder).ToList().Select(
                    q => new Models.Question
                    {
                        ID = q.ID,
                        SectionID = q.SectionID,
                        DisplayOrder = q.DisplayOrder,
                        IsActive = q.IsActive,
                        IsMandatory = q.IsMandatory,
                        Risk = q.Risk,
                        QuestionName = q.QuestionName
                    }
                    ).ToList()
            });
            
            var jsonSerializer = new JavaScriptSerializer();
            var json = jsonSerializer.Serialize(model);
            ViewBag.Json = json;
            string rootUrl = Url.Content("~/");
            ViewBag.UrlApiAdd = rootUrl + "api/adminlist/addQuestion";
            ViewBag.UrlApiUpdate = rootUrl + "api/adminlist/updateQuestion";
            ViewBag.UrlApiReorder = rootUrl + "api/adminlist/reorderQuestions";

            return View("Questions");
        }

        public ActionResult CaseReviewTypeSections()
        {
            var crData = crtLogic.GetAll();
            var secData = secLogic.GetAll();
            var scrtData = scrtLogic.GetAll();

            var crModels = crData.OrderBy(o => o.DisplayOrder).ToList().Select(x => new Models.CaseReviewType
            {
                DisplayOrder = x.DisplayOrder,
                CaseReviewTypeID = x.ID,
                IsActive = x.IsActive,
                TypeName = x.TypeName
            }).ToList();

            foreach (var cr in crModels)
            {
                cr.DisplayOrder = cr.DisplayOrder + 10;
                foreach (var sc in secData.OrderBy(o => o.DisplayOrder))
                {
                    var scrt = new SectionCaseReviewType()
                    {
                        SectionID = sc.ID,
                        SectionName = sc.SectionName
                    };
                    if (scrtData.FirstOrDefault(
                        o => o.CaseReviewTypeID == cr.CaseReviewTypeID
                        && o.SectionID == sc.ID
                        ) != null)
                    {
                        scrt.IsIncluded = true;
                    }
                    cr.SectionCaseReviewTypes.Add(scrt);
                }
            }

            var jsonSerializer = new JavaScriptSerializer();
            var json = jsonSerializer.Serialize(crModels);
            ViewBag.Json = json;
            string rootUrl = Url.Content("~/");
            ViewBag.UrlApiUpdateCaseReviewTypeSection = rootUrl + "api/adminlist/updateCaseReviewTypeSection";
            return View("CaseReviewTypeSections");
        }

    }
}