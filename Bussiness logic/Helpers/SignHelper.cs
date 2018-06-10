using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Model;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Helpers
{
    public static class SignHelper
    {
        public static void PrepareSign(SignInModel SignInModel)
        {
            SignInModel.SignInTimParts = SignInModel.LastSignIn.ToString().Split(' ').ToList();
            if (SignInModel.lastSignOut.Year != 1)
                CalCulateTotalWorked(SignInModel);
        }

        private static void CalCulateTotalWorked(SignInModel SignInModel)
        {
            if (SignInModel.LastSignIn.DayOfYear == DateTime.UtcNow.DayOfYear)
            {
                SignInModel.SignOutTimParts = SignInModel.lastSignOut.ToString().Split(' ').ToList();
                TimeSpan span = SignInModel.lastSignOut.Subtract(SignInModel.LastSignIn);
                SignInModel.WorkedHours = span.Hours;
                SignInModel.WorkedMinut = span.Minutes;
            }
        }
    }
}
