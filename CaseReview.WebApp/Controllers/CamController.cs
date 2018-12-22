using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CaseReview.BusinessLogic;
using CaseReview.DataLayer.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using IdentitySample.Models;

namespace CaseReview.WebApp.Controllers
{
    public class CamController : Controller
    {

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [Authorize(Roles = "Cam")]
        // GET: Cam
        public ActionResult Index()
        {
            var email = UserManager.GetEmail(User.Identity.GetUserId());
            var model = new GeneralLogic().GetCamsAnswers(email);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string acceptance)
        {
            var email = UserManager.GetEmail(User.Identity.GetUserId());
            new GeneralLogic().AcceptNonCompliance(email);
            var model = new List<Answer>();
            return View(model);
        }

        [Authorize(Roles = "Admin,Report")]
        // GET: Cam
        public ActionResult Report()
        {
            var model = new GenericLogic<vwCamNotAcepted>().GetAll();
            return View(model);
        }
    }
}