using Mathema.Interfaces;
using Mathema.Models.Numerics;
using NUnit.Framework;

namespace ModelsTests.FractionTests
{
    [TestFixture]
    public class SubtractTests
    {
        [Test]
        public void Subtract_Integers()
        {
            //Arrange
            var frac1 = new Fraction(7, 2);
            var n = 2;
            var expected = new Fraction(3, 2);

            //Act
            frac1.Subtract(n);

            //Assert
            Assert.AreEqual(expected.Numerator, frac1.Numerator);
            Assert.AreEqual(expected.Denominator, frac1.Denominator);
        }

        [Test]
        public void Subtract_IntegersAsFraction()
        {
            //Arrange
            var frac1 = new Fraction(2, 1);
            var frac2 = new Fraction(3, 1);
            var expected = new Fraction(1, -1);

            //Act
            frac1.Subtract(frac2);

            //Assert
            Assert.AreEqual(expected.Numerator, frac1.Numerator);
            Assert.AreEqual(expected.Denominator, frac1.Denominator);
        }

        [Test]
        public void Subtract_Decimals_InNumertor()
        {
            //Arrange
            var frac1 = new Fraction(3.1m, 1);
            var frac2 = new Fraction(2.3m, 1);
            var expected = new Fraction(0.8m, 1);

            //Act
            frac1.Subtract(frac2);

            //Assert
            Assert.AreEqual(expected.Numerator, frac1.Numerator);
            Assert.AreEqual(expected.Denominator, frac1.Denominator);
        }

        [Test]
        public void Subtract_Decimals_InDenominator()
        {
            //Arrange
            var frac1 = new Fraction(1, 0.5m);
            var frac2 = new Fraction(1, 0.25m);
            var expected = new Fraction(-2, 1);

            //Act
            frac1.Subtract(frac2);

            //Assert
            Assert.AreEqual(expected.Numerator, frac1.Numerator);
            Assert.AreEqual(expected.Denominator, frac1.Denominator);
        }

        [Test]
        public void Subtract_Decimals_Mixed()
        {
            //Arrange
            var frac1 = new Fraction(0.4m, 0.5m);
            var frac2 = new Fraction(0.15m, 0.25m);
            var expected = new Fraction(1, 5);

            //Act
            frac1.Subtract(frac2);

            //Assert
            Assert.AreEqual(expected.Numerator, frac1.Numerator);
            Assert.AreEqual(expected.Denominator, frac1.Denominator);
        }

        [Test]
        public void Subtract_Regular_0()
        {
            //Arrange
            var frac1 = new Fraction(1, 2);
            var frac2 = new Fraction(1, 2);
            var expected = new Fraction(0, 1);

            //Act
            frac1.Subtract(frac2);

            //Assert
            Assert.AreEqual(expected.Numerator, frac1.Numerator);
            Assert.AreEqual(expected.Denominator, frac1.Denominator);
        }

        [Test]
        public void Subtract_Regular_5_6()
        {
            //Arrange
            var frac1 = new Fraction(1, 2);
            var frac2 = new Fraction(1, 3);
            var expected = new Fraction(1, 6);

            //Act
            frac1.Subtract(frac2);

            //Assert
            Assert.AreEqual(expected.Numerator, frac1.Numerator);
            Assert.AreEqual(expected.Denominator, frac1.Denominator);
        }
    }
}
