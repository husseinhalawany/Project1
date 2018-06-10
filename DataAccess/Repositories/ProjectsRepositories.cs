using DataMapping.Entities;
using DataMapping.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Repositories
{
    public class ProjectsRepositories
    {
        public static List<Project> GetAllProjects()
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                return db.Projects.Where(a => a.IsDeleted == false).ToList();
            }
        }
        public static Project GetProjectById(int id)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                return db.Projects.SingleOrDefault(a => a.Id == id && a.IsDeleted == false);
            }
        }
        public static Project GetProjectByName(string projectName)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                return db.Projects.FirstOrDefault(a => a.Name == projectName && a.IsDeleted == false);
            }
        }
        public static void InsertNewProject(Project project)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                db.Projects.Add(project);
                db.SaveChanges();
            }
        }

        public static List<Project> GetProjectsBySprints(int sprintId)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {

                var projects= (from sprint in db.Sprints
                        join sprint_project in db.SprintProjects on sprint.Id equals sprint_project.SprintId
                        join project in db.Projects on sprint_project.ProjectId equals project.Id
                        where sprint.Id == sprintId && sprint.IsDeleted == false && project.IsDeleted == false
                        select project).ToList();

                for(int i =0;i< projects.Count();i++)
                {
                    var stories = (from sprint in db.Sprints
                                   join sprint_story in db.SprintStories on sprint.Id equals sprint_story.SprintId
                                   join story in db.Stories on sprint_story.StoryId equals story.Id
                                   where sprint.Id == sprintId && sprint.IsDeleted == false && story.IsDeleted == false
                                   && story.ProjectId== projects[i].Id
                                   select story).ToList();
                    projects[i].NumberOfStories = stories.Count();
                    projects[i].SprintId = sprintId;
                }
                return projects;
            }
        }

        public static void UpdateProject(Project project, DateTime myDate)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                var temp = db.Projects.SingleOrDefault(a => a.Id == project.Id);
                if (temp != null)
                {
                    temp.Name = project.Name;
                    temp.Descreption = project.Descreption;
                    temp.UpdateDate = myDate;
                    db.SaveChanges();
                }
            }
        }
        public static void DeleteProject(int id)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                var temp = db.Projects.SingleOrDefault(a => a.Id == id);
                if (temp != null)
                {
                    temp.IsDeleted = true;
                    db.SaveChanges();
                }
            }
        }
        public static List<Project> GetProjectlist()
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                return db.Projects.Where(x => x.IsDeleted == false).ToList();
            }
        }
        public static List<ProjectUserDetails> GetProjectUserList(int userId, int skipCount, int takeCount)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                var q = (from project in db.Projects.Where(p => p.IsDeleted == false)
                         join user in db.ProjectUsers.Where(x => x.UserId == userId)
                             on project.Id equals user.ProjectId
                         select new ProjectUserDetails()
                         {
                             Descreption = project.Descreption,
                             Name = project.Name
                         }).OrderBy(x => x.Name)
                        .Take(takeCount)
                        .Skip(skipCount)
                        .ToList();
                return q;
            }
        }
        public static List<SprintProjectsDetails> GetProjectsBySprintId(int sprintId, string searchTxt = "")
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                List<SprintProjectsDetails> sprintProjectList =
                    (from project in db.Projects
                     join sprintProjects in db.SprintProjects
                         on project.Id equals sprintProjects.ProjectId
                     //join sprintStory in db.SprintStories
                     //     on sprintProjects.SprintId equals sprintStory.SprintId
                     //join story in db.Stories
                     //   on sprintStory.StoryId equals story.Id

                     where sprintProjects.SprintId == sprintId &&
                        project.IsDeleted == false &&
                        project.Name.Contains(searchTxt)

                     select new SprintProjectsDetails
                     {
                         Project = project,
                         //IncludedStoriesCount = db.SprintStories.Where(sp => sp.SprintId == sprintId && sp.StoryId == db.Stories
                         //       .FirstOrDefault(st => st.ProjectId == project.Id).Id)
                         //   .Count()
                     })
                       .Distinct()
                       .ToList();
                for (int i = 0; i < sprintProjectList.Count; i++)
                {
                    var storiesInProject = db.Stories
                        .Where(y => y.ProjectId == sprintProjectList[i].Project.Id)
                        .ToList();

                    var storiesInSprint = db.SprintStories
                        .Where(x => x.SprintId == sprintId)
                        .Select(x => x.Story)
                        .ToList();

                    sprintProjectList[i].IncludedStoriesCount = storiesInSprint.Intersect(storiesInProject).Count();
                }
                return sprintProjectList;
            }
        }
        public static bool InsertSprintProject(int sprintId, Project project)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                if (!db.Stories.Any(x => x.Name == project.Name))
                {
                    db.Projects.Add(project);
                    db.SaveChanges();
                    SprintProject sprintProjecty = new SprintProject()
                    {
                        SprintId = sprintId,
                        ProjectId = project.Id
                    };
                    db.SprintProjects.Add(sprintProjecty);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
        }
        public static List<string> AutocompleteProjectsNotInSprint(int sprintId)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                var projectNameList = (from project in db.Projects

                                       where !(from p in db.Projects
                                               join sprintProject in db.SprintProjects
                                                 on p.Id equals sprintProject.ProjectId
                                               where sprintProject.SprintId == sprintId
                                               select p).Contains(project) &&

                                           project.IsDeleted == false
                                       select project.Name)
                                       .Distinct()
                                       .ToList();
                return projectNameList;
            }
        }
        public static List<string> AutocompleteProjectsInSprint(int sprintId)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                var projectNameList = (from p in db.Projects
                                       join sprintProject in db.SprintProjects
                                         on p.Id equals sprintProject.ProjectId

                                       where sprintProject.SprintId == sprintId &&
                                           p.IsDeleted == false
                                       select p.Name)
                                       .Distinct()
                                       .ToList();
                return projectNameList;
            }
        }
    }
}