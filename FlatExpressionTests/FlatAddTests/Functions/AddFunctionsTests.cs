using Mathema.Algorithms.Handlers;
using Mathema.Algorithms.Helpers;
using Mathema.Algorithms.Parsers;
using Mathema.Interfaces;
using NUnit.Framework;


namespace FlatExpressionTests.FlatAddTests.Functions
{
    [TestFixture]
    public class AddFunctionsTests
    {
        [Test]
        public void Add_FlatAdd_2Sin()
        {
            //Arrange
            var text = "Sin(x) + 2 + x + Sin(x)";
            var expText = "x + 2 + 2*Sin(x)";
            var expected = RPNParser.Parse(expText);

            //Act
            var rpn = RPNParser.Parse(text);
            var expr = ExpressionBuilder.BuildFlat(rpn.Output).Execute();
            var actual = RPNParser.Parse(expr.ToString());

            //Assert
            Assert.IsTrue(RPNComparer.Compare(expected.Output, actual.Output), $"Expected: {expText} but was {expr.ToString()}");
        }

        [Test]
        public void Add_FlatAdd_Sinx_Sin2x()
        {
            //Arrange
            var test = "Sin(x) + 2 + x + Sin(2*x)";
            var expText = "x + 2 + Sin(x) + Sin(2*x)";
            var expected = RPNParser.Parse(expText);

            //Act
            var rpn = RPNParser.Parse(test);
            var expr = ExpressionBuilder.BuildFlat(rpn.Output).Execute();
            var actual = RPNParser.Parse(expr.ToString());

            //Assert
            Assert.IsTrue(RPNComparer.Compare(expected.Output, actual.Output), $"Expected: {expText} but was {expr.ToString()}");
        }

        [Test]
        public void Add_FlatAdd_Sinx_Cos2x()
        {
            //Arrange
            var test = "Sin(x) + 2 + x + Cos(2*x)";
            var expText = "x + 2 + Sin(x) + Cos(2*x)";
            var expected = RPNParser.Parse(expText);

            //Act
            var rpn = RPNParser.Parse(test);
            var expr = ExpressionBuilder.BuildFlat(rpn.Output).Execute();
            var actual = RPNParser.Parse(expr.ToString());

            //Assert
            Assert.IsTrue(RPNComparer.Compare(expected.Output, actual.Output), $"Expected: {expText} but was {expr.ToString()}");
        }

        [Test]
        public void Add_FlatAdd_Sinx_Tan2x()
        {
            //Arrange
            var test = "Sin(x) + 2 + x + Tan(2*x)";
            var expText = "x + 2 + Sin(x) + Tan(2*x)";
            var expected = RPNParser.Parse(expText);

            //Act
            var rpn = RPNParser.Parse(test);
            var expr = ExpressionBuilder.BuildFlat(rpn.Output).Execute();
            var actual = RPNParser.Parse(expr.ToString());

            //Assert
            Assert.IsTrue(RPNComparer.Compare(expected.Output, actual.Output), $"Expected: {expText} but was {expr.ToString()}");
        }

        [Test]
        public void Add_FlatAdd_Sinx_CTan2x()
        {
            //Arrange
            var test = "Sin(x) + 2 + x + Ctan(2*x)";
            var expText = "x + 2 + Sin(x) + Ctan(2*x)";
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