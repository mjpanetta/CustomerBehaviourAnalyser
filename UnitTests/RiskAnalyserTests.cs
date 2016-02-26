using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerBehaviourAnalyser.Analysers;
using CustomerBehaviourAnalyser.Model;
using CustomerBehaviourAnalyser.Model.Enums;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class RiskAnalyserTests
    {

        private Bet WonBet = new Bet { Stake = 30, State = BetState.Settled, Win = 500, WinPotential = 0 };
        private Bet LostBet = new Bet { Stake = 50, State = BetState.Settled, Win = 0, WinPotential = 0 };
        private Bet UnsettledBet = new Bet { Stake = 50, State = BetState.Unsettled, Win = 0, WinPotential = 500 };

        private Bet HighWinPotentialBet = new Bet { Stake = 50, State = BetState.Unsettled, Win = 0, WinPotential = 1000 };

        private Bet HighStakeBet = new Bet {Stake = 500, State = BetState.Unsettled, Win = 0, WinPotential = 999};

        private Bet ReallyHighStakeBet = new Bet { Stake = 10000, State = BetState.Unsettled, Win = 0, WinPotential = 999999 };


        [Test]
        public void Catches_Win_Potential_Over_1000_and_Flags_User()
        {
            Customer customer = new Customer(2);

            customer.Bets.Add(HighWinPotentialBet);
            customer.Bets.Add(WonBet);
            customer.Bets.Add(LostBet);
            customer.Bets.Add(UnsettledBet);

            RiskAnalyser analyser = new RiskAnalyser();

            bool result = analyser.AnalyseCustomer(customer);

            Assert.IsTrue(result);
            Assert.AreEqual(customer.Bets.First().BetRisks.First().Reason, "Win potential over 1000");
            Assert.AreEqual(customer.Bets.First().BetRisks.First().RiskLevel, BetRiskLevel.Unsual);

        }

        [Test]
        public void Catches_10_Times_Higher_Stake_Than_Usual()
        {
            Customer customer = new Customer(2);

            customer.Bets.Add(WonBet);
            customer.Bets.Add(LostBet);
            customer.Bets.Add(HighStakeBet);
            customer.Bets.Add(UnsettledBet);

            RiskAnalyser analyser = new RiskAnalyser();

            bool result = analyser.AnalyseCustomer(customer);

            Assert.IsTrue(result);
            Assert.AreEqual(customer.Bets[2].BetRisks.First().Reason, "Bet is 10 times higher than the customers average");
            Assert.AreEqual(customer.Bets[2].BetRisks.First().RiskLevel, BetRiskLevel.Unsual);
        }

        [Test]
        public void Catches_30_Times_Higher_Stake_Than_Usual()
        {
            Customer customer = new Customer(2);

            customer.Bets.Add(WonBet);
            customer.Bets.Add(LostBet);
            customer.Bets.Add(ReallyHighStakeBet);
            customer.Bets.Add(UnsettledBet);

            RiskAnalyser analyser = new RiskAnalyser();

            bool result = analyser.AnalyseCustomer(customer);

            Assert.IsTrue(result);
            Assert.AreEqual(customer.Bets[2].BetRisks[1].Reason, "Bet is 30 timmes higher than the customers average");
            Assert.AreEqual(customer.Bets[2].BetRisks[1].RiskLevel, BetRiskLevel.HighlyUnusual);
        }

        [Test]
        public void Catches_High_Win_Percentage_and_Flags_User()
        {
            Customer customer = new Customer(2);

            customer.IsFlaggedForHighWinPercentage = true;

            customer.Bets.Add(UnsettledBet);
            customer.Bets.Add(UnsettledBet);
            customer.Bets.Add(WonBet);
            customer.Bets.Add(UnsettledBet);

            RiskAnalyser analyser = new RiskAnalyser();

            bool result = analyser.AnalyseCustomer(customer);

            Assert.IsTrue(result);
            Assert.AreEqual(customer.Bets[2].BetRisks[1].Reason, "Customer has Win percentage over 60%");
            Assert.AreEqual(customer.Bets[2].BetRisks[1].RiskLevel, BetRiskLevel.Risky);
        }

        [Test]
        public void Does_Nothing_For_Non_Suspcious_Customer()
        {
            Customer customer = new Customer(2);

            customer.Bets.Add(LostBet);
            customer.Bets.Add(LostBet);
            customer.Bets.Add(LostBet);
            customer.Bets.Add(WonBet);
            customer.Bets.Add(UnsettledBet);

            RiskAnalyser analyser = new RiskAnalyser();

            bool result = analyser.AnalyseCustomer(customer);

            Assert.IsFalse(result);

            foreach (var bet in customer.Bets)
            {
                Assert.IsTrue(!bet.BetRisks.Any());
            }

        }

    }
}
