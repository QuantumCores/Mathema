using Mathema.Algorithms.Helpers;
using Mathema.Algorithms.Parsers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mathema.RPNTests
{
    [TestFixture]
    public class PrecedenceTests
    {
        [Test]
        public void AddThenMultiply()
        {
            //Arrange
            var text = "2 + 3 * 4";
            var output = " 2 3 4 * +";

            //Act
            var rpn = RPNParser.Parse(text);

            //Assert
            Assert.IsTrue(RPNComparer.Compare(rpn, output));
        }

        [Test]
        public void AddThenMultiplyThenAdd()
        {
            //Arrange
            var text = "2 + 3 * 4 + 1";
            var output = " 2 3 4 * + 1 +";

            //Act
            var rpn = RPNParser.Parse(text);

            //Assert
            Assert.IsTrue(RPNComparer.Compare(rpn, output));
        }
    }
}
