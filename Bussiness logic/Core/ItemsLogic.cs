using BusinessLogic.Helpers;
using BusinessLogic.Model;
using DataAccess.Repositories;
using DataMapping.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Bussinesslogic.Core
{
    public class ItemsLogic
    {
        public static List<Item> GetSprintItems(int sprintId,string searchTxt)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                if (searchTxt != "")
                {
                    return ItemsRepositories.GetSprintItems(sprintId).Where(x => x.Name.ToLower().Contains(searchTxt.ToLower())).ToList();
                }
                return ItemsRepositories.GetSprintItems(sprintId);
            }
        }
      
        public static StoryItemsModel GetStoryItemsModel(int storyId, int sprintId)
        {
            StoryItemsModel model = new StoryItemsModel
            {
                StoryId = storyId,
                StoryItemsList = ItemsRepositories.GetStoryItems(storyId),
                StoryName = StoriesRepositories.GetStoryById(storyId).Name
            };
            foreach(Item item in model.StoryItemsList)
            {
               item.IsIncludedInSelectedSprint= ItemsRepositories.IsItemInSprint(item.Id, sprintId);
            }
            return model;
        }
        public static Item GetItemById(int id)
        {
           return ItemsRepositories.GetItemById(id);
        }
        public static Item InsertNewItem(Item item)
        {
            return ItemsRepositories.InsertNewItem(item);
        }
        public static void InsertSprintItem(SprintItem sprintItem)
        {
            ItemsRepositories.InsertSprintItem(sprintItem);
        }
        public static void UpdateItem(Item item)
        {
            ItemsRepositories.UpdateItem(item);
        }
     

        public static void AddItemsToSprint(List<Item> sprintItems, int sprintId)
        {
            ItemsRepositories.AddItemsToSprint(sprintItems, sprintId);
        }
    }
}
