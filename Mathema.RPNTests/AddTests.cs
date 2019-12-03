using Mathema.Algorithms.Helpers;
using Mathema.Algorithms.Parsers;
using NUnit.Framework;

namespace Mathema.RPNTests
{
    [TestFixture]
    public class AddTests
    {

        string d = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Add_TwoSingleDigitIntegers()
        {
            //Arrange
            var text = "2 + 3";
            var output = " 2 3 +";

            //Act
            var rpn = RPNParser.Parse(text);

            //Assert
            Assert.IsTrue(RPNComparer.Compare(rpn, output));
        }

        [Test]
        public void Add_TwoTwoDigitIntegers()
        {
            //Arrange
            var text = "12 + 13";
            var output = " 12 13 +";

            //Act
            var rpn = RPNParser.Parse(text);

            //Assert
            Assert.IsTrue(RPNComparer.Compare(rpn, output));
        }

        [Test]
        public void Add_TwoFloatingNumbers()
        {
            //Arrange
            var text = 12.5.ToString() + "+" + 13.5.ToString();
            var output = 12.5.ToString() + 13.5.ToString() + "+";

            //Act
            var rpn = RPNParser.Parse(text);

            //Assert
            Assert.IsTrue(RPNComparer.Compare(rpn, output));
        }
    }
}