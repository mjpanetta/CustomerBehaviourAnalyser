using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerBehaviourAnalyser.Model;
using CustomerBehaviourAnalyser.Model.Enums;

namespace CustomerBehaviourAnalyser.Analysers
{
    public class HighWinPotentialRule : IAnalysingRule
    {
        private const string HIGH_WIN_POTENTIAL = "Win potential over 1000";


        public bool Analyse(Customer customer)
        {
            bool risky = false;
            foreach (var bet in customer.Bets.Where(b => b.State == BetState.Unsettled))
            {
                if (bet.WinPotential > 1000)
                {
                    bet.BetRisks.Add(new BetRisk {Reason = HIGH_WIN_POTENTIAL, RiskLevel = BetRiskLevel.Unsual});
                    risky = true;
                }
            }

            return risky;
        }
    }
}
