using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerBehaviourAnalyser.Model;
using CustomerBehaviourAnalyser.Model.Enums;

namespace CustomerBehaviourAnalyser.Analysers
{
    public class RiskAnalyser
    {
        private const string HIGH_WIN_POTENTIAL = "Win potential over 1000";

        private const string HIGH_WIN_PERCENTAGE = "Customer has Win percentage over 60%";

        private const string STAKE_HIGHER_THAN_30_TIMES_AVERAGE = "Bet is 30 timmes higher than the customers average";

        private const string STAKE_HIGHER_THAN_10_TIMES_AVERAGE = "Bet is 10 times higher than the customers average";

        public bool AnalyseCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
