using System;
using System.Collections.Generic;
using BusinessLogic.Helpers;
using Castle.Components.DictionaryAdapter;
using DataAccess.Repositories;
using DataMapping.Entities;
using DataMapping.Services;

namespace Bussinesslogic.Core
{
    public class EmployeePointsLogic
    {
        public static void InsertNewEmployeePoint(EmployeePoint employeePoint)
        {
            EmployeePointsRepositories.InsertNewEmployeePoint(employeePoint);
        }
        public static List<EmployeePointDetails> MonthBestEmployees()
        {
            return EmployeePointsRepositories.BestEmployeesOfMonthList(DateTimeHelper.Today());
        }
        public static List<EmployeePointDetails> QuarterBestEmployees(out string quarterName)
        {
            DateTime today = DateTimeHelper.Today();
            DateTime startDate;
            DateTime endDate;
            int quarter = DateTimeHelper.GetQuarter(today);
            switch (quarter)
            {
                case 1:
                    startDate = new DateTime(today.Year, 1, 1);
                    endDate = new DateTime(today.Year, 3, 31);
                    quarterName = "1st";
                    break;
                case 2:
                    startDate = new DateTime(today.Year, 4, 1);
                    endDate = new DateTime(today.Year, 6, 30);
                    quarterName = "2nd";
                    break;
                case 3:
                    startDate = new DateTime(today.Year, 7, 1);
                    endDate = new DateTime(today.Year, 9, 30);
                    quarterName = "3rd";
                    break;
                default:
                    startDate = new DateTime(today.Year, 10, 1);
                    endDate = new DateTime(today.Year, 12, 31);
                    quarterName = "4th";
                    break;
            }
            return EmployeePointsRepositories.BestEmployeesOfQuarterList(startDate.AddDays(-1), endDate.AddDays(1));
        }
        public static List<EmployeePointDetails> MonthAndQuarterBestEmployees()
        {
            List<EmployeePointDetails> bestEmployeeOfMonthAndQuarter = new List<EmployeePointDetails>();
            DateTime today = DateTimeHelper.Today();

            EmployeePointDetails bestEmployeeOfTheMonth = EmployeePointsRepositories.BestEmployeeOfMonth(today);
            if (bestEmployeeOfTheMonth == null)
                bestEmployeeOfTheMonth = new EmployeePointDetails();

            bestEmployeeOfTheMonth.MonthName = today.ToString("MMMM");
            bestEmployeeOfMonthAndQuarter.Add(bestEmployeeOfTheMonth);

            DateTime startDate;
            DateTime endDate;
            string quarterName;
            int quarter = DateTimeHelper.GetQuarter(today);
            switch (quarter)
            {
                case 1:
                    startDate = new DateTime(today.Year, 1, 1);
                    endDate = new DateTime(today.Year, 3, 31);
                    quarterName = "1st";
                    break;
                case 2:
                    startDate = new DateTime(today.Year, 4, 1);
                    endDate = new DateTime(today.Year, 6, 30);
                    quarterName = "2nd";
                    break;
                case 3:
                    startDate = new DateTime(today.Year, 7, 1);
                    endDate = new DateTime(today.Year, 9, 30);
                    quarterName = "3rd";
                    break;
                default:
                    startDate = new DateTime(today.Year, 10, 1);
                    endDate = new DateTime(today.Year, 12, 31);
                    quarterName = "4th";
                    break;
            }
            EmployeePointDetails bestEmployeeOfTheQuarter = EmployeePointsRepositories.BestEmployeeOfQuarter(startDate.AddDays(-1), endDate.AddDays(1));
            if (bestEmployeeOfTheQuarter == null)
                bestEmployeeOfTheQuarter = new EmployeePointDetails();

            bestEmployeeOfTheQuarter.QuarterName = quarterName;
            bestEmployeeOfMonthAndQuarter.Add(bestEmployeeOfTheQuarter);

            return bestEmployeeOfMonthAndQuarter;
        }
    }
}
