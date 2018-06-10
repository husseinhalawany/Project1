using BusinessLogic.Core;
using BusinessLogic.Helpers;
using BusinessLogic.Model;
using DataMapping.Entities;
using DataMapping.Enums;
using DataMapping.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ManagementProject.Controllers
{
    public class WorkFromHomeController : BaseController
    {
        // GET: WorkFromHome
        public ActionResult Index(int statusId)
        {
            return View(new WorkFromHomeDay { StatusId = statusId });
        }
        public ActionResult EmployeeWorkFromHomeDaysList(int? pageNumber, int statusId, int ? employeeUserId)
        {
            var page = pageNumber ?? 0;
            List<RequestFromHomeDayDetails> model = new List<RequestFromHomeDayDetails>();
            try
            {
                if (Roles.IsUserInRole("Admin"))
                {
                    model = WorkFromHomeLogic.GetEmployeeWorkFromHomeDaysList(page, statusId);
                }
                else
                {
                    model = WorkFromHomeLogic.GetEmployeeWorkFromHomeDaysList(page, statusId, SessionData.UserId);
                }
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagementProject/WorkFromHome/EmployeeWorkFromHomeDaysList",
                    Parameters = "pageNumber= " + pageNumber.ToString() + "statusId= " + statusId + " & employeeUserId= " + employeeUserId

                });
            }

            return PartialView("EmployeeWorkFromHomeDaysList", model);
        }
        public ActionResult CreateWorkFromHome(int userId=0)
        {
            WorkFromHomeModel model = new WorkFromHomeModel();
            try
            {
                if (userId == 0) userId = SessionData.UserId;
                bool isAdmin = SessionData.UserRole == UserRoles.Admin ? true : false;
                model = WorkFromHomeLogic.CreateWorkFomHomeModel(isAdmin, userId);
                if (!model.Succeeded)
                    ModelState.AddModelError("ErrorMessage", model.ErrorMessage);
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagementProject/EmployeeVacations/WorkFromHomeRequest(Get)",
                });
            }
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult CreateWorkFromHome(WorkFromHomeModel model)
        {
            try
            {
                if (!model.Succeeded)
                {
                    ModelState.AddModelError("ErrorMessage", model.ErrorMessage);
                    return PartialView(model);
                }
                model= WorkFromHomeLogic.CheckWrokFromHomeRequestModel(model);
                if (model.Succeeded)
                {
                    WorkFromHomeLogic.InsertNewWrokFromHomeRequest(model);
                    string url = Roles.IsUserInRole("Admin") ? "/Users/DeveloperIndex" : "/Employees/Index";
                    return PartialView("JavascriptRedirect", new JavascriptRedirectModel(url));
                }
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagementProject/EmployeeVacations/WorkFromHomeRequest(Post)",
                });
            }
            return PartialView(model);
        }
        public ActionResult Approve(int id, int statusId)
        {
            try
            {
                WorkFromHomeLogic.ChangeStatus(id, EVacationStatus.Approved);
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagementProject/EmployeeVacations/Approve",
                    Parameters = "id=" + id + "& statusId=" + statusId
                });
                return RedirectToAction("GeneralError", "Error", new { ErrorMessage = Error.ServerNotRespond });
            }
            return RedirectToAction("Index", new { statusId = statusId });
        }
        public ActionResult Reject(int id, int statusId)
        {
            try
            {
                WorkFromHomeLogic.ChangeStatus(id, EVacationStatus.Rejected);
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagementProject/EmployeeVacations/Reject",
                    Parameters = "id=" + id + "& statusId=" + statusId
                });
                return RedirectToAction("GeneralError", "Error", new { ErrorMessage = Error.ServerNotRespond });
            }
            return RedirectToAction("Index", new { statusId = statusId });
        }
        public ActionResult Cancel(int id, int statusId)
        {
            try
            {
                WorkFromHomeLogic.ChangeStatus(id, EVacationStatus.Canceled);
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagementProject/EmployeeVacations/Cancel",
                    Parameters = "id=" + id + "& statusId=" + statusId
                });
                return RedirectToAction("GeneralError", "Error", new { ErrorMessage = Error.ServerNotRespond });
            }
            return RedirectToAction("Index", new { statusId = statusId });
        }
    }
}