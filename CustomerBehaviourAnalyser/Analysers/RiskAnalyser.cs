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
            bool risky = false;
            
            foreach (var bet in customer.Bets.Where(b => b.State == BetState.Unsettled))
            {
                if (customer.IsFlaggedForHighWinPercentage)
                {
                    bet.BetRisks.Add(new BetRisk { Reason = HIGH_WIN_PERCENTAGE, RiskLevel = BetRiskLevel.Risky });
                    risky = true;
                }

                if (bet.WinPotential > 1000)
                {
                    bet.BetRisks.Add(new BetRisk { Reason = HIGH_WIN_POTENTIAL, RiskLevel = BetRiskLevel.Unsual });
                    risky = true;
                }

                //assumed current bet shouldn't be included when working out averages
                Double averageStake = customer.Bets.Where(b => b != bet).Average(b => b.Stake);
                //Bet risk will always be 10 times higher if 30 times higher, no point in recording both
                if (bet.Stake > averageStake * 10)
                {
                    risky = true;
                    if (bet.Stake > averageStake * 30)
                        bet.BetRisks.Add(new BetRisk { Reason = STAKE_HIGHER_THAN_30_TIMES_AVERAGE, RiskLevel = BetRiskLevel.HighlyUnusual });
                    else
                        bet.BetRisks.Add(new BetRisk { Reason = STAKE_HIGHER_THAN_10_TIMES_AVERAGE, RiskLevel = BetRiskLevel.Unsual });
                }

            }

            return risky;
        }
    }
}
