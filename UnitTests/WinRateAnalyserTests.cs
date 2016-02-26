using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerBehaviourAnalyser.Model;
using CustomerBehaviourAnalyser.Model.Enums;
using CustomerBehaviourAnalyser.WinRateAnalyser;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class WinRateAnalyserTests
    {
        private Bet WonBet = new Bet {Stake = 30, State = BetState.Settled, Win = 500, WinPotential = 0};

        private Bet LostBet = new Bet { Stake = 50, State = BetState.Settled, Win = 0, WinPotential = 0 };

        private Bet UnsettledBet = new Bet { Stake = 50, State = BetState.Unsettled, Win = 0, WinPotential = 1000 };

        [Test]
        public void Unsettled_Bets_Arent_Taken_Into_Account()
        {
            Customer customer = new Customer(1);
            customer.Bets.Add(WonBet);
            customer.Bets.Add(WonBet);
            customer.Bets.Add(UnsettledBet);
            customer.Bets.Add(UnsettledBet);
            customer.Bets.Add(UnsettledBet);
            customer.Bets.Add(UnsettledBet);

            WinRateAnalyser analyser = new WinRateAnalyser();

            bool result = analyser.AnalyseCustomer(customer);

            Assert.IsTrue(result);
            Assert.IsTrue(customer.IsFlaggedForHighWinPercentage);
        }

        [Test]
        public void Customer_With_All_Wins_Is_Flagged()
        {
            Customer customer = new Customer(1);
            customer.Bets.Add(WonBet);
            customer.Bets.Add(WonBet);
            customer.Bets.Add(WonBet);
            customer.Bets.Add(WonBet);

            WinRateAnalyser analyser = new WinRateAnalyser();

            bool result = analyser.AnalyseCustomer(customer);

            Assert.IsTrue(result);
            Assert.IsTrue(customer.IsFlaggedForHighWinPercentage);
        }

        [Test]
        public void Customer_With_60_Percent_Win_Rate_Is_Flagged()
        {
            Customer customer = new Customer(1);
            customer.Bets.Add(WonBet);
            customer.Bets.Add(WonBet);
            customer.Bets.Add(WonBet);
            customer.Bets.Add(LostBet);
            customer.Bets.Add(LostBet);

            WinRateAnalyser analyser = new WinRateAnalyser();

            bool result = analyser.AnalyseCustomer(customer);

            Assert.IsTrue(result);
            Assert.IsTrue(customer.IsFlaggedForHighWinPercentage);
        }

        [Test]
        public void Customer_With_7_out_of_11_Bets_Is_Flagged()
        {
            Customer customer = new Customer(1);
            customer.Bets.Add(WonBet);
            customer.Bets.Add(WonBet);
            customer.Bets.Add(WonBet);
            customer.Bets.Add(WonBet);
            customer.Bets.Add(WonBet);
            customer.Bets.Add(WonBet);
            customer.Bets.Add(WonBet);

            customer.Bets.Add(LostBet);
            customer.Bets.Add(LostBet);
            customer.Bets.Add(LostBet);
            customer.Bets.Add(LostBet);

            WinRateAnalyser analyser = new WinRateAnalyser();

            bool result = analyser.AnalyseCustomer(customer);

            Assert.IsTrue(result);
            Assert.IsTrue(customer.IsFlaggedForHighWinPercentage);
        }

        [Test]
        public void Customer_With_50_Percent_Win_Rate_Is_Safe()
        {
            Customer customer = new Customer(1);
            customer.Bets.Add(WonBet);
            customer.Bets.Add(WonBet);
            customer.Bets.Add(LostBet);
            customer.Bets.Add(LostBet);
            customer.Bets.Add(LostBet);

            WinRateAnalyser analyser = new WinRateAnalyser();

            bool result = analyser.AnalyseCustomer(customer);

            Assert.IsFalse(result);
            Assert.IsFalse(customer.IsFlaggedForHighWinPercentage);
        }

    }
}
