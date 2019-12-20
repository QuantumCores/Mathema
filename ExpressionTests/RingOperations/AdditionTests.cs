using Mathema.Algorithms.Handlers;
using Mathema.Algorithms.Parsers;
using Mathema.Interfaces;
using NUnit.Framework;


namespace ExpressionTests.RingOperations
{
    [TestFixture]
    public class AdditionTests
    {
        [Test]
        public void IsAssociative()
        {
            //Arrange
            var text1 = "1+(2+3)";
            var text2 = "(1+2)+3";
            var expected = 6;

            //Act
            var rpn1 = RPNParser.Parse(text1);
            var actual1 = ((INumberExpression)ExpressionBuilder.Build(rpn1.Output).Value()).Val;

            var rpn2 = RPNParser.Parse(text2);
            var actual2 = ((INumberExpression)ExpressionBuilder.Build(rpn2.Output).Value()).Val;

            //Assert
            Assert.AreEqual(expected, actual1);
            Assert.AreEqual(expected, actual2);
        }

        [Test]
        public void IsCommutative()
        {
            //Arrange
            var text1 = "1+2";
            var text2 = "2+1";
            var expected = 3;

            //Act
            var rpn1 = RPNParser.Parse(text1);
            var actual1 = ((INumberExpression)ExpressionBuilder.Build(rpn1.Output).Value()).Val;

            var rpn2 = RPNParser.Parse(text2);
            var actual2 = ((INumberExpression)ExpressionBuilder.Build(rpn2.Output).Value()).Val;

            //Assert
            Assert.AreEqual(expected, actual1);
            Assert.AreEqual(expected, actual2);
        }
    }
}
