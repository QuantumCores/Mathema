using Mathema.Algorithms.Handlers;
using Mathema.Algorithms.Helpers;
using Mathema.Algorithms.Parsers;
using Mathema.Interfaces;
using NUnit.Framework;


namespace FlatExpressionTests.FlatAddTests.BasicOperations
{
    [TestFixture]
    public class AddTests
    {
        [Test]
        public void Add_FlatAdd_Variable()
        {
            //Arrange
            var text = "2 + x + x^2 + x";
            var expected = RPNParser.Parse("x*x + 2*x + 2");

            //Act
            var rpn = RPNParser.Parse(text);
            var expr = ExpressionBuilder.BuildFlat(rpn.Output).Execute();
            var actual = RPNParser.Parse(expr.ToString());

            //Assert
            Assert.IsTrue(RPNComparer.Compare(expected.Output, actual.Output));
        }

        [Test]
        public void AddIntegers()
        {
            //Arrange
            var text = "2+2";
            var expected = 4;

            //Act
            var rpn = RPNParser.Parse(text);
            var actual = ExpressionBuilder.BuildFlat(rpn.Output).Execute().Count.ToNumber();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void AddDoubles()
        {
            //Arrange
            var text = 2.2.ToString() + "+" + 4.3.ToString();
            var expected = 6.5;

            //Act
            var rpn = RPNParser.Parse(text);
            var actual = ExpressionBuilder.BuildFlat(rpn.Output).Execute().Count.ToNumber();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void AddDecimals()
        {
            //Arrange
            var text = 2.2m.ToString() + "+" + 4.3m.ToString();
            var expected = 6.5;

            //Act
            var rpn = RPNParser.Parse(text);
            var actual = ExpressionBuilder.BuildFlat(rpn.Output).Execute().Count.ToNumber();

            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}