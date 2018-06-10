using DataMapping.Entities;
using DataMapping.Services;
using System.Collections.Generic;
using System.Linq;
using System;

namespace DataAccess.Repositories
{
    public class VacationTypesRepositories
    {
        public static List<VacationType> GetVacationTypesExceptUnpaid()
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                return db.VacationTypes.Where(v => v.IsDeleted == false && v.Name != "أجازة بدون أجر" && v.Name != "العمل من المنزل").ToList();
            }
        }
        public static List<VacationType> GetVacationTypesList(int skipCount, int takeCount)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                return db.VacationTypes.Where(a => a.IsDeleted == false).Skip(skipCount).Take(takeCount).ToList();
            }
        }
        public static VacationType GetVacationTypeById(int? id)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                return db.VacationTypes.SingleOrDefault(a => a.Id == id && a.IsDeleted == false);
            }
        }

        public static VacationType GetVacationTypeDeductionFromWages()
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                return db.VacationTypes.Where(v => v.IsDeleted == false && v.Name == "أجازة بدون أجر").FirstOrDefault();
            }
        }

        public static void InsertVacationType(VacationType VacationType)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                db.VacationTypes.Add(VacationType);
                db.SaveChanges();
            }
        }
        public static void UpdateVacationType(VacationType vacationType)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                var q = db.VacationTypes.SingleOrDefault(a => a.Id == vacationType.Id);
                if (q != null)
                {
                    q.Id = vacationType.Id;
                    q.Name = vacationType.Name;
                    q.VacationLength = vacationType.VacationLength;
                    db.SaveChanges();
                }
            }
        }
        public static void DeleteVacationType(int id)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                var q = db.VacationTypes.SingleOrDefault(a => a.Id == id);
                if (q != null)
                {
                    q.IsDeleted = true;
                    db.SaveChanges();
                }
            }
        }
        public static List<EmployeeUsersDetails> GetEmployeesInVacationType(int vacationTypeId)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                List<EmployeeUsersDetails> employeesDetailsList = (
                from eVac in db.EmployeeVacations.Where(q=>q.VacationTypeId == vacationTypeId && q.StatusId ==2) //2 == Approved
                group eVac by eVac.EmployeeUserId into empVac
                join user in db.UserProfiles
                    on empVac.FirstOrDefault().EmployeeUserId equals user.UserId
                    select  new EmployeeUsersDetails()
                    {
                        UserId = user.UserId,
                        UserName = user.UserName,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Phone1 = user.Phone1,
                        Email = user.Email,
                        Address = user.Address,
                        ImgURL = user.ProfilePictureUrl,
                        VacationDays = empVac.Sum(q=>q.VacationDays)
                    }
                    ).ToList();

                return employeesDetailsList;
            }
        }
        public static VacationType GetWorkFromHomeVacationType()
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                return db.VacationTypes.FirstOrDefault(h => h.IsDeleted == false && h.Name == "العمل من المنزل");
            }
        }
    }
}