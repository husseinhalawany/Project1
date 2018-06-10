using DataMapping.Entities;
using DataMapping.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Repositories
{
    public class StoriesRepositories
    {
        public static List<Story> GetAllStoriesList(int projectId)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                return db.Stories.Where(story => story.ProjectId == projectId
                && story.IsDeleted == false).ToList();
            }
        }
        public static StoriesDetails GetStoryDetails(int storyId, int projectId)
        {

            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                StoriesDetails model = new StoriesDetails();
                var result = (from story in db.Stories.Where(story => story.Id == storyId && story.ProjectId == projectId && story.IsDeleted == false)
                              select new StoriesDetails
                              {
                                  Id = story.Id,
                                  Name = story.Name,
                                  ProjectId = story.ProjectId
                              });
                model = result.FirstOrDefault();
                if (result != null)
                {
                    model.Sprints = (from s in db.Stories
                                     where (s.ProjectId == projectId && s.IsDeleted == false && s.Id == storyId)
                                     join sprint_story in db.SprintStories on s.Id equals sprint_story.StoryId
                                     join sprint in db.Sprints on sprint_story.SprintId equals sprint.Id
                                     select sprint).ToList();

                }

                return model;

            }
        }
        public static bool InsertStorySprint(int projectId, int sprintId,string storyName,int userId)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                if(db.Stories.Where(x=>x.Name==storyName).Count()==0)
                {
                    Story story = new Story()
                    {
                        CreateDate=DateTime.Now,
                        CreatorId=userId,
                        Name=storyName,
                        ProjectId=projectId
                    };
                    db.Stories.Add(story);
                    db.SaveChanges();
                    SprintStory sprintStory = new SprintStory()
                    {
                        SprintId=sprintId,
                        StoryId=story.Id
                    };
                    db.SprintStories.Add(sprintStory);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
        }
        public static List<StoriesDetails> GetSprintStoriesListAndItemCount(int sprintId)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {

                var stories = (from s in db.Stories
                               where (s.IsDeleted == false)
                               join sprint_story in db.SprintStories on s.Id equals sprint_story.StoryId
                               join sprint in db.Sprints on sprint_story.SprintId equals sprint.Id
                               where sprint.Id == sprintId && sprint.IsDeleted == false
                               select new StoriesDetails
                               {
                                   Id = s.Id,
                                   Name = s.Name,
                                   ProjectId = s.ProjectId
                               }).ToList();

                foreach (var item in stories)
                {

                    item.ContainItem = db.SprintItems.Where(x => x.SprintId == sprintId && x.ItemId == db.Items.FirstOrDefault(y => y.StoryId ==item.Id).Id).Count();
                    stories.FirstOrDefault(x => x.Id == item.Id).ContainItem = item.ContainItem;
                }
                return stories;
            }
        }
        public static List<StoriesDetails> GetStoriesListAndItemCount(int projectId, int sprintId)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {

                var stories = (from s in db.Stories
                               where (s.ProjectId == projectId && s.IsDeleted == false)
                               join sprint_story in db.SprintStories on s.Id equals sprint_story.StoryId
                               join sprint in db.Sprints on sprint_story.SprintId equals sprint.Id
                               where sprint.Id == sprintId && sprint.IsDeleted == false
                               select new StoriesDetails
                               {
                                   Id = s.Id,
                                   Name = s.Name,
                                   ProjectId = projectId
                               }).ToList();

                foreach (var item in stories)
                {
                    item.ContainItem = db.SprintItems.Where(x => x.SprintId == sprintId && x.ItemId == db.Items.FirstOrDefault(y => y.StoryId == item.Id).Id).Count();
                    stories.FirstOrDefault(x => x.Id == item.Id).ContainItem = item.ContainItem;
                }
                return stories;
            }
        }
        public static List<StoriesDetails> GetStoriesList(int projectId)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {

                var result = (from story in db.Stories.Where(story => story.ProjectId == projectId && story.IsDeleted == false)
                              select new StoriesDetails
                              {
                                  Id = story.Id,
                                  Name = story.Name,
                                  ProjectId = story.ProjectId
                              }).ToList();

                int i = 0;
                foreach (var item in result)
                {
                    result[i].Sprints = (from s in db.Stories
                                         where (s.ProjectId == projectId && s.IsDeleted == false && s.Id == result[i].Id)
                                         join sprint_story in db.SprintStories on s.Id equals sprint_story.StoryId
                                         join sprint in db.Sprints on sprint_story.SprintId equals sprint.Id
                                         select sprint).ToList();
                    i++;
                }
                return result;

            }
        }

        public static List<string> AutocompleteStoriesForAdd(int projectId)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                var Stories = from s in db.Stories
                              where (s.ProjectId == projectId && s.IsDeleted != true)
                              select s.Name;
                return Stories.ToList();
            }
        }

        public static List<string> AutocompleteStories(int projectId, int sprintId)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                var Stories = from s in db.Stories
                              where (s.ProjectId == projectId && s.IsDeleted == false)
                              join sprint_story in db.SprintStories on s.Id equals sprint_story.StoryId
                              join sprint in db.Sprints on sprint_story.SprintId equals sprint.Id
                              where sprint.Id == sprintId && sprint.IsDeleted == false
                              select s.Name;
                return Stories.ToList();
            }
        }
        public static Story GetStoryById(int id)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                var story = db.Stories.SingleOrDefault(x => x.Id == id && x.IsDeleted == false);
                return story;
            }
        }
        public static Story CheckIfStoryLinkedToSprint(Story story)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                var q = (from s in db.Stories
                         where (s.ProjectId == story.ProjectId && s.IsDeleted != true)
                         join sprint_story in db.SprintStories
                             on s.Id equals sprint_story.StoryId
                         join sprint in db.Sprints
                             on sprint_story.SprintId equals sprint.Id

                         where sprint.Id == story.sprintId &&
                             s.Id == story.Id &&
                             sprint.IsDeleted == false
                         select story)
                        .FirstOrDefault();

                return q;
            }
        }
        public static Story CheckOfExistingStory(Story story)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                var q = db.Stories
                        .Where(x => x.ProjectId == story.ProjectId &&
                            x.Name == story.Name &&
                            x.IsDeleted == false)
                        .FirstOrDefault();
                return q;
            }
        }
        public static void InsertSprintToStory(int storyId, int sprintId)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                SprintStory sprintStory = new SprintStory() { SprintId = sprintId, StoryId = storyId };
                db.SprintStories.Add(sprintStory);
                db.SaveChanges();
            }
        }
        public static StoriesDetails InsertNewStory(Story story)
        {
            StoriesDetails model = new StoriesDetails();
            if (story.Exist)
            {
                var q = CheckOfExistingStory(story);
                if (CheckIfStoryLinkedToSprint(story) == null)
                {
                    InsertSprintToStory(q.Id, story.sprintId);
                    model = GetStoryDetails(q.Id, story.ProjectId);

                }
            }
            else
            {
                if (CheckOfExistingStory(story) != null)
                {
                    return model;
                }
                else
                {
                    using (ManagementSystemEntities db = new ManagementSystemEntities())
                    {
                        db.Stories.Add(story);
                        db.SaveChanges();
                        int id = db.Stories.OrderByDescending(x => x.Id).First().Id;
                        SprintStory sprint_story = new SprintStory() { SprintId = story.sprintId, StoryId = id };
                        db.SprintStories.Add(sprint_story);
                        db.SaveChanges();
                        model = GetStoryDetails(id, story.ProjectId);
                    }
                }
            }
            return model;
        }
        public static void UpdateStory(Story story, DateTime today)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                var updatedSyory = db.Stories.SingleOrDefault(a => a.Id == story.Id);
                if (updatedSyory != null)
                {
                    updatedSyory.Name = story.Name;
                    db.SaveChanges();
                }
            }
        }
        public static void DeleteStory(int id)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                var q = db.Stories.SingleOrDefault(a => a.Id == id);
                if (q != null)
                {
                    q.IsDeleted = true;
                    db.SaveChanges();
                }
            }
        }
        public static void DeleteAllStory(List<int> storiesIds)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                foreach (var id in storiesIds)
                {
                    var q = db.Stories.FirstOrDefault(a => a.Id == id);
                    if (q != null)
                    {
                        q.IsDeleted = true;
                        db.SaveChanges();
                    }
                }
            }
        }
        public static void FinishCodeReview(int id)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                var q = db.Stories.SingleOrDefault(a => a.Id == id);
                if (q != null)
                {
                    db.SaveChanges();
                }
            }
        }
    }
}
