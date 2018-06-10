using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;
using BusinessLogic.Core;
using System.Web.Routing;
using MangamentProject.Filters;
using MangamentProject.Models;
using BusinessLogic.Model;
using DataMapping.Services;
using ManagementProject.Controllers;
using DataMapping.Entities;
using BusinessLogic.Helpers;
using System.Web.Script.Serialization;
using MangamentProject.Core;

namespace MangamentProject.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class UsersController : BaseController
    {
        public IFormsAuthenticationService FormsService { get; set; }
        public IMembershipService MembershipService { get; set; }
        protected override void Initialize(RequestContext requestContext)
        {
            if (FormsService == null) { FormsService = new FormsAuthenticationService(); }
            if (MembershipService == null) { MembershipService = new AccountMembershipService(); }

            base.Initialize(requestContext);
        }
        public ActionResult Index()
        {
            UseresIndexModel model = new UseresIndexModel();
            try
            {
                model.RoleId = UsersLogic.GetAdminRole();
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagmentProject/Users/Index",
                });
            }
            return View(model);
        }
        public ActionResult DeveloperIndex()
        {
            return View("Index", new UseresIndexModel() { RoleId = 0 });
        }
        public ActionResult UsersList(int roleId, int? pageNo)
        {
            var page = pageNo ?? 0;
            List<EmployeeUsersDetails> employeesList = new List<EmployeeUsersDetails>();
            try
            {
                if (roleId == 0)
                {
                    employeesList = EmployeesLogic.GetEmployeesExceptAdmin(page);
                }
                else
                {
                    employeesList = EmployeesLogic.GetEmployeesByRoleId(roleId, page);
                }
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagmentProject/Users/UsersList",
                    Parameters = "roleId=" + roleId.ToString() + " pageNo= " + pageNo.Value.ToString()
                });
            }
            return PartialView("UsersList", employeesList);
        }
        public ActionResult UsersHistory()
        {
            return View();
        }
        public ActionResult UsersHistoryList(int? pageNo)
        {
            var page = pageNo ?? 0;
            List<UserHistoryDetails> employeesList = new List<UserHistoryDetails>();
            try
            {
                employeesList = EmployeesLogic.GetUsersHistory(page);
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagmentProject/Users/UsersHistoryList",
                    Parameters = "pageNo= {pageNo.Value}"
                });
            }
            return PartialView(employeesList);
        }
        public ActionResult Create(int? roleId)
        {
            int roleID = roleId ?? 0;
            EmployeeUsersDetails model = new EmployeeUsersDetails();

            try
            {

                model = UserProfilesLogic.GetCreateModel(roleID);
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagmentProject/Users/Create"
                });
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(EmployeeUsersDetails model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    DateTime BirthDate = new DateTime(model.year, model.Month, model.day);

                    model.ImgURL = SessionData.ImgUrl;
                    WebSecurity.CreateUserAndAccount(model.UserName, model.PassWord, new
                    {
                        Address = model.Address,
                        Phone1 = model.Phone1,
                        Phone2 = model.Phone2,
                        Email = model.Email,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        BirthDate = BirthDate,
                        ProfilePictureUrl = model.ImgURL,
                        Name = model.FirstName + " " + model.LastName,
                    });
                    EmployeesLogic.InsertNewEmployee(model);

                    if (model.RoleId == 8)
                        return RedirectToAction("DeveloperIndex");

                    return RedirectToAction("Index", new { roleId = model.RoleId });
                }
                catch (MembershipCreateUserException e)
                {
                    LogsLogic.InsertLog(new Log()
                    {
                        Message = e.Message,
                        StackTrace = e.StackTrace,
                        StoryName = "ManagmentProject/Users/Create(Get)",
                        Parameters = new JavaScriptSerializer().Serialize(model)
                    });
                    ModelState.AddModelError("UserName", ErrorCodeToString(e.StatusCode));
                }
            }
            return View(model);
        }
        public ActionResult Edit(int? id, int? roleId)
        {
            int Id = id ?? SessionData.UserId;
            int roleID = roleId ?? 0;
            EditProfileModel model = new EditProfileModel();
            try
            {
                model = EmployeesLogic.GetEditProfileModel(Id);
                model.RoleId = roleID;
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagmentProject/Users/Edit(Get)",
                    Parameters = "Id = " + id.ToString()
                });
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(EditProfileModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.ImgURL = SessionData.ImgUrl;
                    model.BirthDate = new DateTime(model.year, model.Month, model.day);
                    model.ImgURL = SessionData.ImgUrl;
                    EmployeesLogic.UpdateEmployee(model);
                    if (model.RoleId == 8)
                        return RedirectToAction("DeveloperIndex");

                    return RedirectToAction("Index", new { roleId = model.RoleId });

                }
                catch (Exception e)
                {
                    LogsLogic.InsertLog(new Log()
                    {
                        Message = e.Message,
                        StackTrace = e.StackTrace,
                        StoryName = "ManagementProject/Users/Edit(Post)",
                        Parameters = new JavaScriptSerializer().Serialize(model)
                    });
                }
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Delete(int id, int roleId)
        {
            try
            {
                EmployeesLogic.DeleteEmployee(id);
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagementProject/Users/Delete",
                    Parameters = "id = " + id.ToString() + " & roleId = " + roleId.ToString()
                });
            }
            if (roleId == 8) return RedirectToAction("DeveloperIndex");

            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Lock(int id, int? roleId)
        {
            try
            {
                EmployeesLogic.LockEmployee(id);
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagementProject/Users/Lock",
                    Parameters = "id = " + id.ToString() + " & roleId = " + roleId.ToString()
                });
            }
            if (roleId == 8) return RedirectToAction("DeveloperIndex");

            return RedirectToAction("Index");
        }
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";
                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";
                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";
                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";
                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";
                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";
                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";
                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
    }
}