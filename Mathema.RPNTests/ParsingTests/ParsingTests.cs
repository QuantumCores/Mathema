using Mathema.Algorithms.Helpers;
using Mathema.Algorithms.Parsers;
using NUnit.Framework;

namespace Mathema.RPNTests.ParsingTests
{
    [TestFixture]
    public class ParsingTests
    {
        string d = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

        [Test]
        public void Add_TwoSingleDigitIntegers()
        {
            //Arrange
            var text = "2 + 3";
            var expected = " 2 3 +";

            //Act
            var rpn = RPNParser.Parse(text);

            //Assert
            Assert.IsTrue(RPNComparer.Compare(rpn.Output, expected));
        }

        [Test]
        public void Add_TwoTwoDigitIntegers()
        {
            //Arrange
            var text = "12 + 13";
            var expected = " 12 13 +";

            //Act
            var rpn = RPNParser.Parse(text);

            //Assert
            Assert.IsTrue(RPNComparer.Compare(rpn.Output, expected));
        }

        [Test]
        public void Add_TwoFloatingNumbers()
        {
            //Arrange
            var text = 12.5.ToString() + "+" + 13.5.ToString();
            var expected = 12.5.ToString() + 13.5.ToString() + "+";

            //Act
            var rpn = RPNParser.Parse(text);

            //Assert
            Assert.IsTrue(RPNComparer.Compare(rpn.Output, expected));
        }
    }
}