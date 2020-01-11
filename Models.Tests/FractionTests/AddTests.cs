using Mathema.Models.Numerics;
using NUnit.Framework;

namespace ModelsTests.FractionTests
{
    [TestFixture]
    public class AddTests
    {

        [Test]
        public void Add_Integers()
        {
            //Arrange
            var frac1 = new Fraction(2, 3);
            var n = 2;
            var expected = new Fraction(8, 3);

            //Act
            frac1.Add(n);

            //Assert
            Assert.AreEqual(expected.Numerator, frac1.Numerator);
            Assert.AreEqual(expected.Denominator, frac1.Denominator);
        }


        [Test]
        public void Add_IntegersAsFraction()
        {
            //Arrange
            var frac1 = new Fraction(2, 1);
            var frac2 = new Fraction(3, 1);
            var expected = new Fraction(5, 1);

            //Act
            frac1.Add(frac2);

            //Assert
            Assert.AreEqual(expected.Numerator, frac1.Numerator);
            Assert.AreEqual(expected.Denominator, frac1.Denominator);
        }

        [Test]
        public void Add_Decimals_InNumertor()
        {
            //Arrange
            var frac1 = new Fraction(2.1m, 1);
            var frac2 = new Fraction(3.3m, 1);
            var expected = new Fraction(5.4m, 1);

            //Act
            frac1.Add(frac2);

            //Assert
            Assert.AreEqual(expected.Numerator, frac1.Numerator);
            Assert.AreEqual(expected.Denominator, frac1.Denominator);
        }

        [Test]
        public void Add_Decimals_InDenominator()
        {
            //Arrange
            var frac1 = new Fraction(1, 0.5m);
            var frac2 = new Fraction(1, 0.25m);
            var expected = new Fraction(6, 1);

            //Act
            frac1.Add(frac2);

            //Assert
            Assert.AreEqual(expected.Numerator, frac1.Numerator);
            Assert.AreEqual(expected.Denominator, frac1.Denominator);
        }

        [Test]
        public void Add_Decimals_Mixed()
        {
            //Arrange
            var frac1 = new Fraction(0.1m, 0.5m);
            var frac2 = new Fraction(0.45m, 0.25m);
            var expected = new Fraction(2, 1);

            //Act
            frac1.Add(frac2);

            //Assert
            Assert.AreEqual(expected.Numerator, frac1.Numerator);
            Assert.AreEqual(expected.Denominator, frac1.Denominator);
        }

        [Test]
        public void Add_Regular_1()
        {
            //Arrange
            var frac1 = new Fraction(1, 2);
            var frac2 = new Fraction(1, 2);
            var expected = new Fraction(1, 1);

            //Act
            frac1.Add(frac2);

            //Assert
            Assert.AreEqual(expected.Numerator, frac1.Numerator);
            Assert.AreEqual(expected.Denominator, frac1.Denominator);
        }

        [Test]
        public void Add_Regular_5_6()
        {
            //Arrange
            var frac1 = new Fraction(1, 3);
            var frac2 = new Fraction(1, 2);
            var expected = new Fraction(5, 6);

            //Act
            frac1.Add(frac2);

            //Assert
            Assert.AreEqual(expected.Numerator, frac1.Numerator);
            Assert.AreEqual(expected.Denominator, frac1.Denominator);
        }
    }
}