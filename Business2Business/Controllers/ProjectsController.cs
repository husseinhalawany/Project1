using System;
using System.Web.Mvc;
using BusinessLogic.Core;
using DataMapping.Entities;
using BusinessLogic.Model;
using ManagementProject.Controllers;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using DataMapping.Services;
using BusinessLogic.Helpers;
using System.Linq;

namespace MangamentProject.Controllers
{
    [Authorize]
    public class ProjectsController : BaseController
    { 
        public ActionResult Index(int projectId)
        {
            return View(new ProjectModel()
            {
                Sprint = new Sprint(),
                ProjectId = projectId
            }
            );
        }
        public ActionResult ProjectsInSprint()
        {
            List<Project> model = new List<Project>();
            try
            {
              model=  ProjectsLogic.GetProjectsBySprints(StoriesFilterSession.sprintId);
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagementProject/Projects/ProjectsInSprint",
                });
                
            }
            return View(model);
        }
        public ActionResult Create()
        {
            return PartialView("Create");
        }
        [HttpPost]
        public ActionResult Create(Project project)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    project.CreatorId = SessionData.UserId;
                    project.CreateDate = DateTimeHelper.Today();
                    project.UpdateDate = DateTimeHelper.Today();
                    ProjectsLogic.InsertNewProject(project);
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception e)
                {
                    LogsLogic.InsertLog(new Log()
                    {
                        Message = e.Message,
                        StackTrace = e.StackTrace,
                        StoryName = "ManagementProject/Projects/Create(Post)",
                        Parameters = new JavaScriptSerializer().Serialize(project)
                    });
                    return View(project);
                }
            }
            return View(project);
        }
        public ActionResult Edit(int id)
        {
            Project project = new Project();
            try
            {
                project = ProjectsLogic.GetProjectById(id);
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagementProject/Projects/Edit(Get)",
                    Parameters = "id=" + id
                });
            }
            return PartialView("Edit", project);
        }
        [HttpPost]
        public ActionResult Edit(Project project)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    project.UpdateDate = DateTimeHelper.Today();
                    ProjectsLogic.UpdateProject(project);
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception e)
                {
                    LogsLogic.InsertLog(new Log()
                    {
                        Message = e.Message,
                        StackTrace = e.StackTrace,
                        StoryName = "ManagementProject/Projects/Edit(Post)",
                        Parameters = new JavaScriptSerializer().Serialize(project)
                    });
                    return View(project);
                }
            }
            return View(project);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                ProjectsLogic.DeleteProject(id);
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagementProject/Projects/Delete",
                    Parameters = "id=" + id
                });
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult GetUserProjectList(int? pageNo, int userId)
        {

            var page = pageNo ?? 0;
            List<ProjectUserDetails> model = new List<ProjectUserDetails>();
            try
            {
                model = ProjectsLogic.GetUserProjectList(page, userId);
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagementProject/Projects/GetUserProjectList",

                });
            }
            return PartialView("GetUserProjectList", model);


        }
        public ActionResult SprintProjectList(string searchTxt = "")
        {
            List<SprintProjectsDetails> projects = new List<SprintProjectsDetails>();
            try
            {

                projects = ProjectsLogic.GetProjectsBySprintId(StoriesFilterSession.sprintId, searchTxt);
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagementProject/Projects/SprintProjectList",
                    Parameters = "sprintId= " + StoriesFilterSession.sprintId
                });
                return RedirectToAction("GeneralError", "Error", new { ErrorMessage = Error.ServerNotRespond });
            }
            return PartialView("StoriesSprintList", projects);
        }

        [HttpPost]
        public ActionResult CreateNewProjectAndAssignToSprint(string projectName)
        {
            try
            {
                if (ProjectsLogic.InsertProjectSprint(StoriesFilterSession.sprintId, projectName, SessionData.UserId))
                {
                    return Json("success", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagementProject/Projects/CreateNewProjectAndAssignToSprint(Post)",
                    Parameters = new JavaScriptSerializer().Serialize(projectName)
                });

            }
            return Json("fail", JsonRequestBehavior.AllowGet);
        }
        public JsonResult AutocompleteProjects(bool isInSprint)
        {
            List<string> projects = new List<string>();
            try
            {
                projects = ProjectsLogic.AutocompleteProjects(StoriesFilterSession.sprintId, isInSprint);
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagementProject/Projects/Search"
                });
            }
            return Json(projects, JsonRequestBehavior.AllowGet);
        }
    }
}