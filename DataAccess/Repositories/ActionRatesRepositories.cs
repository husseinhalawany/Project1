using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataMapping.Entities;

namespace DataAccess.Repositories
{
    public class ActionRatesRepositories
    {
      
     
       
        
        public static ActionRate GetActionRateByName(string name)
        {
            using (ManagementSystemEntities db = new ManagementSystemEntities())
            {
                return db.ActionRates.FirstOrDefault(a => a.Name == name);
            }
        }
     
      
    }
}
