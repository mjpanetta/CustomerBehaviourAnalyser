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

        private List<IAnalysingRule> _analysingRules;

        public RiskAnalyser()
        {
            _analysingRules = new List<IAnalysingRule>();
        }

        public void RegisterRule(IAnalysingRule rule)
        {
            _analysingRules.Add(rule);
        }

        public bool AnalyseCustomer(Customer customer)
        {
            bool risky = false;
            foreach (var rule in _analysingRules)
            {
                bool result = rule.Analyse(customer);
                risky = risky || result;
            }
            return risky;
        }
    }
}
