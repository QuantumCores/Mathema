using Mathema.Algorithms.Handlers;
using Mathema.Algorithms.Parsers;
using NUnit.Framework;

namespace ExpressionTests.BasicOperations
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
            var actual = ExpressionBuilder.Build(rpn).Value();

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
            var actual = ExpressionBuilder.Build(rpn).Value();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void AddDecimals()
        {
            //Arrange
            var text = 2.2d.ToString() + "+" + 4.3d.ToString();
            var expected = 6.5;

            //Act
            var rpn = RPNParser.Parse(text);
            var actual = ExpressionBuilder.Build(rpn).Value();

            //Assert
            Assert.AreEqual(expected, actual);
        }       
    }
}