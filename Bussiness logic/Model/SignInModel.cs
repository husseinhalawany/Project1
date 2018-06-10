using BusinessLogic.Helpers;
using DataMapping.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Model
{
    public class SignInModel : IResult
    {
        public DateTime CurrentDateTime { get; set; }
        public DateTime LastSignIn { set; get; }
        public DateTime lastSignOut { set; get; }
        public DateTime CurrenTime { set; get; }
        public List<string> SignInTimParts { get; set; }
        public List<string> SignOutTimParts { get; set; }

        public int WorkedHours { set; get; }

        public int WorkedMinut { set; get; }
        public string UserName { get; set; }
        public bool Succeeded { get; set; }
        public string ErrorMessage { get; set; }
        public string Image { get; set; }
        public void PrepareSign()
        {
            DateTime dt = DateTimeHelper.Today();
            string date = dt.ToString("dd MMMM yyyy");
            GetPart();
            if (lastSignOut.Year != 1)
                CalCulateTotalWorked();
        }

        private void CalCulateTotalWorked()
        {
            DateTime myDate = DateTimeHelper.Today();
            if (LastSignIn.DayOfYear == myDate.DayOfYear)
            {
                SignInTimParts = LastSignIn.ToString().Split(' ').ToList();
                SignOutTimParts = lastSignOut.ToString().Split(' ').ToList();
                List<string> InHoursVsMinutes = SignInTimParts[1].Split(':').ToList();
                List<string> OutHoursVsMinutes = SignOutTimParts[1].Split(':').ToList();
                int StartHour = int.Parse(InHoursVsMinutes[0]);
                int StartMinute = int.Parse(InHoursVsMinutes[1]);
                int FinishHoure = int.Parse(OutHoursVsMinutes[0]);
                int FinishMinute = int.Parse(OutHoursVsMinutes[1]);
                if (SignInTimParts[2] == "PM")
                {
                    StartHour += 12;
                }
                if (SignOutTimParts[2] == "PM")
                {
                    FinishHoure += 12;
                }
                WorkedHours = FinishHoure - StartHour;
                WorkedMinut = FinishMinute - StartMinute;
                if (WorkedMinut < 0)
                {
                    WorkedHours--;
                    WorkedMinut += 60;
                }
            }
        }

        private void GetPart()
        {
            if (LastSignIn != null)
                SignInTimParts = LastSignIn.ToString().Split(' ').ToList();
        }
    }
}
