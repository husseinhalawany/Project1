using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Sockets;


namespace MangamentProject.Model
{
    public class GetDateTime
    {

        public DateTime? MyDate { get; set; }



        public static DateTime GetServerTime()
        {
            string utcDateTimeString = Path.GetExtension("Sample".Trim()); ;
            var client = new TcpClient("time.nist.gov", 13);
            using (var streamReader = new StreamReader(client.GetStream()))
            {
                var response = streamReader.ReadToEnd();
                utcDateTimeString = response.Substring(7, 17);
            }
            DateTime myDate = DateTime.ParseExact(utcDateTimeString, "yy-MM-dd HH:mm:ss",
                                       System.Globalization.CultureInfo.InvariantCulture);

            return myDate;
        }
    }
}
