using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerBehaviourAnalyser.Model
{
    public class Customer
    {

        public Customer(int customerId)
        {
            Bets = new List<Bet>();
            CustomerId = customerId;
        }

        public int CustomerId { get; private set; }

        public List<Bet> Bets { get; set; }

        public bool IsFlaggedForHighWinPercentage { get; set; }
    }
}
