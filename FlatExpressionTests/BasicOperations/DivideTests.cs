using Mathema.Algorithms.Handlers;
using Mathema.Algorithms.Parsers;
using Mathema.Interfaces;
using NUnit.Framework;

namespace FlatExpressionTests.BasicOperations
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
            var actual = ((INumberExpression)ExpressionBuilder.Build(rpn.Output).Value()).Val;

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
            var actual = ((INumberExpression)ExpressionBuilder.Build(rpn.Output).Value()).Val;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void DivideDecimals()
        {
            //Arrange
            var text = 7.5m.ToString() + "/" + 2.5m.ToString();
            var expected = 3;

            //Act
            var rpn = RPNParser.Parse(text);
            var actual = ((INumberExpression)ExpressionBuilder.Build(rpn.Output).Value()).Val;

            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
