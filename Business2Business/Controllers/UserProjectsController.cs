using System;
using System.Collections.Generic;
using System.Linq;
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
namespace B2B.Controllers
{
    [Authorize]
    public class UserProjectsController : BaseController
    {
        public ActionResult UserProjectsList(int projectId)
        {

            ProjectModel model = new ProjectModel();
            try
            {
                model.UsersList = UsersProjectLogic.GetUserProjectsList(projectId);
                model.ProjectId = projectId;
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagementProject/UserProjects/ProjectUsersList"
                });
            }
            return PartialView("ProjectUsersList", model);
        }
        public ActionResult UserProjectsListDetails(int projectId)
        {

            UsersProjectUpdateDetails model = new UsersProjectUpdateDetails();
            try
            {
                model.usersInProject = UsersProjectLogic.GetUserProjectsList(projectId);
                model.userProject.ProjectId = projectId;
                model.userProject.ProjectName = model.usersInProject.FirstOrDefault().ProjectName;

            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagementProject/UserProjects/ProjectUsersList"
                });
            }
            return PartialView(model);
        }
        public JsonResult GetUserNames(string term,int projectId)
        {
            List<string> UsersNames = new List<string>();
            try
            {
                UsersNames = UserProfilesLogic.GetUserNamesNotInProjectByTerm(term, projectId);
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagementProject/UserProjects/GetUserNames",
                    Parameters = "term= " + term
                });
            }
            return Json(UsersNames, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Create(int projectId)
        {
            UsersProjectUpdateDetails model = new UsersProjectUpdateDetails();
            try
            {
                model = UsersProjectLogic.GetUserProjectModelForCreate(projectId);
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagementProject/UserProjects/Create(Get)",
                });
            }
            return View("Create", model);
        }
        [HttpPost]
        public ActionResult Create(UsersProjectUpdateDetails model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.userProject.CreatorId = SessionData.UserId;
                    model.userProject.UserName = model.To;
                    UsersProjectLogic.CreateUserProject(model.userProject);
                    return RedirectToAction("UserProjectsListDetails", new { projectId = model.userProject.ProjectId });
                }
                catch (Exception e)
                {
                    LogsLogic.InsertLog(new Log()
                    {
                        Message = e.Message,
                        StackTrace = e.StackTrace,
                        StoryName = "ManagementProject/UserProjects/Create(Post)",
                        Parameters = new JavaScriptSerializer().Serialize(model)
                    });
                }
            }
            UsersProjectUpdateDetails newModel = UsersProjectLogic.GetUserProjectModelForCreate(model.userProject.ProjectId);
            model.users = newModel.users;
            model.projectRoles = newModel.projectRoles;
            return View(model);
        }
        public ActionResult Edit(int userId, int projectId)
        {
            UsersProjectUpdateDetails model = new UsersProjectUpdateDetails();
            try
            {
                model = UsersProjectLogic.GetUserProjectUpdateModel(userId, projectId);
                return PartialView("Edit", model);
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagementProject/UserProjects/Edit(Get)",
                    Parameters = "userId=" + userId + "& projectId=" + projectId
                });
            }
            return PartialView("Edit", model);
        }
        [HttpPost]
        public ActionResult Edit(UsersProjectUpdateDetails model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    UsersProjectLogic.UpdateUserProject(model.userProject);
                    return RedirectToAction("Create", new { projectId = model.userProject.ProjectId });

                }
                catch (Exception e)
                {
                    LogsLogic.InsertLog(new Log()
                    {
                        Message = e.Message,
                        StackTrace = e.StackTrace,
                        StoryName = "ManagementProject/UserProjects/Edit(Post)",
                        Parameters = new JavaScriptSerializer().Serialize(model)
                    });
                }
            }
            UsersProjectUpdateDetails newModel = UsersProjectLogic.GetUserProjectModelForCreate(model.userProject.ProjectId);
            model.projectRoles = newModel.projectRoles;
            return View(model);
        }
        [HttpPost]
        public ActionResult Delete(int userId, int projectId)
        {
            try
            {
                UsersProjectLogic.DeleteUserFromProject(userId, projectId);
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagementProject/UserProjects/Delete",
                    Parameters = "userId=" + userId + "& projectId=" + projectId
                });
            }
            return RedirectToAction("UserProjectsListDetails", new { projectId = projectId });
        }
    }
}

