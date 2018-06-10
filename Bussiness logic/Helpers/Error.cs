using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Helpers
{
    public class Error
    {
        public static string ServerNotRespond
        {
            get
            {
                return "Sorry server not respond now please try again later";
            }
        }
     
        public static string FailedToSaveData
        {
            get
            {
                return "Sorry server can not save your Data now please try again later";
            }
        }
        public static string FailedToDeleteData
        {
            get
            {
                return "Sorry server can not delete your Data now please try again later";
            }
        }
        public static string WrongUserName
        {
            get
            {
                return "UserName is wrong";
            }
        }

        public static string WrongPassoword
        {
            get
            {
                return "Passoword is wrong";
            }
        }
     
    }
}
