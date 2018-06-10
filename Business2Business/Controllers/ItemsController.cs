using BusinessLogic.Core;
using BusinessLogic.Helpers;
using BusinessLogic.Model;
using Bussinesslogic.Core;
using DataMapping.Entities;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ManagementProject.Controllers
{
    public class ItemsController : BaseController
    {
        public ActionResult SprintItems()
        {
            return View();
        }
        public ActionResult StoryItems(int storyId)
        {
            StoryItemsModel model = ItemsLogic.GetStoryItemsModel(storyId, StoriesFilterSession.sprintId);
            StoriesFilterSession.SprintItems = model.StoryItemsList.Where(x => x.IsIncludedInSelectedSprint == true).ToList();
            return View(model);

        }
        public ActionResult SprintItemsList(string searchTxt="")
        {
            List<Item> sprintItems = ItemsLogic.GetSprintItems(StoriesFilterSession.sprintId,searchTxt);
            return PartialView("SprintItemsListPartial", sprintItems);

        }
        public ActionResult Create(int storyId)
        {
            Item model = new Item
            {
                StoryId = storyId,
                IsIncludedInSelectedSprint = true
            };
            return PartialView("Create",model);
        }
        [HttpPost]
        public ActionResult Create(Item model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.CreateDate = DateTimeHelper.Today();
                    Item item = ItemsLogic.InsertNewItem(model);
                    ItemsLogic.InsertSprintItem(new SprintItem
                    {
                        ItemId = item.Id,
                        SprintId = StoriesFilterSession.sprintId
                    });
                    return PartialView("JavascriptRedirect", new JavascriptRedirectModel("/Items/StoryItems?storyId="+model.StoryId));
                }
                catch (Exception e)
                {
                    LogsLogic.InsertLog(new Log()
                    {
                        Message = e.Message,
                        StackTrace = e.StackTrace,
                        StoryName = "ManagementProject/Items/Create(Post)",
                        Parameters = new JavaScriptSerializer().Serialize(model)
                    });
                    return PartialView(model);
                }
            }
            return PartialView(model);
        }
        public ActionResult Edit(int id)
        {
            Item model = new Item();
            try
            {
                model = ItemsLogic.GetItemById(id);
                model.Succeeded = true;
            }
            catch (Exception e)
            {
                model = new Item
                {
                    Succeeded = false,
                    ErrorMessage = e.Message,
                };
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagementProject/Items/Edit(Get)",
                    Parameters = "id=" + id
                });
            }
            return PartialView("Edit", model);
        }
        [HttpPost]
        public ActionResult Edit(Item model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ItemsLogic.UpdateItem(model);
                    return PartialView("JavascriptRedirect", new JavascriptRedirectModel("/Items/StoryItems?storyId=" + model.StoryId));
                }
                catch (Exception e)
                {
                    LogsLogic.InsertLog(new Log()
                    {
                        Message = e.Message,
                        StackTrace = e.StackTrace,
                        StoryName = "ManagementProject/Items/Edit(Post)",
                        Parameters = new JavaScriptSerializer().Serialize(model)
                    });
                    return PartialView(model);
                }
            }
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                //ItemsLogic.DeleteItem(id);
            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "ManagmentProject/Items/Delete",
                    Parameters = "id=" + id
                });
            }
            return RedirectToAction("StorySprint", "Stories", new { projectId = StoriesFilterSession.projectId });
        }
        [HttpGet]
        public void AddOrRemoveItemInSprint(int itemId)
        {

            Item item = StoriesFilterSession.SprintItems.FirstOrDefault(x => x.Id == itemId);
            if (item!=null)
            {
                item.IsDeleted=!item.IsDeleted;
            }
            else
            {
                StoriesFilterSession.SprintItems.Add(ItemsLogic.GetItemById(itemId));
            }
        }
        public ActionResult AddItemsToSprint()
        {
            ItemsLogic.AddItemsToSprint(StoriesFilterSession.SprintItems, StoriesFilterSession.sprintId);
            return RedirectToAction("StorySprint", "Stories", new { projectId = StoriesFilterSession.projectId });
        }

        
    }
}
