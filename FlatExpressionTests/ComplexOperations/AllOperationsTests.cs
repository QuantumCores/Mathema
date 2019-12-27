using Mathema.Algorithms.Handlers;
using Mathema.Algorithms.Helpers;
using Mathema.Algorithms.Parsers;
using Mathema.Interfaces;
using NUnit.Framework;
using System;

namespace FlatExpressionTests.ComplexOperations
{
    [TestFixture]
    public class FlatExpressionValueTests
    {
        [Test]
        public void AllOperators()
        {
            //Arrange
            var text = "(4- 6 * -2 )^1/(2 + 2)^(1/2)";
            var expected = 8;

            //Act
            var rpn = RPNParser.Parse(text);
            var actual = ExpressionBuilder.BuildFlat(rpn.Output).Execute().Count.ToNumber();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void AllOperatorsAndFunctions()
        {
            //Arrange
            var text = "Sin((4- 6 * -2 )^1/(2 + 2)^(1/2))";
            var expected = (decimal)Math.Sin(8);

            //Act
            var rpn = RPNParser.Parse(text);
            var actual = ExpressionBuilder.BuildFlat(rpn.Output).Execute().Count.ToNumber();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void VariableWithFunction()
        {
            //Arrange
            var text = "Sin(x) + x + Sin(x)";
            var expected = "2 * Sin(x) + x";

            //Act
            var rpn = RPNParser.Parse(text);
            var actual = ExpressionBuilder.BuildFlat(rpn.Output).Execute().ToString();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void VariableWithAddAndDivide()
        {
            //Arrange
            var text = "2*x + 7 -2 + 2*x*x/x"; //+ 2*x*x/x
            var expected = RPNParser.Parse("4*x + 5");

            //Act
            var rpn = RPNParser.Parse(text);
            var the = ExpressionBuilder.BuildFlat(rpn.Output).Execute();
            var actual = RPNParser.Parse(ExpressionBuilder.BuildFlat(rpn.Output).Execute().ToString());

            //Assert
            Assert.IsTrue(RPNComparer.Compare(actual.Output, expected.Output));
        }
    }
}
