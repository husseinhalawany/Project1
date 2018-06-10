using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using System.Web.Routing;
using System.IO;
using BusinessLogic.Model;
using BusinessLogic.Core;
using DataMapping.Entities;
using BusinessLogic.Helpers;
using DataMapping.Services;
using System.Web.Script.Serialization;

namespace ManagementProject.Controllers
{
    [Authorize]
    public class ImageUploadController : BaseController
    {
        [AllowAnonymous]
        public ActionResult UploadDocument()
        {
            UserDataSession userSession = SessionData;
            userSession.ImgUrl = "";
            SessionData = userSession;
            return PartialView("CreateImage", SessionData.ImgUrl);
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult UploadIndexImage(HttpPostedFileBase file)
        {
            try
            {
                List<string> content = file.ContentType.Split('/').ToList();
                if (content[0] == "image")
                {
                    string userName = "";
                    if (SessionData == null)
                    {
                        userName = SessionData.UserName;
                    }
                    EditProfileModel generaladmin = EmployeesLogic.GetEditProfileModel(userName);
                    string fileName = Path.GetFileName(file.FileName);
                    string fileUrl = WebMessaging.UploadMessageAttachment(file, file.ContentType, fileName);
                    generaladmin.ImgURL = fileUrl;
                    EmployeesLogic.UpdateEmployee(generaladmin);
                    EmployeeUsersDetails model = new EmployeeUsersDetails { ImgURL = fileUrl };
                    return RedirectToAction("Index", "Home", model);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }

            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagementProject/ImageUpload/UploadIndexImage",
                    Parameters = new JavaScriptSerializer().Serialize(file)
                });
                return RedirectToAction("GeneralError", "Error", new { ErrorMessage = Error.ServerNotRespond });
            }


        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult UploadProfileImage(HttpPostedFileBase file)
        {
            string userName = "";
            if (SessionData == null)
            {

                userName = SessionData.UserName;
            }
            if (file != null)
            {
                List<string> content = file.ContentType.Split('/').ToList();
                if (content[0] == "image")
                {

                    string fileName = Path.GetFileName(file.FileName);
                    string fileUrl = WebMessaging.UploadMessageAttachment(file, file.ContentType, fileName);
                    UserDataSession userSession = SessionData;
                    userSession.ImgUrl = fileUrl;
                    SessionData = userSession;
                }

            }
            return PartialView("CreateImage", SessionData.ImgUrl);
        }
        [AllowAnonymous]
        public ActionResult UploadImageToEdit(string imgurl)
        {
            UserDataSession userSession = SessionData;
            userSession.ImgUrl = imgurl;
            SessionData = userSession;
            return PartialView("CreateImage", imgurl);
        }
        public ActionResult showimage(string imageUrl)
        {
            return View("ShowImagePartial", imageUrl);
        }
    }
}