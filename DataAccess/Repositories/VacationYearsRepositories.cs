using DataMapping.Entities;
using DataMapping.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class VacationYearsRepositories
    {
        public static VacationYear GetCurrentVacationYear()
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                DateTime currentYearDateTime = DateTime.Now;
                VacationYear vacationYear = db.VacationYears.FirstOrDefault(a => a.StartDate.Year == currentYearDateTime.Year && a.IsDeleted == false);
                if (vacationYear != null)
                    if (!vacationYear.IsCurrent)
                    {
                        vacationYear.IsCurrent = true;
                        db.SaveChanges();
                    }
                return vacationYear;
            }
        }
        public static VacationYear GetNextVacationYear()
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                DateTime nextYearDateTime = DateTime.Now.AddYears(1);
                VacationYear vacationYear = db.VacationYears
                    .FirstOrDefault(a => a.StartDate.Year == nextYearDateTime.Year &&
                    a.IsDeleted == false);

                return vacationYear;
            }
        }
        public static List<VacationYear> GetPreviousVacationYearsList()
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                DateTime currentYearDateTime = DateTime.Now;
                var q= db.VacationYears
                    .Where(a => a.StartDate.Year < currentYearDateTime.Year &&
                        a.IsDeleted == false)
                    .OrderByDescending(a => a.StartDate);

                q.ToList().ForEach(a=>a.VacationYearStatusId=2);
                return q.ToList();
            }
        }
        public static void InsertNewVacationYear(VacationYear vacationYear)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                db.VacationYears.Add(vacationYear);
                db.SaveChanges();
            }
        }
        public static VacationYear GetVacationYear(int? vacationId)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                return db.VacationYears.FirstOrDefault(a => a.Id == vacationId);
            }
        }
        public static void DeleteVacationYear(int id)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                VacationYear vacationYearItem = db.VacationYears.FirstOrDefault(a => a.Id == id);
                if (vacationYearItem != null)
                {
                    vacationYearItem.IsDeleted = true;
                    db.SaveChanges();
                }
            }
        }
    }
}
