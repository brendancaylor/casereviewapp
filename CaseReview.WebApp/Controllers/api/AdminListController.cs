using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using CaseReview.WebApp.Models;
using CaseReview.BusinessLogic;
using CaseReview.DataLayer.Models;

namespace CaseReview.WebApp.Controllers.api
{
    public class AdminListController : ApiController
    {
        private JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();

        
        [System.Web.Http.HttpPost]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Authorize]
        public HttpResponseMessage updateCaseType(BaseModel model)
        {
            var genLogic = new GeneralLogic();
            genLogic.UpdateCaseReviewType(new CaseReview.DataLayer.Models.CaseReviewType()
            {
                ID = model.ID,
                IsActive = model.IsActive,
                TypeName = model.Name
            });
            var json = jsonSerializer.Serialize(model);
            return Request.CreateResponse(HttpStatusCode.OK, json);
        }
        
        [System.Web.Http.HttpPost]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Authorize]
        public HttpResponseMessage addCaseType(BaseModel model)
        {
            var crtLogic = new GenericLogic<CaseReview.DataLayer.Models.CaseReviewType>();
            var newModel = crtLogic.Add(new CaseReview.DataLayer.Models.CaseReviewType()
            {
                ID = Guid.Empty,
                IsActive = model.IsActive,
                DisplayOrder = model.DisplayOrder,
                TypeName = model.Name
            });
            model.ID = newModel.ID;
            var json = jsonSerializer.Serialize(model);
            return Request.CreateResponse(HttpStatusCode.OK, json);
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Authorize]
        public HttpResponseMessage reorderCaseTypes([FromBody]string body)
        {
            var crtLogic = new GenericLogic<CaseReview.DataLayer.Models.CaseReviewType>();
            var model = jsonSerializer.Deserialize<List<Guid>>(body);
            List<CaseReview.DataLayer.Models.CaseReviewType> items = model.Select(o => new CaseReview.DataLayer.Models.CaseReviewType { ID = o}).ToList();
            crtLogic.UpdateReorder(items);
            var json = jsonSerializer.Serialize(model);
            return Request.CreateResponse(HttpStatusCode.OK, json);
        }

        

        [System.Web.Http.HttpPost]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Authorize]
        public HttpResponseMessage updateSection(BaseModel model)
        {
            var genLogic = new GeneralLogic();
            genLogic.UpdateSectionActiveName(new CaseReview.DataLayer.Models.Section()
            {
                ID = model.ID,
                IsActive = model.IsActive,
                SectionName = model.Name
            });
            var json = jsonSerializer.Serialize(model);
            return Request.CreateResponse(HttpStatusCode.OK, json);
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Authorize]
        public HttpResponseMessage addSection(BaseModel model)
        {
            var secLogic = new GenericLogic<CaseReview.DataLayer.Models.Section>();
            var newModel = secLogic.Add(new CaseReview.DataLayer.Models.Section()
            {
                ID = Guid.Empty,
                IsActive = model.IsActive,
                DisplayOrder = model.DisplayOrder,
                SectionName = model.Name
            });
            model.ID = newModel.ID;
            var json = jsonSerializer.Serialize(model);
            return Request.CreateResponse(HttpStatusCode.OK, json);
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Authorize]
        public HttpResponseMessage reorderSections([FromBody]string body)
        {
            var secLogic = new GenericLogic<CaseReview.DataLayer.Models.Section>();
            var model = jsonSerializer.Deserialize<List<Guid>>(body);
            List<CaseReview.DataLayer.Models.Section> items = model.Select(o => new CaseReview.DataLayer.Models.Section { ID = o }).ToList();
            secLogic.UpdateReorder(items);
            var json = jsonSerializer.Serialize(model);
            return Request.CreateResponse(HttpStatusCode.OK, json);
        }


        [System.Web.Http.HttpPost]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Authorize]
        public HttpResponseMessage updateQuestion(CaseReview.WebApp.Models.Question model)
        {
            var quLogic = new GenericLogic<CaseReview.DataLayer.Models.Question>();
            quLogic.Update(new CaseReview.DataLayer.Models.Question()
            {
                ID = model.ID,
                SectionID = model.SectionID,
                DisplayOrder = model.DisplayOrder,
                IsActive = model.IsActive,
                QuestionName = model.QuestionName,
                IsMandatory = model.IsMandatory,
                Risk = model.Risk
            });
            var json = jsonSerializer.Serialize(model);
            return Request.CreateResponse(HttpStatusCode.OK, json);
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Authorize]
        public HttpResponseMessage addQuestion(CaseReview.WebApp.Models.Question model)
        {
            var quLogic = new GenericLogic<CaseReview.DataLayer.Models.Question>();
            var newModel = quLogic.Add(new CaseReview.DataLayer.Models.Question()
            {
                ID = Guid.Empty,
                IsActive = model.IsActive,
                DisplayOrder = model.DisplayOrder,
                QuestionName = model.QuestionName,
                IsMandatory = model.IsMandatory,
                Risk = model.Risk,
                SectionID = model.SectionID
            });
            model.ID = newModel.ID;
            var json = jsonSerializer.Serialize(model);
            return Request.CreateResponse(HttpStatusCode.OK, json);
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Authorize]
        public HttpResponseMessage reorderQuestions([FromBody]string body)
        {
            var quLogic = new GenericLogic<CaseReview.DataLayer.Models.Question>();
            var model = jsonSerializer.Deserialize<List<Guid>>(body);
            List<CaseReview.DataLayer.Models.Question> items = model.Select(o => new CaseReview.DataLayer.Models.Question { ID = o }).ToList();
            quLogic.UpdateReorder(items);
            var json = jsonSerializer.Serialize(model);
            return Request.CreateResponse(HttpStatusCode.OK, json);
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Authorize]
        public HttpResponseMessage test([FromBody]string body)
        {
            List<int> items = new List<int>();
            items.Add(1);
            items.Add(2);
            var json = jsonSerializer.Serialize(items);
            return Request.CreateResponse(HttpStatusCode.OK, json);
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Authorize]
        public HttpResponseMessage UpdateCaseReviewTypeSection(Models.Api.UpdateCaseReviewTypeSection model)
        {
            var genLogic = new GeneralLogic();
            genLogic.UpdateCaseReviewTypeSection(model.CaseReviewTypeID, model.SectionID, model.IsIncluded);
            var json = jsonSerializer.Serialize(model);
            return Request.CreateResponse(HttpStatusCode.OK, json);
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Authorize]
        public HttpResponseMessage updateStaff(Models.Staff model)
        {
            var logic = new GenericLogic<CaseReview.DataLayer.Models.Staff>();
            logic.Update(new CaseReview.DataLayer.Models.Staff()
            {
                ID = model.ID,
                IsActive = model.IsActive,
                StaffFirstname = model.StaffFirstname,
                StaffSurname = model.StaffSurname,
                Email = model.Email
            });

            var json = jsonSerializer.Serialize(model);
            return Request.CreateResponse(HttpStatusCode.OK, json);
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Authorize]
        public HttpResponseMessage addStaff(Models.Staff model)
        {
            var logic = new GenericLogic<CaseReview.DataLayer.Models.Staff>();
            var newModel = logic.Add(new CaseReview.DataLayer.Models.Staff()
            {
                ID = Guid.Empty,
                IsActive = model.IsActive,
                StaffFirstname = model.StaffFirstname,
                StaffSurname = model.StaffSurname,
                Email = model.Email
            });
            model.ID = newModel.ID;
            var json = jsonSerializer.Serialize(model);
            return Request.CreateResponse(HttpStatusCode.OK, json);
        }

    }
}
