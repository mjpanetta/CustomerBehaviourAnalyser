using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerBehaviourAnalyser.Model.Enums;

namespace CustomerBehaviourAnalyser.Model
{
    public class BetRisk
    {

        public BetRiskLevel RiskLevel { get; set; }
        public string Reason { get; set; }
    }
}
