using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerBehaviourAnalyser;
using CustomerBehaviourAnalyser.Model.Enums;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class CustomerParserTests
    {
        string input =
            "Customer,Event,Participant,Stake,Win | 1,1,6,50,250 | 2,1,3,5,0 | 3,1,3,20,0 | 1,1,6,200,1000 | 1,2,1,20,60";

        string invalidInputExtraColumn =
            "Customer,Event,Participant,Stake,Win | 1,1,6,50,250 | 2,1,3,5,0 | 3,1,3,20,0 | 1,1,6,200,1000,5 | 1,2,1,20,60";
        string invalidInputNonInt =
            "Customer,Event,Participant,Stake,Win | 1,1,abc,50,250 | 2,1,3,5,0 | 3,1,3,20,0 | 1,1,6,200,1000,5 | 1,2,1,20,60";

        [Test]
        public void Doesnt_Create_Duplicate_Customers()
        {
            var inputArray = input.Split('|');
            CustomerParser parser = new CustomerParser(inputArray[0]);

            for (int i = 1; i < inputArray.Length; i++)
            {
                parser.ParseLine(inputArray[i]);
            }

            Assert.IsTrue(parser.Customers.Count == 3);
        }

        [Test]
        public void Throws_Exception_For_Extra_Column()
        {
            var inputArray = invalidInputExtraColumn.Split('|');


            try
            {
                CustomerParser parser = new CustomerParser(inputArray[0]);

                for (int i = 1; i < inputArray.Length; i++)
                {
                    parser.ParseLine(inputArray[i]);
                }
            }

            catch (Exception e)
            {
                Assert.IsTrue(e is ArgumentException);
            }

        }

        [Test]
        public void Throws_Exception_For_Non_Int_Provided()
        {
            var inputArray = invalidInputNonInt.Split('|');

            try
            {
                CustomerParser parser = new CustomerParser(inputArray[0]);

                for (int i = 1; i < inputArray.Length; i++)
                {
                    parser.ParseLine(inputArray[i]);
                }
            }

            catch (Exception e)
            {
                Assert.IsTrue(e is ArgumentException);
            }
        }

        [Test]
        public void Creates_Customers_And_Unsettled_Bets_Correctly()
        {
            var inputArray = input.Split('|');

            CustomerParser parser = new CustomerParser(inputArray[0]);

            for (int i = 1; i < inputArray.Length; i++)
            {
                parser.ParseLine(inputArray[i]);
            }

            var customers = parser.Customers;

            var firstCustomer = customers.FirstOrDefault(c => c.CustomerId == 1);
            var secondCustomer = customers.FirstOrDefault(c => c.CustomerId == 2);
            var firstCustomerFirstBet = firstCustomer.Bets.FirstOrDefault();

            Assert.AreEqual(firstCustomer.Bets.Count, 3);
            Assert.AreEqual(secondCustomer.Bets.Count, 1);
            Assert.AreEqual(firstCustomerFirstBet.Stake, 50);
            Assert.AreEqual(firstCustomerFirstBet.State, BetState.Unsettled);
            Assert.AreEqual(firstCustomerFirstBet.WinPotential, 250);
            Assert.AreEqual(firstCustomerFirstBet.Win, 0);
            Assert.IsTrue(!firstCustomerFirstBet.BetRisks.Any());
        }

        [Test]
        public void Creates_Customers_And_Settled_Bets_Correctly()
        {
            var inputArray = input.Split('|');

            CustomerParser parser = new CustomerParser(inputArray[0]);

            for (int i = 1; i < inputArray.Length; i++)
            {
                parser.ParseLine(inputArray[i], BetState.Settled);
            }

            var customers = parser.Customers;

            var firstCustomer = customers.FirstOrDefault(c => c.CustomerId == 1);
            var secondCustomer = customers.FirstOrDefault(c => c.CustomerId == 2);
            var firstCustomerFirstBet = firstCustomer.Bets.FirstOrDefault();

            Assert.AreEqual(firstCustomer.Bets.Count, 3);
            Assert.AreEqual(secondCustomer.Bets.Count, 1);
            Assert.AreEqual(firstCustomerFirstBet.Win, 250);
            Assert.AreEqual(firstCustomerFirstBet.WinPotential, 0);
            Assert.AreEqual(firstCustomerFirstBet.State, BetState.Settled);
        }
    }
}
