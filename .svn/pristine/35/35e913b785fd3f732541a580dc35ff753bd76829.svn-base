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
    public class AdminController : ApiController
    {
        private GenericLogic<CaseReviewType> crtLogic = new GenericLogic<CaseReviewType>();

        private JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();

        [System.Web.Http.HttpPost]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Authorize]
        public HttpResponseMessage updateCaseType(CaseReviewTypeModel model)
        {
            crtLogic.UpdateActive(new CaseReviewType()
            {
                ID = model.ID,
                IsActive = model.IsActive
            });
            var json = jsonSerializer.Serialize(model);
            return Request.CreateResponse(HttpStatusCode.OK, json);
        }


        [System.Web.Http.HttpPost]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Authorize]
        public HttpResponseMessage addCaseType(CaseReviewTypeModel model)
        {
            var newModel = crtLogic.Add(new CaseReviewType()
            {
                ID = model.ID,
                IsActive = model.IsActive,
                DisplayOrder = model.DisplayOrder,
                TypeName = model.TypeName
            });
            var json = jsonSerializer.Serialize(newModel);
            return Request.CreateResponse(HttpStatusCode.OK, json);
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Authorize]
        public HttpResponseMessage reorderCaseTypes([FromBody]string body)
        {
            var model = jsonSerializer.Deserialize<List<int>>(body);
            List<CaseReviewType> items = model.Select(o => new CaseReviewType { ID = o}).ToList();
            crtLogic.UpdateReorder(items);
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

    }
}
