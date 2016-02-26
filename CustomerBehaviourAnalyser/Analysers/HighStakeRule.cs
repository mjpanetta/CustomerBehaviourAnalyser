using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerBehaviourAnalyser.Model;
using CustomerBehaviourAnalyser.Model.Enums;

namespace CustomerBehaviourAnalyser.Analysers
{
    public class HighStakeRule : IAnalysingRule
    {
        private const string STAKE_HIGHER_THAN_30_TIMES_AVERAGE = "Bet is 30 timmes higher than the customers average";

        private const string STAKE_HIGHER_THAN_10_TIMES_AVERAGE = "Bet is 10 times higher than the customers average";

        public bool Analyse(Customer customer)
        {
            bool risky = false;
            foreach (var bet in customer.Bets.Where(b => b.State == BetState.Unsettled))
            {
                Double averageStake = customer.Bets.Where(b => b != bet).Average(b => b.Stake);
                //Bet risk will always be 10 times higher if 30 times higher, no point in recording both
                if (bet.Stake > averageStake*10)
                {
                    risky = true;
                    if (bet.Stake > averageStake*30)
                        bet.BetRisks.Add(new BetRisk
                        {
                            Reason = STAKE_HIGHER_THAN_30_TIMES_AVERAGE,
                            RiskLevel = BetRiskLevel.HighlyUnusual
                        });
                    else
                        bet.BetRisks.Add(new BetRisk
                        {
                            Reason = STAKE_HIGHER_THAN_10_TIMES_AVERAGE,
                            RiskLevel = BetRiskLevel.Unsual
                        });
                }
            }

            return risky;
        }
    }
}
