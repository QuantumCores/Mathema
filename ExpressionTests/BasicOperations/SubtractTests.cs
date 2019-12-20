using Mathema.Algorithms.Handlers;
using Mathema.Algorithms.Parsers;
using Mathema.Interfaces;
using NUnit.Framework;

namespace ExpressionTests.BasicOperations
{
    [TestFixture]
    public class SubtractTests
    {
        [Test]
        public void SubtractIntegers()
        {
            //Arrange
            var text = "4-6";
            var expected = -2m;

            //Act
            var rpn = RPNParser.Parse(text);
            var actual = ExpressionBuilder.Build(rpn.Output).Value().Count.ToNumber();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SubtractDoubles()
        {
            //Arrange
            var text = 5.2.ToString() + "-" + 1.3.ToString();
            var expected = 3.9m;

            //Act
            var rpn = RPNParser.Parse(text);
            var actual = ExpressionBuilder.Build(rpn.Output).Value().Count.ToNumber();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SubtractDecimals()
        {
            //Arrange
            var text = 5.2m.ToString() + "-" + 1.3m.ToString();
            var expected = 3.9m;

            //Act
            var rpn = RPNParser.Parse(text);
            var actual = ExpressionBuilder.Build(rpn.Output).Value().Count.ToNumber();

            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
