﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CaseReview.BusinessLogic;
using CaseReview.DataLayer;
using CaseReview.WebApp.Models;
using Answer = CaseReview.DataLayer.Models.Answer;

namespace CaseReview.WebApp.Controllers
{
    public class saveanswerController : ApiController
    {
        // api/saveanswer

        [System.Web.Http.HttpPost]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage Get(SaveAnswerReq model)
        {
            Answer answer = new Answer()
            {
                ID = model.id,
                Comments = model.comments,
                Compliant = model.compliant
            };
            new GeneralLogic().UpdateAnswer(answer);
            return Request.CreateResponse(HttpStatusCode.OK, "worked");
        }



    }
}