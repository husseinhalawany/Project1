using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Model
{
    public class JavascriptRedirectModel
    {
        public JavascriptRedirectModel(string location)
        {
            Location = location;
        }

        public string Location { get; set; }
    }
}
