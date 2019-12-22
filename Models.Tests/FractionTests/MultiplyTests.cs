using Mathema.Interfaces;
using Mathema.Models.Numerics;
using NUnit.Framework;


namespace ModelsTests.FractionTests
{
    [TestFixture]
    public class MultiplyTests
    {
        [Test]
        public void Multiply_Integers()
        {
            //Arrange
            var frac1 = new Fraction(1, 2);
            var frac2 = new Fraction(1, 4);
            var expected = new Fraction(1, 8);

            //Act
            frac1.Multiply(frac2);

            //Assert
            Assert.AreEqual(expected.Numerator, frac1.Numerator);
            Assert.AreEqual(expected.Denominator, frac1.Denominator);
        }

        [Test]
        public void Multiply_Decimals_InNumerator()
        {
            //Arrange
            var frac1 = new Fraction(0.25m, 1);
            var frac2 = new Fraction(0.5m, 1);
            var expected = new Fraction(1, 8);

            //Act
            frac1.Multiply(frac2);

            //Assert
            Assert.AreEqual(expected.Numerator, frac1.Numerator);
            Assert.AreEqual(expected.Denominator, frac1.Denominator);
        }

        [Test]
        public void Multiply_Decimals_InDenumerator()
        {
            //Arrange
            var frac1 = new Fraction(1, 0.25m);
            var frac2 = new Fraction(1, 0.5m);
            var expected = new Fraction(8, 1);

            //Act
            frac1.Multiply(frac2);

            //Assert
            Assert.AreEqual(expected.Numerator, frac1.Numerator);
            Assert.AreEqual(expected.Denominator, frac1.Denominator);
        }

        [Test]
        public void Multiply_Decimals_Mixed()
        {
            //Arrange
            var frac1 = new Fraction(0.6m, 0.15m);
            var frac2 = new Fraction(0.5m, 0.25m);
            var expected = new Fraction(8, 1);

            //Act
            frac1.Multiply(frac2);

            //Assert
            Assert.AreEqual(expected.Numerator, frac1.Numerator);
            Assert.AreEqual(expected.Denominator, frac1.Denominator);
        }
    }
}
