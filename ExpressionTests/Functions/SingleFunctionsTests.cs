using Mathema.Algorithms.Handlers;
using Mathema.Algorithms.Parsers;
using Mathema.Interfaces;
using NUnit.Framework;
using System;

namespace ExpressionTests.Functions
{
    [TestFixture]
    public class SingleFunctionsTests
    {
        [Test]
        public void ParseSin()
        {
            //Arrange
            var rad = 12.243; 
            var text = "Sin(" + rad.ToString() +")";
            var expected = (decimal)Math.Sin(rad);

            //Act
            var rpn = RPNParser.Parse(text);
            var actual = ((INumberExpression)ExpressionBuilder.Build(rpn).Value()).Val;

            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
