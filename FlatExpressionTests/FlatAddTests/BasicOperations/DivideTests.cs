using Mathema.Algorithms.Handlers;
using Mathema.Algorithms.Helpers;
using Mathema.Algorithms.Parsers;
using Mathema.Interfaces;
using NUnit.Framework;

namespace FlatExpressionTests.FlatAddTests.BasicOperations
{
    [TestFixture]
    public class DivideTests
    {
        [Test]
        public void Divide_FlatAdd_FlatAdd()
        {
            //Arrange
            var test = "(2 + x)/(2 + x)";
            var expText = "1";
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
