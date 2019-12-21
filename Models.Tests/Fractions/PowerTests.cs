using Mathema.Interfaces;
using Mathema.Models.Numerics;
using NUnit.Framework;

namespace ModelsTests.Fractions
{
    [TestFixture]
    public class PowerTests
    {
        [Test]
        public void Pow_Integers()
        {
            //Arrange
            var frac1 = new Fraction(2, 1);
            var frac2 = new Fraction(3, 1);
            var expected = new Fraction(8, 1);

            //Act
            frac1.Pow(frac2);

            //Assert
            Assert.AreEqual(expected.Numerator, frac1.Numerator);
            Assert.AreEqual(expected.Denominator, frac1.Denominator);
        }

        [Test]
        public void Pow_Decimals()
        {
            //Arrange
            var frac1 = new Fraction(4, 1);
            var frac2 = new Fraction(0.5m, 1);
            var expected = new Fraction(2, 1);

            //Act
            frac1.Pow(frac2);

            //Assert
            Assert.AreEqual(expected.Numerator, frac1.Numerator);
            Assert.AreEqual(expected.Denominator, frac1.Denominator);
        }
    }
}
