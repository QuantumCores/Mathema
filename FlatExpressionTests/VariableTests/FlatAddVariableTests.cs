using Mathema.Algorithms.Handlers;
using Mathema.Algorithms.Parsers;
using Mathema.Enums.DimensionKeys;
using Mathema.Interfaces;
using Mathema.Models.Expressions;
using Mathema.Models.FlatExpressions;
using NUnit.Framework;
using System;

namespace FlatExpressionTests.VariableTests
{
    [TestFixture]
    public class FlatAddVariableTests
    {
        [Test]
        public void CreatesFlatExpression()
        {
            //Arrange
            var text = "2 - x";

            //Act
            var rpn = RPNParser.Parse(text);
            var actual = ExpressionBuilder.BuildFlat(rpn.Output).Execute();

            //Assert
            Assert.IsTrue(actual is FlatAddExpression);
        }

        [Test]
        public void RecognizesVariable()
        {
            //Arrange
            var text = "2 + x";
            var expected = new FlatAddExpression();
            expected.Add(new NumberExpression(1));
            var x = new VariableExpression("x", 1m);
            expected.Add(new UnaryExpression(Mathema.Enums.Operators.OperatorTypes.Sign, x));

            //Act
            var rpn = RPNParser.Parse(text);
            var actual = (FlatAddExpression)(ExpressionBuilder.BuildFlat(rpn.Output).Execute());

            //Assert
            Assert.IsTrue(actual.Expressions.ContainsKey(Dimensions.Number));
            Assert.IsTrue(actual.Expressions.ContainsKey("x"));
        }

        [Test]
        public void Squash_Numbers()
        {
            //Arrange
            var text = "2 - x  +1";
            var expected = "( 3 + -x)";

            //Act
            var rpn = RPNParser.Parse(text);
            var actual = ExpressionBuilder.BuildFlat(rpn.Output).Execute().ToString();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Squash_OneVariable()
        {
            //Arrange
            var text = "2 - x  + 1 - x";
            var expected = "( 3 + -2 * x)";

            //Act
            var rpn = RPNParser.Parse(text);
            var actual = ExpressionBuilder.BuildFlat(rpn.Output).Execute().ToString();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Value_Squash_TwoVariables()
        {
            //Arrange
            var text = "y + 2 - x  + 1 - x + y + y";
            var expected = "( 3 + -2 * x + 3 * y)";

            //Act
            var rpn = RPNParser.Parse(text);
            var actual = ExpressionBuilder.BuildFlat(rpn.Output).Execute().ToString();

            //Assert
            Assert.AreEqual(expected, actual);
        }

    }
}
