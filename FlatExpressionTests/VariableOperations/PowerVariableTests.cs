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

        [Test]
        public void Power_2On_FlatAdd_x_y()
        {
            //Arrange
            var text = "(x+y)^2";
            var expected = RPNParser.Parse("x*x + 2*x*y + y*y");

            //Act
            var rpn = RPNParser.Parse(text);
            var the = ExpressionBuilder.BuildFlat(rpn.Output).Execute().ToString();
            var actual = RPNParser.Parse(ExpressionBuilder.BuildFlat(rpn.Output).Execute().ToString());

            //Assert
            Assert.IsTrue(RPNComparer.Compare(expected.Output, actual.Output));
        }

        [Test]
        public void Power_2On_FlatAdd_x_y_z()
        {
            //Arrange
            var text = "(x+y+z)^2";
            var expected = RPNParser.Parse("x*x + 2*x*y + 2*x*z + y*y + 2*y*z + z*z");

            //Act
            var rpn = RPNParser.Parse(text);
            var the = ExpressionBuilder.BuildFlat(rpn.Output).Execute().ToString();
            var actual = RPNParser.Parse(ExpressionBuilder.BuildFlat(rpn.Output).Execute().ToString());

            //Assert
            Assert.IsTrue(RPNComparer.Compare(expected.Output, actual.Output));
        }

        [Test]
        public void Power_3On_FlatAdd_x_y()
        {
            //Arrange
            var text = "(x+y)^3";
            var expected = RPNParser.Parse("x*x*x + 3*x*x*y + 3*x*y*y + y*y*y");

            //Act
            var rpn = RPNParser.Parse(text);
            var the = ExpressionBuilder.BuildFlat(rpn.Output).Execute().ToString();
            var actual = RPNParser.Parse(ExpressionBuilder.BuildFlat(rpn.Output).Execute().ToString());

            //Assert
            Assert.IsTrue(RPNComparer.Compare(expected.Output, actual.Output));
        }
    }
}
