using DenominationCalculator;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestDenominationCalculator
{
    public class Tests
    {
        private CurrencyChangeCalculator? _currencyChangeCalculator;

        [SetUp]
        public void Setup()
        {
            _currencyChangeCalculator = new CurrencyChangeCalculator();
        }

        [Test]
        public void ProductPriceTest()
        {
            double productPrice = 0;
            double given = 0;

            var exception = Assert.Throws<Exception>(() => _currencyChangeCalculator?.GetChanges(productPrice, given));
            Assert.That(exception?.Message == "Product price should not be zero");
        }

        [Test]
        public void AmountShouldBeMoreThanProductPriceTest()
        {
            double productPrice = 5.5;
            double given = 5;

            var exception = Assert.Throws<Exception>(() => _currencyChangeCalculator?.GetChanges(productPrice, given));
            Assert.That(exception?.Message == "Amount should be more than product price");
        }

        [Test]
        public void CurrencyChangeTest()
        {
            double productPrice = 5.5;
            double given = 20;

            if(_currencyChangeCalculator != null)
            {
                List<DenominationModel> denominations = _currencyChangeCalculator.GetChanges(productPrice, given);
                Assert.AreEqual(3, denominations.Count);
            }
            else
            {
                Assert.Fail("Null reference");
            }
        }

        [Test]
        public void NoChangeToReturnTest()
        {
            double productPrice = 10;
            double given = 10;

            if (_currencyChangeCalculator != null)
            {
                List<DenominationModel> denominations = _currencyChangeCalculator.GetChanges(productPrice, given);
                Assert.AreEqual(0, denominations.Count);
            }
            else
            {
                Assert.Fail("Null reference");
            }
        }

        [Test]
        public void ShouldHave10PoundAsFirstDenominationTest()
        {
            double productPrice = 5.5;
            double given = 20;

            if (_currencyChangeCalculator != null)
            {
                List<DenominationModel> denominations = _currencyChangeCalculator.GetChanges(productPrice, given);
                DenominationModel denomination = denominations.First();

                Assert.AreEqual(10, denomination.Denomination);
            }
            else
            {
                Assert.Fail("Null reference");
            }
        }

        [Test]
        public void ShouldHave2PoundDenominationWithMultipleCountTest()
        {
            double productPrice = 5.5;
            double given = 20;

            if (_currencyChangeCalculator != null)
            {
                List<DenominationModel> denominations = _currencyChangeCalculator.GetChanges(productPrice, given);
                DenominationModel? denomination = denominations.FindAll((d)=> d.Denomination == 2).FirstOrDefault();

                Assert.AreEqual(2, denomination?.Denomination);
                Assert.AreEqual(2, denomination?.Count);
            }
            else
            {
                Assert.Fail("Null reference");
            }
        }
    }
}