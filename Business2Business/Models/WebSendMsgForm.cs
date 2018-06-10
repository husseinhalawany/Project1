using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MangamentProject.Models
{
    public class WebSendMsgForm
    {
        public string To { set; get; }
        public string Title { set; get; }
        public string Body { set; get; }
        public string FileUrl{set;get;}


    }
}
