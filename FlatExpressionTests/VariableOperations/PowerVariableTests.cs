using Mathema.Algorithms.Handlers;
using Mathema.Algorithms.Helpers;
using Mathema.Algorithms.Parsers;
using Mathema.Interfaces;
using NUnit.Framework;

namespace FlatExpressionTests.VariableOperations
{
    [TestFixture]
    public class PowerVariableTests
    {
        [Test]
        public void Power_x2()
        {
            //Arrange
            var text = "x^2";
            var expected = RPNParser.Parse("x*x");

            //Act
            var rpn = RPNParser.Parse(text);
            var the = ExpressionBuilder.BuildFlat(rpn.Output).Execute().ToString();
            var actual = RPNParser.Parse(ExpressionBuilder.BuildFlat(rpn.Output).Execute().ToString());

            //Assert
            Assert.IsTrue(RPNComparer.Compare(expected.Output, actual.Output));
        }

        [Test]
        public void Power_xy2()
        {
            //Arrange
            var text = "x*y^2";
            var expected = RPNParser.Parse("x*y*y");

            //Act
            var rpn = RPNParser.Parse(text);
            var actual = RPNParser.Parse(ExpressionBuilder.BuildFlat(rpn.Output).Execute().ToString());

            //Assert
            Assert.IsTrue(RPNComparer.Compare(expected.Output, actual.Output));
        }

        [Test]
        public void Power_x2y2()
        {
            //Arrange
            var text = "x^2*y^2";
            var expected = RPNParser.Parse("x*x*y*y");

            //Act
            var rpn = RPNParser.Parse(text);
            var the = ExpressionBuilder.BuildFlat(rpn.Output).Execute().ToString();
            var actual = RPNParser.Parse(ExpressionBuilder.BuildFlat(rpn.Output).Execute().ToString());

            //Assert
            Assert.IsTrue(RPNComparer.Compare(expected.Output, actual.Output));
        }

        [Test]
        public void Power_xAndy2()
        {
            //Arrange
            var text = "(x*y)^2";
            var expected = RPNParser.Parse("x*x*y*y");

            //Act
            var rpn = RPNParser.Parse(text);
            var the = ExpressionBuilder.BuildFlat(rpn.Output).Execute().ToString();
            var actual = RPNParser.Parse(ExpressionBuilder.BuildFlat(rpn.Output).Execute().ToString());

            //Assert
            Assert.IsTrue(RPNComparer.Compare(expected.Output, actual.Output));
        }       
    }
}
