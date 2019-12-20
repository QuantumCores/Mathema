using Mathema.Algorithms.Handlers;
using Mathema.Algorithms.Parsers;
using Mathema.Interfaces;
using NUnit.Framework;


namespace Tests
{
    [TestFixture]
    public class AddTests
    {
        [Test]
        public void AddIntegers()
        {
            //Arrange
            var text = "2+2";
            var expected = 4;

            //Act
            var rpn = RPNParser.Parse(text);
            var actual = ((INumberExpression)ExpressionBuilder.BuildFlat(rpn).Value()).Val;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void AddDoubles()
        {
            //Arrange
            var text = 2.2.ToString() + "+" + 4.3.ToString();
            var expected = 6.5;

            //Act
            var rpn = RPNParser.Parse(text);
            var actual = ((INumberExpression)ExpressionBuilder.Build(rpn).Value()).Val;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void AddDecimals()
        {
            //Arrange
            var text = 2.2m.ToString() + "+" + 4.3m.ToString();
            var expected = 6.5;

            //Act
            var rpn = RPNParser.Parse(text);
            var actual = ((INumberExpression)ExpressionBuilder.Build(rpn).Value()).Val;

            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}