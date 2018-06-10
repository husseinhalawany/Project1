using DataAccess.Repositories;
using DataMapping.Entities;
using System;
using System.Collections.Generic;
using BusinessLogic.Helpers;
using DataMapping.Services;
namespace BusinessLogic.Core
{
    public class ProjectsLogic
    {
        public static Project GetProjectById(int id)
        {
            return ProjectsRepositories.GetProjectById(id);
        }
        public static void InsertNewProject(Project project)
        {
            ProjectsRepositories.InsertNewProject(project);
        }
        public static void UpdateProject(Project project)
        {
            DateTime myDate = DateTimeHelper.Today();
            ProjectsRepositories.UpdateProject(project, myDate);
        }
        public static void DeleteProject(int id)
        {
            ProjectsRepositories.DeleteProject(id);
        }

        public static List<Project> GetProjectsBySprints(int sprintId)
        {
            return ProjectsRepositories.GetProjectsBySprints(sprintId);
        }

        public static List<Project> GetProjectlist()
        {
            return ProjectsRepositories.GetProjectlist();
        }
        public static List<ProjectUserDetails> GetUserProjectList(int page, int userId)
        {

            int takeCount = Config.PageItemCount();
            int skipCount = page * takeCount;
            return ProjectsRepositories.GetProjectUserList(userId, skipCount, takeCount);

        }
        public static List<SprintProjectsDetails> GetProjectsBySprintId(int sprintId, string searchTxt)
        {
            return ProjectsRepositories.GetProjectsBySprintId(sprintId, searchTxt);
        }
        public static bool InsertProjectSprint(int sprintId, string projectName, int userId)
        {
            return ProjectsRepositories.InsertSprintProject(sprintId, new Project()
            {
                CreateDate = DateTimeHelper.Today(),
                UpdateDate = DateTimeHelper.Today(),
                CreatorId = userId,
                Name = projectName
            });
        }
        public static List<string> AutocompleteProjects(int sprintId, bool isInSprint)
        {
            if (isInSprint)
                return ProjectsRepositories.AutocompleteProjectsInSprint(sprintId);

            return ProjectsRepositories.AutocompleteProjectsNotInSprint(sprintId);
        }
    }
}