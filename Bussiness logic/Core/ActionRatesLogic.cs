using DataAccess.Repositories;
using DataMapping.Entities;

namespace Bussinesslogic.Core
{
    public class ActionRatesLogic
    {
        public static ActionRate GetActionRateByName(string name)
        {
            return ActionRatesRepositories.GetActionRateByName(name);
        }
    }
}
