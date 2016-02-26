using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerBehaviourAnalyser.Model
{
    public class Customer
    {

        public Customer()
        {
            Bets = new List<Bet>();
        }

        public int CustomerId { get; set; }

        public List<Bet> Bets { get; set; }

        public bool IsFlaggedForHighWinPercentage { get; set; }
    }
}
