using DataMapping.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class OccasionVacationsRepositories
    {
        public static List<OccasionVacation> GetOccasionVacationsList(int skipCount, int takeCount, int vacationYearId, string startDate)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                var q = db.OccasionVacations
                    .Where(a => a.IsDeleted == false && a.VacationYearId == vacationYearId)
                    .Skip(skipCount)
                    .Take(takeCount)
                    .ToList();

                q.ToList()
                    .ForEach(a => 
                    {
                        a.StartDate = startDate + "/01/01";
                        a.EndDate = startDate + "/12/31";
                    });
                return q.ToList();
            }
        }
        public static List<OccasionVacation> GetRequestOccesionVacation(DateTime startDate, DateTime endDate)
        {

            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                return db.OccasionVacations.Where(a => a.IsDeleted == false && a.Date.Date >= startDate.Date && a.Date.Date <= endDate.Date).ToList();
            }
        }
        public static bool InsertNewOccasionVacation(OccasionVacation occasionVacation)
        {
            try
            {
                using (ManagementSystemEntities db = new ManagementSystemEntities())
                {
                    db.OccasionVacations.Add(occasionVacation);
                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        public static bool IsDateValid(DateTime date, int? vacationYearId)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                VacationYear vacationYear = db.VacationYears.FirstOrDefault(a => a.IsDeleted == false && a.Id == vacationYearId);
                if (vacationYear != null)
                {
                    if (date >= vacationYear.StartDate && date <= vacationYear.EndDate)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
        public static OccasionVacation GetOccasionVacationById(int id)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                return db.OccasionVacations.FirstOrDefault(a => a.Id == id);
            }
        }
        public static bool UpdateOccasionVacation(OccasionVacation occasionVacation)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                OccasionVacation occasionVacationItem = db.OccasionVacations.FirstOrDefault(a => a.Id == occasionVacation.Id);
                if (occasionVacationItem != null)
                {
                    occasionVacationItem.Name = occasionVacation.Name;
                    occasionVacationItem.Date = occasionVacation.Date;
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
        }
        public static bool DeleteOccasionVacation(int id)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                OccasionVacation occasionVacationItem = db.OccasionVacations.FirstOrDefault(a => a.Id == id);
                if (occasionVacationItem != null)
                {
                    occasionVacationItem.IsDeleted = true;
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
        }
        public static int GetCountOfVacationDaysBetweenTowDates(DateTime startDate, DateTime endDate)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                return db.OccasionVacations.Where(o => o.Date >= startDate && o.Date <= endDate).Count();
            }
        }
    }
}
