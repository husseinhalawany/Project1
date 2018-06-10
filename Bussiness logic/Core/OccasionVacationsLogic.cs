using BusinessLogic.Helpers;
using DataAccess.Repositories;
using DataMapping.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MangamentProject.Core
{
    public class OccasionVacationsLogic
    {
        public static List<OccasionVacation> GetOccasionVacationsList(int page, int vacationYearId,string startDate)
        {
            int takeCount = Config.PageItemCount();
            int skipCount = page * takeCount;
            return OccasionVacationsRepositories.GetOccasionVacationsList(skipCount, takeCount, vacationYearId, startDate).ToList();
        }
        public static bool InsertNewOccasionVacation(OccasionVacation occasionVacation)
        {
            if (!OccasionVacationsRepositories.IsDateValid(occasionVacation.Date, occasionVacation.VacationYearId))
            {
                occasionVacation.ErrorMessage = "Invalid Date Please Select Date During selected Year Period!";
                return false;
            }
            if (string.IsNullOrWhiteSpace(occasionVacation.Name))
            {
                occasionVacation.ErrorMessage = "Name is Requerid!";
                return false;
            }
            occasionVacation.CreateDate = DateTime.Now;
           return  OccasionVacationsRepositories.InsertNewOccasionVacation(occasionVacation);
        }
        public static OccasionVacation GetOccasionVacationById(int id)
        {
            return OccasionVacationsRepositories.GetOccasionVacationById(id);
        }
        public static bool UpdateOccasionVacation(OccasionVacation occasionVacation)
        {
            if (!OccasionVacationsRepositories.IsDateValid(occasionVacation.Date, occasionVacation.VacationYearId))
            {
                occasionVacation.ErrorMessage = "Invalid Date Please Select Date During selected Year Period!";
                return false;
            }
            if (string.IsNullOrWhiteSpace(occasionVacation.Name))
            {
                occasionVacation.ErrorMessage = "Name is Requerid!";
                return false;
            }
            
            return OccasionVacationsRepositories.UpdateOccasionVacation(occasionVacation);
        }
        public static bool DeleteOccasionVacation(int id)
        {
            return OccasionVacationsRepositories.DeleteOccasionVacation(id);
        }
        public static int GetCountOfVacationDaysBetweenTowDates(DateTime startDate, DateTime endDate)
        {
            return OccasionVacationsRepositories.GetCountOfVacationDaysBetweenTowDates(startDate, endDate);
        }
     }
}
