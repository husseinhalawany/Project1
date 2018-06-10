using System;
using System.Collections.Generic;
using DataMapping.Entities;
using DataAccess.Repositories;
using BusinessLogic.Model;
using BusinessLogic.Helpers;

namespace BusinessLogic.Core
{
    public class SprintsLogic
    {
        public static List<Sprint> GetAllSprints()
        {
            return SprintsRepositories.GetAllSprint();
        }

      

       
        public static int GetCurrentSprint(int projectId)
        {
            return SprintsRepositories.GetCurrentSprint(projectId);

        }
       
        public static void InsertNewSprint(Sprint sprint)
        {
            if (!SprintValidToCreate(sprint))
                return;

            sprint.CreateDate = DateTimeHelper.Today();
            if (sprint.CurrentSprint)
            {
                sprint.StartDate = sprint.CreateDate;
                if (sprint.StartDate.DayOfWeek != DayOfWeek.Sunday)
                    sprint.StartDate = sprint.StartDate.AddDays(7 - (int)sprint.StartDate.DayOfWeek);
            }
            else if (sprint.FutureSprint)
            {
                Sprint cur = SprintsRepositories.GetCurrentSprint();
                sprint.StartDate = cur.EndDate.AddDays(7 - (int)cur.EndDate.DayOfWeek);
            }

            int weekCount = sprint.IsOneWeek ? 1 : 2;
            sprint.EndDate = sprint.StartDate.AddDays((7 * weekCount) - 3);
            sprint.Name = "Sprint" + sprint.StartDate.ToString("yyyyMMdd");

            SprintsRepositories.InsertNewSprint(sprint);
        }
        private static bool SprintValidToCreate(Sprint sprint)
        {
            if (SprintsRepositories.IsCurrentSprintExist())
                sprint.FutureSprint = true;
            else
                sprint.CurrentSprint = true;

            if (SprintsRepositories.IsFutureSprintExist())
                return false;

            return true;
        }
      
      
      

        public static void SetCurrent()
        {
            SprintsRepositories.SetCurrent();

        }


        public static bool InsertNewSprintProject(int sprintId, string projectName)
        {
            Project project = ProjectsRepositories.GetProjectByName(projectName);
            if (project != null)
            {
                SprintsRepositories.InsertNewSprintProject(new SprintProject
                {
                    SprintId = sprintId,
                    ProjectId = project.Id
                });
                return true;
            }
            return false;
        }
    }
}