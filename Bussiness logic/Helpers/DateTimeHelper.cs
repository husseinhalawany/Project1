using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Core;
using DataMapping.Entities;
using System.Net;
using DataAccess.Repositories;
using DataMapping.Services;
using BusinessLogic.Model;
using BusinessLogic.Helpers;
using System.IO;
using System.Net.Sockets;
using System.Globalization;
using MangamentProject.Core;

namespace BusinessLogic.Helpers
{
    public class DateTimeHelper
    {


        public static DateTime Today()
        {
            DateTime? dateTime = null;
            try
            {
                var myHttpWebRequest = (System.Net.HttpWebRequest)WebRequest.Create("http://www.microsoft.com");
                var response = myHttpWebRequest.GetResponse();
                string todaysDates = response.Headers["date"];
                dateTime = DateTime.ParseExact(todaysDates, "ddd, dd MMM yyyy HH:mm:ss 'GMT'", System.Globalization.CultureInfo.InvariantCulture);

            }
            catch (Exception e)
            {
                LogsLogic.InsertLog(new Log()
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    StoryName = "BusinessLogic/AttendancesLogic/GetServerTime"
                });
            }
            return dateTime.Value.AddHours(2);
        }

        public static int GetQuarter(DateTime date)
        {
            if (date.Month >= 1 && date.Month <= 3)
                return 1;
            else if (date.Month >= 4 && date.Month <= 6)
                return 2;
            else if (date.Month >= 7 && date.Month <= 9)
                return 3;
            else
                return 4;

        }
        public static int GetWorkingDaysCountInMonth(DateTime startDate, DateTime endDate)
        {
            int days = 0;
            while (startDate <= endDate)
            {
                if (startDate.DayOfWeek != DayOfWeek.Friday && startDate.DayOfWeek != DayOfWeek.Saturday)
                {
                    ++days;
                }
                startDate = startDate.AddDays(1);
            }
            int occasionVacationDaysCount = OccasionVacationsLogic.GetCountOfVacationDaysBetweenTowDates(startDate, endDate);
            return days - occasionVacationDaysCount;
        }
        public static void GetDifference(DateTime fromDate, DateTime toDate, out int years,
    out int months, out int weeks, out int days)
        {
            //years
            TimeSpan diff = toDate - fromDate;
            years = diff.Days / 366;
            DateTime workingDate = fromDate.AddYears(years);

            while (workingDate.AddYears(1) <= toDate)
            {
                workingDate = workingDate.AddYears(1);
                years++;
            }

            //months
            diff = toDate - workingDate;
            months = diff.Days / 31;
            workingDate = workingDate.AddMonths(months);

            while (workingDate.AddMonths(1) <= toDate)
            {
                workingDate = workingDate.AddMonths(1);
                months++;
            }

            //weeks and days
            diff = toDate - workingDate;
            weeks = diff.Days / 7;
            days = diff.Days % 7;
        }

    }
}
