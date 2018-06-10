using DataMapping.Entities;
using System.Collections.Generic;
using System.Linq;
using System;


namespace DataAccess.Repositories
{
    public class ItemsRepositories
    {
        public static List<Item> GetSprintItems(int sprintId)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                var q = from sprint in db.Sprints
                        join
                        sprintItem in db.SprintItems on sprint.Id equals sprintItem.SprintId
                        join item in db.Items on sprintItem.ItemId equals item.Id
                        select item;
                return q.ToList();
            }
        }
        public static List<Item> GetStoryItems(int storyId)
        {
            using(ManagementSystemEntities db=new ManagementSystemEntities())
            {
                return db.Items.Where(x => x.StoryId == storyId && x.IsDeleted == false).ToList();
            }
        }
        public static Item GetItemById(int id)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                return db.Items.FirstOrDefault(x => x.Id == id);
            }
        }

        public static bool IsItemInSprint(int itemId, int sprintId)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                var q = from sprint in db.Sprints.Where(x=>x.Id== sprintId && x.IsDeleted==false)
                        join
                        sprintItem in db.SprintItems.Where(x=>x.IsDeleted==false) on sprint.Id equals sprintItem.SprintId
                        join item in db.Items.Where(x=>x.Id== itemId && x.IsDeleted==false) on sprintItem.ItemId equals item.Id
                        select item;
                return q.ToList().Count > 0;
            }
        }
        public static Item InsertNewItem(Item item)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                db.Items.Add(item);
                db.SaveChanges();
                return item;
            }
        }
        public static void InsertSprintItem(SprintItem sprintItem)
        {
            using(ManagementSystemEntities db=new ManagementSystemEntities())
            {
                db.SprintItems.Add(sprintItem);
                db.SaveChanges();
            }
        }
        public static void UpdateItem(Item item)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                var q = db.Items.FirstOrDefault(x => x.Id == item.Id);
                q.ItemStatu = item.ItemStatu;
                q.ItemType = item.ItemType;
                q.Name = item.Name;
                q.TypeId = item.TypeId;
                q.StatusId = item.StatusId;
                q.StoryId = item.StoryId;
                db.SaveChanges();
            }
        }
        public static void DeleteItem(int id)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                var q = db.Items.FirstOrDefault(x => x.Id == id);
                q.IsDeleted = true;
                db.SaveChanges();
            }
        }

        public static void AddItemsToSprint(List<Item> sprintItems, int sprintId)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                foreach (Item item in sprintItems)
                {
                    var oldItem = db.SprintItems.FirstOrDefault(x => x.ItemId == item.Id && x.SprintId == sprintId);
                    if (oldItem != null)
                    {
                        oldItem.IsDeleted = item.IsDeleted;
                    }
                    else
                    {
                        if (!item.IsDeleted)
                        {
                            db.SprintItems.Add(new SprintItem
                            {
                                ItemId = item.Id,
                                SprintId = sprintId
                            });
                        }
                    }
                }
                db.SaveChanges();
            }
        }
    }
}
