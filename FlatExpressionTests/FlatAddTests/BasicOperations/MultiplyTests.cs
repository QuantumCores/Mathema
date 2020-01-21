using Mathema.Algorithms.Handlers;
using Mathema.Algorithms.Helpers;
using Mathema.Algorithms.Parsers;
using Mathema.Interfaces;
using NUnit.Framework;

namespace FlatExpressionTests.FlatAddTests.BasicOperations
{
    [TestFixture]
    public class MultiplyTests
    {
        [Test]
        public void Multiply_FlatAdd_FlatAdd_TheSame()
        {
            //Arrange
            var test = "(2 + x)*(2 + x)";
            var expText = "(2+x)^2";
            var expected = RPNParser.Parse(expText);

            //Act
            var rpn = RPNParser.Parse(test);
            var expr = ExpressionBuilder.BuildFlat(rpn.Output).Execute();
            var actual = RPNParser.Parse(expr.ToString());

            //Assert
            Assert.IsTrue(RPNComparer.Compare(expected.Output, actual.Output), $"Expected: {expText} but was {expr.ToString()}");
        }

        [Test]
        public void Multiply_FlatAdd_FlatAdd_Different()
        {
            //Arrange
            var test = "(2 + x)*(3 + 2*x)";
            var expText = "(2 + x)*(3 + 2*x)";
            var expected = RPNParser.Parse(expText);

            //Act
            var rpn = RPNParser.Parse(test);
            var expr = ExpressionBuilder.BuildFlat(rpn.Output).Execute();
            var actual = RPNParser.Parse(expr.ToString());

            //Assert
            Assert.IsTrue(RPNComparer.Compare(expected.Output, actual.Output), $"Expected: {expText} but was {expr.ToString()}");
        }

        [Test]
        public void Multiply_FlatAdd_Number()
        {
            //Arrange
            var test = "(2 + x)*2";
            var expText = "2*(2 + x)";
            var expected = RPNParser.Parse(expText);

            //Act
            var rpn = RPNParser.Parse(test);
            var expr = ExpressionBuilder.BuildFlat(rpn.Output).Execute();
            var actual = RPNParser.Parse(expr.ToString());

            //Assert
            Assert.IsTrue(RPNComparer.Compare(expected.Output, actual.Output), $"Expected: {expText} but was {expr.ToString()}");
        }
    }
}
