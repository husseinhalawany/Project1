using System;
using System.Collections.Generic;
using System.Linq;
using DataMapping.Entities;
namespace DataAccess.Repositories
{
    public class SprintsRepositories
    {
        public static List<Sprint> GetAllSprint()
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                return db.Sprints.Where(x => !x.IsDeleted).ToList();
            }
        }
        public static Sprint GetSprintById(int id)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                return db.Sprints.FirstOrDefault(p => p.Id == id && !p.IsDeleted);
            }
        }
        public static string GetSprintName(int id)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                return db.Sprints.FirstOrDefault(x => x.Id == id && !x.IsDeleted).Name;
            }
        }
        public static void InsertNewSprint(Sprint sprint)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                db.Sprints.Add(sprint);
                db.SaveChanges();
            }
        }
        public static int GetCurrentSprint(int projectId)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                SprintProject item = db.SprintProjects
                    .FirstOrDefault(x => x.ProjectId == projectId);
                if (item == null)
                    return 0;
                return Convert.ToInt32(item.SprintId);
            }
        }
        public static Sprint GetCurrentSprint()
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                return db.Sprints.FirstOrDefault(x => x.CurrentSprint && !x.IsDeleted);
            }
        }
        public static bool IsCurrentSprintExist()
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                return db.Sprints.Any(x => x.CurrentSprint && !x.IsDeleted);
            }
        }
        public static bool IsFutureSprintExist()
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                return db.Sprints.Any(x => x.FutureSprint && !x.IsDeleted);
            }
        }
        public static void UpdateSprint(Sprint Sprint)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                var q = db.Sprints.FirstOrDefault(p => p.Id == Sprint.Id);
                if (q != null)
                {
                    q.Name = Sprint.Name;
                    db.SaveChanges();
                }
            }
        }
        public static void DeleteSprint(int sprintId, int projectId)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                List<SprintStory> storiesSprints =
                    (from s in db.Stories
                     where (s.ProjectId == projectId && s.IsDeleted == false)
                     join sprint_story in db.SprintStories
                        on s.Id equals sprint_story.StoryId
                     join sprint in db.Sprints
                        on sprint_story.SprintId equals sprint.Id
                     where sprint.Id == sprintId && sprint.IsDeleted == false

                     select sprint_story
                     )
                     .ToList();

                foreach (var item in storiesSprints)
                {
                    db.SprintStories.Remove(item);
                }
                var sprints = db.Sprints
                    .FirstOrDefault(x => x.Id == sprintId);
                if (sprints != null)
                {
                    sprints.IsDeleted = true;
                }
                db.SaveChanges();
            }
        }
        public static void SetCurrent()
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                Sprint future = db.Sprints.FirstOrDefault(p => p.FutureSprint &&
                            p.IsDeleted == false);
                Sprint cur = db.Sprints.FirstOrDefault(x => x.CurrentSprint == true
                            && x.IsDeleted == false);

                if (future != null && cur != null)
                {
                    future.CurrentSprint = true;
                    future.FutureSprint = false;

                    cur.CurrentSprint = false;
                    cur.PreviousSprint = true;
                    db.SaveChanges();
                }
            }
        }

        public static void InsertNewSprintProject(SprintProject sprintProject)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                db.SprintProjects.Add(sprintProject);
                db.SaveChanges();
            }
        }
    }
}