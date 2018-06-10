using System;
using System.Web.Mvc;
using BusinessLogic.Core;
using DataMapping.Entities;
using BusinessLogic.Model;
using BusinessLogic.Helpers;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using DataMapping.Services;

namespace ManagementProject.Controllers
{
    [Authorize]
    public class SprintsController : BaseController
    {
        public ActionResult Index(int? sprintId)
        {
            List<Sprint> sprints = SprintsLogic.GetAllSprints();
            if (sprintId == null && StoriesFilterSession == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (StoriesFilterSession == null && sprints.Count > 0)
            {
                StoriesFilterSession = new StoriesFilter();
                StoriesFilterSession.Sprints = new SprintModel()
                {
                    Current = sprints.Find(a => a.CurrentSprint),
                    Future = sprints.FindAll(a => a.FutureSprint),
                    Previous = sprints.FindAll(a => a.PreviousSprint)
                };
            }
            StoriesFilterSession.sprintId = sprintId.Value;

            return View(StoriesFilterSession);
        }
        public ActionResult SprintProjects()
        {
            return View();
        }
        public ActionResult SprintProjectsList(string searchTxt = "")
        {
            List<SprintProjectsDetails> ProjectsDetails = ProjectsLogic.GetProjectsBySprintId(StoriesFilterSession.sprintId, searchTxt);
            return PartialView(ProjectsDetails);
        }
        public ActionResult Create(bool isOneWeek)
        {
            try
            {
                SprintsLogic.InsertNewSprint(new Sprint()
                {
                    CreateByUserId = SessionData.UserId,
                    IsOneWeek = isOneWeek
                });
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagementProject/Sprints/Create"
                });
            }
            return RedirectToAction("Index", "Home");
        }
        public void SetCurrent()
        {
            try
            {
                SprintsLogic.SetCurrent();
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagementProject/Sprints/SetCurrent"
                });
            }

        }

        [HttpPost]
        public ActionResult AssignProjectToSprint(string projectName)
        {
            try
            {
                if (SprintsLogic.InsertNewSprintProject(StoriesFilterSession.sprintId, projectName))
                {
                    return Json("success", JsonRequestBehavior.AllowGet);
                    //return RedirectToAction("Index", new { sprintId = StoriesFilterSession.sprintId });
                }
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagementProject/Sprints/AssignProjectToSprint(Post)",
                    Parameters = projectName
                });

            }
            return Json("fail", JsonRequestBehavior.AllowGet);
        }
    }
}