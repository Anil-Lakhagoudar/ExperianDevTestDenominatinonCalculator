using DenominationCalculator;
using NUnit.Framework;
using System;
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

        [Test]
        public void ProductPriceTest()
        {
            double productPrice = 0;
            double given = 0;

            var exception = Assert.Throws<Exception>(() => _currencyChangeCalculator.GetChanges(productPrice, given));
            Assert.That(exception.Message == "Product price should not be zero");
        }

        [Test]
        public void AmountShouldBeMoreThanProductPriceTest()
        {
            double productPrice = 5.5;
            double given = 5;

            var exception = Assert.Throws<Exception>(() => _currencyChangeCalculator.GetChanges(productPrice, given));
            Assert.That(exception.Message == "Amount should be more than product price");
        }
    }
}