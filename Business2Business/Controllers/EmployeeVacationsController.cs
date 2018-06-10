using System;
using System.Collections.Generic;
using System.Web.Mvc;
using BusinessLogic.Core;
using BusinessLogic.Model;
using DataMapping.Entities;
using DataMapping.Services;
using DataMapping.Enums;
using BusinessLogic.Helpers;
using System.Web.Script.Serialization;

namespace ManagementProject.Controllers
{
    [Authorize]
    public class EmployeeVacationsController : BaseController
    {
        public ActionResult Index(int? statusId, int? userId)
        {
            EmployeeVacationsIndexModel model;
            try
            {
                model = EmployeeVacationsLogic.GetEmployeeVacationsIndexModel(statusId, userId);
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagementProject/EmployeeVacations/Index",
                    Parameters = "statusId = " + statusId + "& userId= " + userId
                });
                model = new EmployeeVacationsIndexModel()
                {
                    EmployeeUsersList = new List<UserProfile>(),
                    VacationStatusList = new List<VacationStatu>()
                };
            }
            return View(model);
        }
        public ActionResult EmployeeVacationsList(int? pageNumber, int statusId, int employeeUserId)
        {
            var page = pageNumber ?? 0;
            List<EmplyeeVacationDetails> model = new List<EmplyeeVacationDetails>();
            try
            {
                if (SessionData.UserRole != UserRoles.Admin) employeeUserId = SessionData.UserId;
                model = EmployeeVacationsLogic.GetEmployeeVacationsList(page, statusId, employeeUserId);
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagementProject/EmployeeVacations/EmployeeVacationsList",
                    Parameters = "pageNumber= " + pageNumber.ToString() + "statusId= " + statusId + " & employeeUserId= " + employeeUserId

                });
            }

            return PartialView("EmployeeVacationsList", model);
        }
        public ActionResult Create(int userId = 0)
        {
            CreateEmployeeVacationModel model;
            try
            {
                if (userId == 0) userId = SessionData.UserId;
                model = EmployeeVacationsLogic.CreateEmployeeVacationRequest(userId);
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagementProject/EmployeeVacations/Create(Get)",
                });
                model = new CreateEmployeeVacationModel() { EmployeeVacation = new EmployeeVacation(), VacationTypesList = new List<VacationType>() };
            }
            return PartialView("CreatePartial", model);
        }
        [HttpPost]
        public ActionResult Create(CreateEmployeeVacationModel model)
        {
            try
            {
                model.IsAdmin = (SessionData.UserRole == UserRoles.Admin);
                EmployeeVacationsLogic.CreateEmployeeVacationRequest(model);
                    if (!model.Succeeded)
                    {
                        ModelState.AddModelError("ErrorMessage", model.ErrorMessage);
                        return PartialView("CreatePartial", model);
                    }
                string url = model.IsAdmin? "/Users/DeveloperIndex" : "/Employees/Index";
                return PartialView("JavascriptRedirect", new JavascriptRedirectModel(url));
                }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagementProject/EmployeeVacations/Create(Post)",
                    Parameters = new JavaScriptSerializer().Serialize(model)
                });
            }

            return PartialView("CreatePartial", model);
        }
        public ActionResult WorkFromHomeRequest()
        {
            CreateWorkFromHomeRequest model = new CreateWorkFromHomeRequest();

            try
            {
                bool isAdmin = SessionData.UserRole == UserRoles.Admin ? true : false;
                model = EmployeeVacationsLogic.GetCreateWorkFromHomeRequest(isAdmin, SessionData.UserId);
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
        public ActionResult WorkFromHomeRequest(CreateWorkFromHomeRequest model)
        {
            try
            {
                if (!model.Succeeded)
                {
                    return RedirectToAction("WorkFromHomeRequest");
                }
                EmployeeVacationsLogic.InsertNewWrokFromHomeRequest(model);
                ModelState.AddModelError("ErrorMessage", model.ErrorMessage);
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
        public ActionResult Approve(int id, int statusId, int userId)
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
                    Parameters = "id=" + id + "& statusId=" + statusId + "& userId=" + userId
                });
                return RedirectToAction("GeneralError", "Error", new { ErrorMessage = Error.ServerNotRespond });
            }
            return RedirectToAction("Index", new { statusId = statusId, userId = userId });
        }
        public ActionResult Reject(int id, int statusId, int userId)
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
                    Parameters = "id=" + id + "& statusId=" + statusId + "& userId=" + userId
                });
                return RedirectToAction("GeneralError", "Error", new { ErrorMessage = Error.ServerNotRespond });
            }
            return RedirectToAction("Index", new { statusId = statusId, userId = userId });
        }
        public ActionResult Cancel(int id, int statusId, int userId)
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
                    Parameters = "id=" + id + "& statusId=" + statusId + "& userId=" + userId
                });
                return RedirectToAction("GeneralError", "Error", new { ErrorMessage = Error.ServerNotRespond });
            }
            return RedirectToAction("Index", new { statusId = statusId, userId = userId });
        }
        public ActionResult Finish(int id, int statusId, int userId)
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
                    StoryName = "ManagementProject/EmployeeVacations/Finish",
                    Parameters = "id=" + id + "& statusId=" + statusId + "& userId=" + userId
                });
                return RedirectToAction("GeneralError", "Error", new { ErrorMessage = Error.ServerNotRespond });
            }
            return RedirectToAction("Index", new { statusId = statusId, userId = userId });
        }
        public int GetRemainingVacationDays(int userId, int vacationTypeId, int year = 0)
        {
            int remainingDaysOfVacation = 0;
            try
            {
                if (year == 0) year = DateTimeHelper.Today().Year;
                int totalTakenDaysOfVavationType = EmployeeVacationsLogic.GetUserVacationDays(userId, vacationTypeId, year);
                int vacationMaxDays = VacationTypesLogic.GetVacationTypeById(vacationTypeId).VacationLength;
                remainingDaysOfVacation = vacationMaxDays - totalTakenDaysOfVavationType;

            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagmentProject/EmployeeVacations/GetRemainingVacationDays",
                    Parameters = "userId=" + userId + "&vacationTypeId=" + vacationTypeId
                });
            }
            return remainingDaysOfVacation;
        }
        public int GetVacationStatusDays(int userId, int statusId)
        {
            int vacationStatusDays = 0;
            try
            {
                if (SessionData.UserRole == UserRoles.Admin)
                {
                    vacationStatusDays = EmployeeVacationsLogic.GetVacationStatusDaysByUserId(userId, statusId);
                }
                else
                {
                    vacationStatusDays = EmployeeVacationsLogic.GetVacationStatusDaysByUserId(SessionData.UserId, statusId);
                }
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagementProject/EmployeeVacations/GetVacationStatusDays",
                    Parameters = "userId= " + userId + "& statusId=" + statusId
                });
            }
            return vacationStatusDays;
        }
        public int VacationDays(DateTime startDate, DateTime endDate)
        {
            int vacationDays;
            vacationDays = EmployeeVacationsLogic.GetRequestedVacationDays(startDate, endDate);
            return vacationDays;
        }
        public ActionResult EmployeeApprovedVacations(int userId)
        {
            List<EmplyeeVacationDetails> employeeVacationsDetails = EmployeeVacationsLogic.GetEmployeeVacationTypeDetails((int)EVacationStatus.Approved, userId);
            return View(employeeVacationsDetails);
        }
        public ActionResult EmployeeAttendence(int userId)
        {
            DateTime today = DateTimeHelper.Today();
            DateTime firstDay = new DateTime(today.Year, today.Month, 1);
            List<Attendance> userAttendances = AttendancesLogic.GetAllUserAttendencesBetweenTwoDates(userId, firstDay, today);
            int totalWorkingDaysInMonth = DateTimeHelper.GetWorkingDaysCountInMonth(firstDay, today);
            int totalUserWorkingDays = userAttendances.Count;
            UserProfile user = UserProfilesLogic.GetUserById(userId);
            UserAttendancesModel model = new UserAttendancesModel
            {
                TotalWorkingDaysInMonth = totalWorkingDaysInMonth,
                TotalUserWorkingDays = totalUserWorkingDays,
                TotalUserAbsenceDays = (totalWorkingDaysInMonth - totalUserWorkingDays),
                TotalUserWorkingHours = Math.Round(AttendancesLogic.GetTotalUserWorkingHoursInMonth(userId, firstDay, today), 2),
                UserAttendances = userAttendances,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
            return View(model);
        }
    }
}