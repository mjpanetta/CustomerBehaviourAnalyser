using CustomerBehaviourAnalyser.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerBehaviourAnalyser.Model;

namespace CustomerBehaviourAnalyser
{
    public class CustomerParser
    {
        private int _columns;
        private List<Customer> _customers;

        public CustomerParser(string headers)
        {
            _columns = headers.Split(',').Length;
            _customers = new List<Customer>();
        }

        public void ParseLine(string inputLine, BetState state = BetState.Unsettled)
        {
            var inputArray = inputLine.Split(',');

            if (inputArray.Length != _columns)
                throw new ArgumentException($"Invalid input: Expecting {_columns} columns but got {inputArray.Length}");

            int customerId;
            int betStake;
            int payOff;

            if (!int.TryParse(inputArray[0], out customerId) || !int.TryParse(inputArray[3], out betStake) ||
                !int.TryParse(inputArray[4], out payOff))
            {
                throw new ArgumentException($"Invalid input: A value provided was not an int for customerid {inputArray[0]} ");
            }

            var customer = _customers.FirstOrDefault(c => c.CustomerId == customerId);

            if (customer == null)
            {
                customer = new Customer(customerId);
                _customers.Add(customer);
            }

            Bet bet = new Bet {Stake = betStake};
            switch (state)
            {
                case BetState.Unsettled:
                    bet.State = BetState.Unsettled;
                    bet.WinPotential = payOff;
                    break;
                case BetState.Settled:
                    bet.State = BetState.Settled;
                    bet.Win = payOff;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }

            customer.Bets.Add(bet);
        }

        public List<Customer> Customers => _customers;
    }
}