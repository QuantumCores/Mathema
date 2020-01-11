using Mathema.Algorithms.Handlers;
using Mathema.Algorithms.Helpers;
using Mathema.Algorithms.Parsers;
using Mathema.Enums.DimensionKeys;
using Mathema.Models.Expressions;
using Mathema.Models.FlatExpressions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlatExpressionTests.VariableTests
{
    [TestFixture]
    public class FlatMultVariableTests
    {
        [Test]
        public void CreatesFlatExpression()
        {
            //Arrange
            var text = "2 * x * y";

            //Act
            var rpn = RPNParser.Parse(text);
            var actual = ExpressionBuilder.BuildFlat(rpn.Output).Execute();

            //Assert
            Assert.IsTrue(actual is FlatMultExpression);
        }

        [Test]
        public void RecognizesVariable()
        {
            //Arrange
            var text = "2 * x * y";
            var expected = new FlatMultExpression();
            expected.Add(new NumberExpression(2));
            expected.Add(new VariableExpression("x", 1m));

            //Act
            var rpn = RPNParser.Parse(text);
            var actual = (FlatMultExpression)(ExpressionBuilder.BuildFlat(rpn.Output).Execute());

            //Assert
            Assert.IsTrue(actual.Count.Re.ToNumber() == 2);
            Assert.IsTrue(actual.Expressions.ContainsKey("x"));
            Assert.IsTrue(actual.Expressions.ContainsKey("y"));
        }

        [Test]
        public void Squash_Numbers()
        {
            //Arrange
            var test = "2 * x  *3";
            var expText = "6 * x";
            var expected = RPNParser.Parse(expText);

            //Act
            var rpn = RPNParser.Parse(test);
            var expr = ExpressionBuilder.BuildFlat(rpn.Output).Execute();
            var actual = RPNParser.Parse(expr.ToString());

            //Assert
            Assert.IsTrue(RPNComparer.Compare(expected.Output, actual.Output), $"Expected: {expText} but was {expr.ToString()}");
        }

        [Test]
        public void Squash_OneVariable()
        {

            //Arrange
            var test = "2 * x  * 3 * x";
            var expText = "( 6 * x * x)";
            var expected = RPNParser.Parse(expText);

            //Act
            var rpn = RPNParser.Parse(test);
            var expr = ExpressionBuilder.BuildFlat(rpn.Output).Execute();
            var actual = RPNParser.Parse(expr.ToString());

            //Assert
            Assert.IsTrue(RPNComparer.Compare(expected.Output, actual.Output), $"Expected: {expText} but was {expr.ToString()}");
        }

        [Test]
        public void Value_Squash_TwoVariables()
        {
            //Arrange
            var text = "y * 2 * x  * 3 * x * y * y";
            var expText = "( 6 * x * x * y * y * y)";
            var expected = RPNParser.Parse(expText);

            //Act
            var rpn = RPNParser.Parse(text);
            var expr = ExpressionBuilder.BuildFlat(rpn.Output).Execute();
            var actual = RPNParser.Parse(expr.ToString());

            //Assert
            Assert.IsTrue(RPNComparer.Compare(expected.Output, actual.Output), $"Expected: {expText} but was {expr.ToString()}");
        }


    }
}
