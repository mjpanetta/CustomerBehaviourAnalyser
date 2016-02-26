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
            throw new NotImplementedException();
        }

        public List<Customer> Customers => _customers;
    }
}