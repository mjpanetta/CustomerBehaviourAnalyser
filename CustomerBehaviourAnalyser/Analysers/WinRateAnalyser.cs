using System;
using System.Linq;
using CustomerBehaviourAnalyser.Model;
using CustomerBehaviourAnalyser.Model.Enums;

namespace CustomerBehaviourAnalyser.Analysers
{
    public class WinRateAnalyser
    {

        public bool AnalyseCustomer(Customer customer)
        {
            var betWins = customer.Bets.Count(b => b.State == BetState.Settled && b.Win > 0);
            var totalBets = customer.Bets.Count(b => b.State == BetState.Settled);

            int percentageWon = (int)Math.Round((double)(100 * betWins) / totalBets);

            if (percentageWon >= 60)
            {
                customer.IsFlaggedForHighWinPercentage = true;
                return true;
            }
            return false;
        }
    }
}
