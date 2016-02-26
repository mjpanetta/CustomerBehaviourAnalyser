using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerBehaviourAnalyser.Model;
using CustomerBehaviourAnalyser.Model.Enums;

namespace CustomerBehaviourAnalyser.Analysers
{
    public class CustomerFlaggedRule : IAnalysingRule
    {
        private const string HIGH_WIN_PERCENTAGE = "Customer has Win percentage over 60%";

        public bool Analyse(Customer customer)
        {
            bool risky = false;
            foreach (var bet in customer.Bets.Where(b => b.State == BetState.Unsettled))
            {
                if (customer.IsFlaggedForHighWinPercentage)
                {
                    bet.BetRisks.Add(new BetRisk {Reason = HIGH_WIN_PERCENTAGE, RiskLevel = BetRiskLevel.Risky});
                    risky = true;
                }
            }
            return risky;
        }
    }
}
