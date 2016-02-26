using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CustomerBehaviourAnalyser.Model;

namespace CustomerBehaviourAnalyser
{
    public class CustomerCSVWriter
    {
        FileInfo _outputFileInfo;
        public CustomerCSVWriter(string fileLocation)
        {
            _outputFileInfo = new FileInfo(fileLocation);
        }

        public void WriteCustomerBets(Customer customer)
        {
            using (TextWriter txtWriter = new StreamWriter(_outputFileInfo.Open(FileMode.OpenOrCreate)))
            {
                txtWriter.WriteLine("CustomerId, IsFlaggedForHighWinPercentage");
                txtWriter.WriteLine($"{customer.CustomerId}, {customer.IsFlaggedForHighWinPercentage}");
                txtWriter.WriteLine();
                txtWriter.WriteLine("Customers bets");
                txtWriter.WriteLine("Stake, State, Win Potential, Win, Bet Risks and Reasons");
                foreach (var bet in customer.Bets)
                {
                    StringBuilder sb = new StringBuilder();

                    sb.Append($"{bet.Stake},{bet.State},{bet.WinPotential}, {bet.Win},");

                    foreach (var betrisk in bet.BetRisks)
                    {
                        sb.Append(betrisk.RiskLevel);
                        sb.Append(",");
                        sb.Append(betrisk.Reason);
                        sb.Append(",");
                    }
                    txtWriter.WriteLine(sb.ToString());
                }

            }
        }
    }
}
