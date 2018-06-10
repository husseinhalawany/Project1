using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogic.Model;
using BusinessLogic.Core;
using DataMapping.Entities;

namespace ManagementProject.Controllers
{
    public class ErrorController : Controller
    {

        [Authorize]
        public ActionResult GeneralError(string errorMessage = null, string parameter = null)
        {
            string path="";
            if(errorMessage != null)
            {
                if (Request.UrlReferrer != null) path = Request.UrlReferrer.AbsolutePath;
            }

            ReloadURLModel Model = new ReloadURLModel { URL = path, Message =errorMessage};
            if (errorMessage == null)
                errorMessage = "An Error Occured";

            LogsLogic.InsertLog(new Log()
            {
                Message = errorMessage,
                StoryName = path,
                Parameters = parameter
            });

            return View(Model);
        }

    }
}
