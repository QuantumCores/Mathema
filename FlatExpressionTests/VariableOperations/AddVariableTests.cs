using Mathema.Algorithms.Handlers;
using Mathema.Algorithms.Helpers;
using Mathema.Algorithms.Parsers;
using Mathema.Interfaces;
using NUnit.Framework;


namespace FlatExpressionTests.VariableOperations
{
    [TestFixture]
    public class AddVariableTests
    {
        [Test]
        public void Add_x_x()
        {
            //Arrange
            var text = "x+x";
            var expected = RPNParser.Parse("2*x");

            //Act
            var rpn = RPNParser.Parse(text);
            var actual = RPNParser.Parse(ExpressionBuilder.BuildFlat(rpn.Output).Execute().ToString());

            //Assert
            Assert.IsTrue(RPNComparer.Compare(expected.Output, actual.Output));
        }

        [Test]
        public void Add_x_y()
        {
            //Arrange
            var text = "x + x + y";
            var expected = RPNParser.Parse("2*x+y");

            //Act
            var rpn = RPNParser.Parse(text);
            var actual = RPNParser.Parse(ExpressionBuilder.BuildFlat(rpn.Output).Execute().ToString());

            //Assert
            Assert.IsTrue(RPNComparer.Compare(expected.Output, actual.Output));
        }

        [Test]
        public void Add_x_y_fun()
        {
            //Arrange
            var text = "x + x + y + fun + fun";
            var expected = RPNParser.Parse("2*fun + 2*x + y");

            //Act
            var rpn = RPNParser.Parse(text);
            var actual = RPNParser.Parse(ExpressionBuilder.BuildFlat(rpn.Output).Execute().ToString());

            //Assert
            Assert.IsTrue(RPNComparer.Compare(expected.Output, actual.Output));
        }
    }
}