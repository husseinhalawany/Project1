using ManagementProject.Controllers;
using BusinessLogic.Core;
using BusinessLogic.Model;
using DataMapping.Entities;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using BusinessLogic.Helpers;
using System.Web.Script.Serialization;
using DataMapping.Services;
using DataMapping.Enums;

namespace MangamentProject.Controllers
{
    [Authorize]
    public class SuggestionsController : BaseController
    {
        public ActionResult Index()
        {

            return View();
        }
        public ActionResult SuggestionsList(int? pageNo)
        {
            var page = pageNo ?? 0;
            List<SuggestionUserDetails> model = new List<SuggestionUserDetails>();
            try
            {
                model = SuggestionsLogic.GetSuggestionsList(page);
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagementProject/Suggestions/SuggestionsList",
                    Parameters =  "& pageNo=" + page
                });
            }
           
            return PartialView( model);
        }
        public ActionResult Create()
        {
             
            return PartialView("CreatePartial", new Suggestion());
        }
        [HttpPost]
        public ActionResult Create(Suggestion suggestion)
        {
           
            try
            {
                    suggestion.SuggestByUserId = SessionData.UserId;
                    SuggestionsLogic.InsertNewSuggestion(suggestion);
                if (SessionData.UserRole == UserRoles.Employee)
                {
                    return PartialView("JavascriptRedirect", new JavascriptRedirectModel("/Employees/Index"));
                }
                else
                {
                    return PartialView("JavascriptRedirect", new JavascriptRedirectModel("/Suggestions/Index"));
                }



            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagementProject/Suggestions/Create(Post)",
                    Parameters = new JavaScriptSerializer().Serialize(suggestion)
                });
                return PartialView("CreatePartial", suggestion);

            }

        }
        public ActionResult Edit(int id)
        {
            CreateSuggestionModel model = new CreateSuggestionModel();
            try
            {
                model = SuggestionsLogic.GetEditSuggestionModel(id);
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagementProject/Suggestions/Edit",
                    Parameters = "id=" + id
                });
                return RedirectToAction("GeneralError", "Error", new { ErrorMessage = Error.ServerNotRespond });
            }

            return PartialView("EditPartial", model);
        }
        [HttpPost]
        public ActionResult Edit(CreateSuggestionModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    SuggestionsLogic.UpdateSuggestion(model.Suggestion);
                    return PartialView("JavascriptRedirect", new JavascriptRedirectModel("/Suggestions/Index" ));
                }
                CreateSuggestionModel SuggestModel = SuggestionsLogic.UpdateProjectsList(model);
                return PartialView("EditPartial", SuggestModel);
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagementProject/Suggestions/Edit(Post)",
                    Parameters = new JavaScriptSerializer().Serialize(model)
                });
                return PartialView("EditPartial", model);
            }

        }
        public ActionResult Details(int id)
        {
            Suggestion model = new Suggestion();
            try
            {
                model = SuggestionsLogic.GetSuggestionById(id);
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagementProject/Suggestions/Details",
                    Parameters = "id=" + id
                });
                return RedirectToAction("GeneralError", "Error", new { ErrorMessage = Error.ServerNotRespond });
            }

            return PartialView("DetailsPartial", model);
        }
        public ActionResult Delete(int id)
        {
            try
            {
                SuggestionsLogic.DeleteSuggestion(id);
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagementProject/Suggestions/Delete",
                    Parameters =  "id=" + id
                });
                return RedirectToAction("GeneralError", "Error", new { ErrorMessage = Error.ServerNotRespond });
            }
            return RedirectToAction("Index");
        }
        [AllowAnonymous]
        public ActionResult Suggest()
        {
            CreateSuggestionModel model = new CreateSuggestionModel();
            try
            {
                model = SuggestionsLogic.GetCreateSuggestionModel();
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagementProject/Suggestions/Suggest",
                });
                return RedirectToAction("GeneralError", "Error", new { ErrorMessage = Error.ServerNotRespond });
            }

            return PartialView("SuggestPartial", model);
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Suggest(CreateSuggestionModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.Suggestion.SuggestByUserId = SessionData.UserId;
                    SuggestionsLogic.InsertNewSuggestion(model.Suggestion);
                    return PartialView("JavascriptRedirect", new JavascriptRedirectModel("/Developer/Index"));
                }
                CreateSuggestionModel SuggestionModel = SuggestionsLogic.UpdateProjectsList(model);
                return PartialView("SuggestPartial", SuggestionModel);
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagementProject/Suggestions/Suggest(Post)",
                    Parameters = new JavaScriptSerializer().Serialize(model)
                });
                return PartialView("SuggestPartial", model);
            }

        }
       
        
    }
}