using Mathema.Algorithms.Handlers;
using Mathema.Algorithms.Parsers;
using NUnit.Framework;

namespace ExpressionTests.BasicOperations
{
    [TestFixture]
    public class DivideTests
    {
        [Test]
        public void DivideIntegers()
        {
            //Arrange
            var text = 10.ToString() + "/" + 4.ToString();
            var expected = 2.5;

            //Act
            var rpn = RPNParser.Parse(text);
            var actual = ExpressionBuilder.Build(rpn).Value();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void DivideDoubles()
        {
            //Arrange
            var text = 7.5.ToString() + "/" + 2.5.ToString();
            var expected = 3;

            //Act
            var rpn = RPNParser.Parse(text);
            var actual = ExpressionBuilder.Build(rpn).Value();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void DivideDecimals()
        {
            //Arrange
            var text = 7.5d.ToString() + "/" + 2.5d.ToString();
            var expected = 3;

            //Act
            var rpn = RPNParser.Parse(text);
            var actual = ExpressionBuilder.Build(rpn).Value();

            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
