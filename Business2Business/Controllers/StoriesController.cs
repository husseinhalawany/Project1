using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BusinessLogic.Core;
using DataMapping.Entities;
using BusinessLogic.Model;
using DataMapping.Services;
using ManagementProject.Controllers;
using System.Net;
using BusinessLogic.Helpers;
using System.Web.Script.Serialization;

namespace MangamentProject.Controllers
{
    [Authorize]
    public class StoriesController : BaseController
    {
        public ActionResult Index(int projectId)
        {
            StoriesIndexModel model = new StoriesIndexModel();
            try
            {
                if (StoriesFilterSession == null)
                {
                    StoriesFilter story = new StoriesFilter()
                    {
                        AllStories = StoriesLogic.GetStoriesList(projectId),
                        projectId = projectId,
                        sprintId = SprintsLogic.GetCurrentSprint(projectId)
                };

                    StoriesFilterSession = story;
                }

                model = StoriesLogic.GetStoriesIndexModel(StoriesFilterSession, projectId);
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagementProject/Stories/Index",
                    Parameters = "projectId= " + projectId
                });
                return RedirectToAction("GeneralError", "Error", new { ErrorMessage = Error.ServerNotRespond });
            }

            return View(model);
        }
        public ActionResult StorySprint(int projectId=0,int sprintId=0)
        {
            StoriesIndexModel model = new StoriesIndexModel();
            try
            {
               // StoriesFilterSession.sprintId = sprintId;
                StoriesFilterSession.projectId = projectId;
                model = StoriesLogic.GetStoriesIndexModel(StoriesFilterSession, projectId);
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagementProject/Stories/Index",
                    Parameters = "projectId= " + projectId
                });
                return RedirectToAction("GeneralError", "Error", new { ErrorMessage = Error.ServerNotRespond });
            }

            return View(model);
        }
        public ActionResult StoriesSprintList(int projectId=0,string searchTxt="")
        {
            List<StoriesDetails> stories = new List<StoriesDetails>();
            StoriesListModel model = new StoriesListModel();
            try
            {

                stories = StoriesLogic.GetStoriesListAndItemCount(projectId,StoriesFilterSession.sprintId);
                model.Stories = stories;
                model.StoriesCount = stories.Count;
                if (searchTxt != "")
                {
                    model.Stories = stories.Where(x => x.Name.Contains(searchTxt)).ToList();
                }
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagementProject/Stories/StoriesList",
                    Parameters = "projectId= " + projectId
                });
                return RedirectToAction("GeneralError", "Error", new { ErrorMessage = Error.ServerNotRespond });
            }
            return PartialView("StoriesSprintList", model);
        }

        public ActionResult StoriesList(int projectId)
        {
            List<StoriesDetails> stories = new List<StoriesDetails>();
            StoriesListModel model = new StoriesListModel();
            try
            {
                stories = StoriesLogic.GetFilteredStoriesList(StoriesFilterSession);
                model.Stories = stories;
                model.StoriesCount = stories.Count;

            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagementProject/Stories/StoriesList",
                    Parameters = "projectId= " + projectId
                });
                return RedirectToAction("GeneralError", "Error", new { ErrorMessage = Error.ServerNotRespond });
            }
            return PartialView("StoriesList", model);
        }
        [HttpPost]
        public ActionResult CreateStorySprint(int projectId,string storyName="")
        {
            try
            {
                if (StoriesLogic.InsertStorySprint(projectId, StoriesFilterSession.sprintId, storyName, SessionData.UserId))
                {
                    return Json("success", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("fail", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagementProject/Stories/CreateStorySprint(Post)",
                    Parameters = new JavaScriptSerializer().Serialize(storyName)
                });

            }
            return Json("fail", JsonRequestBehavior.AllowGet);
        }

        public JsonResult Search(int projectId)
        {
            List<string> stories = new List<string>();
            try
            {
                stories = StoriesLogic.AutocompleteStories(projectId, StoriesFilterSession.sprintId);
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagementProject/Stories/Search",
                    Parameters = "projectId= " + projectId
                });
            }
            return Json(stories, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SearchForAddStory(int projectId, int exist)
        {
            List<string> stories = new List<string>();
            try
            {
                if (exist == 1)
                {
                    stories = StoriesLogic.AutocompleteStoriesForAdd(projectId);
                }
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagementProject/Stories/Search",
                    Parameters = "projectId= " + projectId
                });
            }
            return Json(stories, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Create(int projectId)
        {
            StoryCreateModel story = new StoryCreateModel { ProjectId = projectId, selectedType = 1 };
            return View(story);
        }
        [HttpPost]
        public ActionResult Create(StoryCreateModel storyModel, FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Story model = new Story()
                    {
                        ProjectId = storyModel.ProjectId,
                        Exist = false,
                        sprintId = StoriesFilterSession.sprintId
                    };
                    
                    if (storyModel.selectedType == 1)
                    {
                        model.Exist = true;
                        model.Name = collection["search"].ToString();
                    }
                    else
                    {
                        model.Name = collection["NewStory"].ToString();
                    }
                    model.CreatorId = SessionData.UserId;
                    model.CreateDate = DateTimeHelper.Today();
                    
                    StoriesDetails newStory = StoriesLogic.InsertNewStory(model);
                    if (newStory != null)
                    {
                        StoriesFilter story = StoriesFilterSession;
                        story.AllStories.Add(newStory);
                        StoriesFilterSession = story;

                    }
                    if (newStory == null && storyModel.selectedType == 2)
                    {
                        // return pop up to set option for create with the same name
                    }

                }
                catch (Exception e)
                {
                    LogsLogic.InsertLog(new Log()
                    {
                        Message = e.Message,
                        StackTrace = e.StackTrace,
                        StoryName = "ManagementProject/Stories/Create(Post)",
                        Parameters = new JavaScriptSerializer().Serialize(storyModel)
                    });

                }
            }
            return RedirectToAction("StoriesList", new { projectId = storyModel.ProjectId });
        }

        public ActionResult Edit(int id)
        {
            Story model = new Story();
            try
            {
                model = StoriesLogic.GetStoryById(id);
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagementProject/Stories/Edit(Get)",
                    Parameters = new JavaScriptSerializer().Serialize(model)
                });

            }
            return PartialView("EditPartial", model);
        }
        [HttpPost]
        public ActionResult Edit(Story model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    StoriesLogic.UpdateStory(model);
                    StoriesFilter story = StoriesFilterSession;
                    story.AllStories = StoriesLogic.GetStoriesList(model.ProjectId);
                    StoriesFilterSession = story;
                    string URL = "/Stories/Index?ProjectId=" + model.ProjectId;
                    return PartialView("JavascriptRedirect", new JavascriptRedirectModel(URL));
                }
                catch (Exception e)
                {
                    LogsLogic.InsertLog(new Log()
                    {
                        Message = e.Message,
                        StackTrace = e.StackTrace,
                        StoryName = "ManagementProject/Stories/Edit(Post)",
                        Parameters = new JavaScriptSerializer().Serialize(model)
                    });

                }
            }
            return PartialView("EditPartial", model);
        }
        [HttpPost]
        public ActionResult Delete(int id, int projectId)
        {
            try
            {
                StoriesLogic.DeleteStory(id);
                StoriesFilter story = StoriesFilterSession;
                story.AllStories = StoriesLogic.GetStoriesList(projectId);
                StoriesFilterSession = story;
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagementProject/Stories/Delete",
                    Parameters = "id= " + id + "& projectId= " + projectId
                });
            }
            return RedirectToAction("Index", new { ProjectId = projectId });
        }
        [HttpPost]
        public ActionResult DeleteAll(int projectId)
        {
            try
            {
                StoriesFilter story = StoriesFilterSession;
                List<StoriesDetails> stories = new List<StoriesDetails>();
                stories = StoriesLogic.GetFilteredStoriesList(StoriesFilterSession);
                StoriesLogic.DeleteStoriesList(stories);
                story.AllStories = StoriesLogic.GetStoriesList(projectId);
                StoriesFilterSession = story;
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagementProject/Stories/Delete",
                    Parameters = "projectId= " + projectId
                });
            }
            return RedirectToAction("Index", new { ProjectId = projectId });
        }
        public ActionResult GetStoriesInProject(int projectId, int? editableStoryId)
        {
            var storyId = editableStoryId ?? 0;
            List<Story> model = new List<Story>();

            try
            {
                model = StoriesLogic.GetStoriesInProject(projectId);
                if (storyId != 0)
                {
                    Story editableStory = StoriesLogic.GetStoryById(storyId);
                    model.Add(editableStory);
                    model = model.OrderBy(s => s.Name).ToList();
                }
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagementProject/Stories/GetStoriesInProject",
                    Parameters = "projectId= " + projectId + "& editableStoryId=" + editableStoryId
                });
            }
            return PartialView("DDLStoriesInProject", model);
        }
        public void FinishCodeReview(int storyId, int projectId)
        {
            try
            {
                StoriesLogic.FinishCodeReview(storyId);
                StoriesDetails s = StoriesFilterSession.AllStories.FirstOrDefault(st => st.Id == storyId);
                // SetFilterStorySession();
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagementProject/Stories/FinishCodeReview",
                    Parameters = " storyId=" + storyId + "& projectId = " + projectId
                });
            }

        }
        public void SprintChecked(int sprintId)
        {
            StoriesFilter model = StoriesFilterSession;
            try
            {
                model.sprintId = sprintId;
                StoriesFilterSession = model;
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagementProject/Stories/SprintChecked",
                    Parameters = "sprintId= " + sprintId
                });
            }

        }
        public void OrderedByNameChecked()
        {
            StoriesFilter model = StoriesFilterSession;
            model.OrderedByName = !StoriesFilterSession.OrderedByName;
            StoriesFilterSession = model;
        }
        public void ReviewedChecked()
        {
            StoriesFilter model = StoriesFilterSession;
            model.Reviewed = !StoriesFilterSession.Reviewed;
            StoriesFilterSession = model;
        }
        public void SearchTextChanged(string searchTxt)
        {
            StoriesFilter model = StoriesFilterSession;
            model.SearchText = searchTxt;
            StoriesFilterSession = model;

        }

    }
}