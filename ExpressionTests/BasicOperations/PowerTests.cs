using Mathema.Algorithms.Handlers;
using Mathema.Algorithms.Parsers;
using NUnit.Framework;

namespace ExpressionTests.BasicOperations
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
            var actual = ExpressionBuilder.Build(rpn).Value();

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
            var actual = ExpressionBuilder.Build(rpn).Value();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void PowDecimals()
        {
            //Arrange
            var text = 2d.ToString() + "^" + 4d.ToString();
            var expected = 16;

            //Act
            var rpn = RPNParser.Parse(text);
            var actual = ExpressionBuilder.Build(rpn).Value();

            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
