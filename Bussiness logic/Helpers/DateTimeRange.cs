using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Helpers
{
    public class DateTimeRange
    {
        public DateTime Start;
        public DateTime End;

        public DateTimeRange(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
            if (Start > End)
            {
                throw new Exception("Invalid range");
            }
        }
        public bool Intersects(DateTimeRange dateTimeRange)
        {
            if (Start >= dateTimeRange.End || dateTimeRange.Start >= End)
            {
                return false;
            }
            return true;
        }
    }
}
