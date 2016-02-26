using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerBehaviourAnalyser.Model.Enums;

namespace CustomerBehaviourAnalyser.Model
{
    public class Bet
    {
        public Bet()
        {
            BetRisks = new List<BetRisk>();
        }

        public int Stake { get; set; }
        public int Win { get; set; }

        public int WinPotential { get; set; }
        public BetState State { get; set; }

        public List<BetRisk> BetRisks { get; set; }
    }
}
