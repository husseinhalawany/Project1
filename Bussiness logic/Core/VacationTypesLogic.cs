using DataAccess.Repositories;
using DataMapping.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Model;
using BusinessLogic.Helpers;
using DataMapping.Services;

namespace BusinessLogic.Core
{
    public class VacationTypesLogic
    {
        public static List<VacationType> GetVacationTypesList(int page)
        {
            int takeCount = Config.PageItemCount();
            int skipCount = page * takeCount;
            return VacationTypesRepositories.GetVacationTypesList(skipCount, takeCount).ToList();
        }
        public static void DeleteVacationType(int id)
        {
            VacationTypesRepositories.DeleteVacationType(id);
        }
        public static void InsertVacationType(VacationType vacationType)
        {
            VacationTypesRepositories.InsertVacationType(vacationType);
        }
        public static VacationType GetVacationTypeById(int? id)
        {
            return VacationTypesRepositories.GetVacationTypeById(id);
        }
      
        public static void UpdateVacationType(VacationType vacationType)
        {
            VacationTypesRepositories.UpdateVacationType(vacationType);
        }
        public static List<EmployeeUsersDetails> GetEmployeesInVacationType(int vacationTypeId)
        {
            return VacationTypesRepositories.GetEmployeesInVacationType(vacationTypeId);
        }
      
      
        
    }
}