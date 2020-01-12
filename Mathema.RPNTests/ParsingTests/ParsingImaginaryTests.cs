using Mathema.Algorithms.Helpers;
using Mathema.Algorithms.Parsers;
using Mathema.Enums.Symbols;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mathema.RPNTests.ParsingTests
{
    [TestFixture]
    public class ParsingImaginaryTests
    {
        [Test]
        public void Parse_RecognizesImaginary()
        {
            //Arrange
            var text = "1 + i";
            var expected = "1i+";

            //Act
            var rpn = RPNParser.Parse(text);

            //Assert
            Assert.IsTrue(RPNComparer.Compare(rpn.Output, expected));
            Assert.IsTrue(rpn.Output[1].Type == SymbolTypes.Imaginary);
        }
    }
}
