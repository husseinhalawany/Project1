using BusinessLogic.Helpers;
using DataAccess.Repositories;
using DataMapping.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Model;
using DataMapping.Enums;

namespace MangamentProject.Core
{
    public class VacationYearsLogic
    {
        public static VacationYear GetCurrentVacationYear()
        {
            VacationYear currentVacationYear = VacationYearsRepositories.GetCurrentVacationYear();
            if (currentVacationYear == null)
            {
                currentVacationYear = new VacationYear();
                currentVacationYear.CreateDate = DateTime.Now;
                DateTime currentYearDateTime = DateTime.Now;
                currentVacationYear.YearName = currentYearDateTime.Year.ToString();
                currentVacationYear.StartDate = new DateTime(currentYearDateTime.Year, 1, 1);
                currentVacationYear.EndDate = new DateTime(currentYearDateTime.Year, 12, 31);
                currentVacationYear.IsCurrent = true;
                VacationYearsRepositories.InsertNewVacationYear(currentVacationYear);
            }
            currentVacationYear.VacationYearStatusId = (int)Y.Current;
            return currentVacationYear;
        }
        public static VacationYear GetNextVacationYear()
        {
            VacationYear nextVacationYear = VacationYearsRepositories.GetNextVacationYear();
            if (nextVacationYear == null)
            {
                nextVacationYear = new VacationYear();
                nextVacationYear.CreateDate = DateTime.Now;
                DateTime nextYearDateTime = DateTime.Now.AddYears(1);
                nextVacationYear.YearName = nextYearDateTime.Year.ToString();
                nextVacationYear.StartDate = new DateTime(nextYearDateTime.Year, 1, 1);
                nextVacationYear.EndDate = new DateTime(nextYearDateTime.Year, 12, 31);
                nextVacationYear.IsCurrent = false;
                VacationYearsRepositories.InsertNewVacationYear(nextVacationYear);
            }
            nextVacationYear.VacationYearStatusId = (int)Y.Next;
            return nextVacationYear;
        }
        public static List<VacationYear> GetPreviousVacationYearsList()
        {
            return VacationYearsRepositories.GetPreviousVacationYearsList();
        }
        public static VacationYear GetVacationYear(int? vacationId)
        {
            return VacationYearsRepositories.GetVacationYear(vacationId);
        }
        public static void DeleteVacationYear(int id)
        {
            VacationYearsRepositories.DeleteVacationYear(id);
        }
    }
}
