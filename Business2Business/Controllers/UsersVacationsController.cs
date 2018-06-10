using ManagementProject.Controllers;
using BusinessLogic.Core;
using BusinessLogic.Model;
using DataMapping.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using DataMapping.Services;
using BusinessLogic.Helpers;
using System.Web.Security;
using DataMapping.Enums;

namespace MangamentProject.Controllers
{
    [Authorize]
    public class UsersVacationsController : BaseController
    {
        public ActionResult Index(string statusId)
        {
            return View(new UsersVacationsDetails { StatusId = Int32.Parse(statusId) });
        }
        public ActionResult UsersVacationsList(int? pageNumber, int? userId, int statusId)
        {
            var page = pageNumber ?? 0;
            List<EmplyeeVacationDetails> model = new List<EmplyeeVacationDetails>();
            try
            {
                if (Roles.IsUserInRole("Admin"))
                {
                    model = EmployeeVacationsLogic.GetUsersVacationsList(page, statusId);
                }
                else
                {
                    model = EmployeeVacationsLogic.GetUsersVacationsList(page, statusId, SessionData.UserId);
                }
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagementProject/EmployeeVacations/EmployeeVacationsList",
                    Parameters = "pageNumber= " + pageNumber.ToString() + "statusId= " + statusId

                });
            }

            return PartialView("UsersVacationsList", model);
        }
        public ActionResult Approve(int id, int statusId)
        {
            try
            {
                EmployeeVacationsLogic.ChangeStatus(id, EVacationStatus.Approved);
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
                EmployeeVacationsLogic.ChangeStatus(id, EVacationStatus.Rejected);
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
        public ActionResult Complete(int id, int statusId)
        {
            try
            {
                EmployeeVacationsLogic.ChangeStatus(id, EVacationStatus.Finished);
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagementProject/EmployeeVacations/Complete",
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
                EmployeeVacationsLogic.ChangeStatus(id, EVacationStatus.Canceled);
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
        public ActionResult CompletedVacations()
        {
            return View();
        }
        public ActionResult CompletedVacationsList(int? pageNumber)
        {
            var page = pageNumber ?? 0;
            List<EmplyeeVacationDetails> model = new List<EmplyeeVacationDetails>();
            try
            {
                if (Roles.IsUserInRole("Admin"))
                    model = EmployeeVacationsLogic.GetAllCompletedVacations(page, EVacationStatus.Finished);
                else
                    model = EmployeeVacationsLogic.GetCompletedVacations(page, SessionData.UserId, EVacationStatus.Finished);
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagementProject/EmployeeVacations/CompletedVacationsList",
                    Parameters = "pageNumber= " + pageNumber.ToString()

                });
            }

            return PartialView("CompletedVacationsList", model);
        }
    }
}