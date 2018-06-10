using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataMapping.Services;

namespace Bussinesslogic.Model
{
    public class StandUpMeetingMonthlyHistoryModel
    {
        public List<StandUpMeetingDetails> StandUpMeetingDetailsList { get; set; }
        public string ViewTitleName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
