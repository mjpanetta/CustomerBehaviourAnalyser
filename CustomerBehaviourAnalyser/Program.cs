using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerBehaviourAnalyser.Analysers;
using CustomerBehaviourAnalyser.Model.Enums;

namespace CustomerBehaviourAnalyser
{
    class Program
    {
        static void Main(string[] args)
        {
            FileInfo settledBetsFile = new FileInfo(ConfigurationManager.AppSettings["SettledBetsPath"]);
            FileInfo unSettledBetsFile = new FileInfo(ConfigurationManager.AppSettings["UnsettledBetsPath"]);
            CustomerParser parser = null;
            WinRateAnalyser winRateAnalyser = new WinRateAnalyser();
            RiskAnalyser riskAnalyser = new RiskAnalyser();
            riskAnalyser.RegisterRule(new CustomerFlaggedRule());
            riskAnalyser.RegisterRule(new HighStakeRule());
            riskAnalyser.RegisterRule(new HighWinPotentialRule());

            using (var fileStream = settledBetsFile.OpenRead())
            {
                using (var reader = new StreamReader(fileStream))
                {
                    parser = new CustomerParser(reader.ReadLine());
                    while (!reader.EndOfStream)
                    {
                        parser.ParseLine(reader.ReadLine(), BetState.Settled);
                    }
                }
            }
            using (var fileStream = unSettledBetsFile.OpenRead())
            {
                using (var reader = new StreamReader(fileStream))
                {
                    reader.ReadLine(); //Getting rid of headers
                    while (!reader.EndOfStream)
                    {
                        parser.ParseLine(reader.ReadLine(), BetState.Unsettled);
                    }
                }
            }

            var customers = parser.Customers;

            foreach (var customer in customers)
            {
                winRateAnalyser.AnalyseCustomer(customer);
                riskAnalyser.AnalyseCustomer(customer);

                CustomerCSVWriter writer = new CustomerCSVWriter($"../../customer {customer.CustomerId} bets.csv");
                writer.WriteCustomerBets(customer);
            }

            

        }
    }
}
