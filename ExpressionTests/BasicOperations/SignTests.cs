using Mathema.Algorithms.Handlers;
using Mathema.Algorithms.Parsers;
using NUnit.Framework;

namespace ExpressionTests.BasicOperations
{
    [TestFixture]
    public class SignTests
    {
        [Test]
        public void ChangeOnly()
        {
            //Arrange
            var text = "-6";
            var expected = -6;

            //Act
            var rpn = RPNParser.Parse(text);
            var actual = ExpressionBuilder.Build(rpn).Value();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ChangeWhenAdding()
        {
            //Arrange
            var text = "12+-6";
            var expected = 6;

            //Act
            var rpn = RPNParser.Parse(text);
            var actual = ExpressionBuilder.Build(rpn).Value();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ChangeWhenSubtracting()
        {
            //Arrange
            var text = "12--6";
            var expected = 18;

            //Act
            var rpn = RPNParser.Parse(text);
            var actual = ExpressionBuilder.Build(rpn).Value();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ChangeWhenMultiplying()
        {
            //Arrange
            var text = "3*-6";
            var expected = 18;

            //Act
            var rpn = RPNParser.Parse(text);
            var actual = ExpressionBuilder.Build(rpn).Value();

            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
