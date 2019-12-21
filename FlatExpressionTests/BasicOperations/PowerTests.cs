using Mathema.Algorithms.Handlers;
using Mathema.Algorithms.Parsers;
using Mathema.Interfaces;
using NUnit.Framework;

namespace FlatExpressionTests.BasicOperations
{
    [TestFixture]
    public class PowerTests
    {
        [Test]
        public void PowIntegers()
        {
            //Arrange
            var text = "2^2";
            var expected = 4;

            //Act
            var rpn = RPNParser.Parse(text);
            var actual = ExpressionBuilder.BuildFlat(rpn.Output).Execute().Count.ToNumber();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void PowDoubles()
        {
            //Arrange
            var text = 2.0.ToString() + "^" + 3.0.ToString();
            var expected = 8;

            //Act
            var rpn = RPNParser.Parse(text);
            var actual = ExpressionBuilder.BuildFlat(rpn.Output).Execute().Count.ToNumber();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void PowDecimals()
        {
            //Arrange
            var text = 2m.ToString() + "^" + 4m.ToString();
            var expected = 16;

            //Act
            var rpn = RPNParser.Parse(text);
            var actual = ExpressionBuilder.BuildFlat(rpn.Output).Execute().Count.ToNumber();

            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
