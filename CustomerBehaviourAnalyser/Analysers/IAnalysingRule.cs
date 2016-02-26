using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerBehaviourAnalyser.Model;

namespace CustomerBehaviourAnalyser.Analysers
{
    public interface IAnalysingRule
    {
        bool Analyse(Customer customer);
    }
}
