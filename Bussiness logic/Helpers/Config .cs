using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLogic.Helpers
{
    public class Config
    {
        public static int PageItemCount()
        {
            string pathValue = ConfigurationManager.AppSettings["PageItemsCount"];
            int PageItemsCount = Convert.ToInt32(pathValue);
            return PageItemsCount;
        }

        //public static int PageItemCount
        //{
        //    get
        //    {
        //        var setting = Caching.Settings.FirstOrDefault(x => x.ConfigKey == "PageItemCount");
        //        if (setting != null)
        //            return Convert.ToInt32(setting.Value);
        //        else
        //            return 0;
        //    }
        //}
  
        public static string S3BlogFolder
        {
            get
            {
                return "Blogs/";
            }
        }
        public static string S3Path
        {
            get
            {
                return " https://s3.eu-central-1.amazonaws.com/alrajhisolutions/";
            }
        }

        public static string[] GDSProviders
        {
            get
            {
                return new string[] { "1G" };
            }
        }
        public static int SaltSize
        {
            get
            {
                // 128 / 8
                // size in byte 
                return 16;
            }
        }
        public static int PBKDF2SubkeyLength
        {
            get
            {
                // 256 / 8
                return 32;
            }
        }
        public static int PBKDF2IterCount
        {
            get
            {
                return 1000;
            }
        }

        public static string CallCenterNumber
        {
            get
            {
                return "920004603";
            }
        }
        public static string AirLowFareSearchPort
        {
            get
            {
                return "AirLowFareSearchPort";
            }
        }
        public static string APIDomainUrl
        {
            get
            {
                return "http://sandbox.moreholidays.net/api/";
                //var setting = Caching.Settings.FirstOrDefault(x => x.ConfigKey == "APIDomainUrl");
                //if (setting != null)
                //    return setting.Value;
                //else
                //    return "";
            }
        }
        public static string APILocalhostUrl
        {
            get
            {
                return "http://localhost:51101/api/";
            }
        }
        public static int ExpertSolutionsMaxNumber
        {
            get
            {
                return 200;
            }
        }
        public static string AirPricePort
        {
            get
            {
                return "AirPricePort";
            }
        }
        public static string PaymentFormKey
        {
            get
            {
                return "2";
            }
        }
        public static string PaymentFormType
        {
            get
            {
                return "Cash";
            }
        }
        public static string AdultCode
        {
            get
            {
                return "ADT";
            }
        }
        public static string ChildCode
        {
            get
            {
                return "CNN";
            }
        }

        public static string InfantCode
        {
            get
            {
                return "INF";
            }
        }
        public static string AddressName
        {
            get
            {
                return "Home";
            }
        }
        public static string AddressStreet
        {
            get
            {
                return "Malaz Salah Ul Din ST";
            }
        }
        public static string AddressCity
        {
            get
            {
                return "Riyadh";
            }
        }
        public static string AddressPostalCode
        {
            get
            {
                return "11417";
            }
        }
        public static string AddressState
        {
            get
            {
                return "CO";
            }
        }

        public static string GuastUserName
        {
            get
            {
                return "Guest@asfar.com";
            }
        }

    }
}
