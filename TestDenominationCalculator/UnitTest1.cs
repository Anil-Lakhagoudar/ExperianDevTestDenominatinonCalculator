using DenominationCalculator;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace TestDenominationCalculator
{
    public class Tests
    {
        private CurrencyChangeCalculator _currencyChangeCalculator;

        [SetUp]
        public void Setup()
        {
            _currencyChangeCalculator = new CurrencyChangeCalculator();
        }

        [Test]
        public void Test1()
        {
            double productPrice = 5.5;
            double given = 20;

            List<DenominationModel> denominations = _currencyChangeCalculator.GetChanges(productPrice, given);

            Assert.AreEqual(3, denominations.Count);
            Assert.AreEqual(10, denominations.FirstOrDefault().Denomination);
        }
    }
}