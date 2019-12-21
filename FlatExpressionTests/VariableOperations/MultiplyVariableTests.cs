using Mathema.Algorithms.Handlers;
using Mathema.Algorithms.Helpers;
using Mathema.Algorithms.Parsers;
using Mathema.Interfaces;
using NUnit.Framework;

namespace FlatExpressionTests.VariableOperations
{
    [TestFixture]
    public class MultiplyVariableTests
    {
        [Test]
        public void Multiply_x_x()
        {
            //Arrange
            var text = "x*x";
            var expected = RPNParser.Parse("x*x");

            //Act
            var rpn = RPNParser.Parse(text);
            var actual = RPNParser.Parse(ExpressionBuilder.BuildFlat(rpn.Output).Execute().ToString());

            //Assert
            Assert.IsTrue(RPNComparer.Compare(expected.Output, actual.Output));
        }

        [Test]
        public void Multiply_x_y()
        {
            //Arrange
            var text = "x*y";
            var expected = RPNParser.Parse("x*y");

            //Act
            var rpn = RPNParser.Parse(text);
            var actual = RPNParser.Parse(ExpressionBuilder.BuildFlat(rpn.Output).Execute().ToString());

            //Assert
            Assert.IsTrue(RPNComparer.Compare(expected.Output, actual.Output));
        }

        [Test]
        public void Multiply_x_y_fun_x()
        {
            //Arrange
            var text = "x*y*fun*x";
            var expected = RPNParser.Parse("fun*x*x*y");

            //Act
            var rpn = RPNParser.Parse(text);
            var actual = RPNParser.Parse(ExpressionBuilder.BuildFlat(rpn.Output).Execute().ToString());

            //Assert
            Assert.IsTrue(RPNComparer.Compare(expected.Output, actual.Output));
        }
    }
}
