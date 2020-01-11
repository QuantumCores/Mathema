using Mathema.Algorithms.Helpers;
using Mathema.Algorithms.Parsers;
using Mathema.Enums.Symbols;
using NUnit.Framework;


namespace Mathema.RPNTests.ParsingTests
{
    [TestFixture]
    public class ParsingFunctions
    {
        [Test]
        public void Parse_Sin()
        {
            //Arrange            
            var text = "Sin("+3.14m.ToString() +")";
            var expected = 3.14m.ToString() +" sin";

            //Act
            var rpn = RPNParser.Parse(text);

            //Assert
            Assert.IsTrue(RPNComparer.Compare(rpn.Output, expected));
            Assert.IsTrue(rpn.Output[1].Type == SymbolTypes.Function);
        }

        [Test]
        public void Parse_Cos()
        {
            //Arrange
            var text = "Cos(" + 3.14m.ToString() + ")";
            var expected = 3.14m.ToString() + " cos";

            //Act
            var rpn = RPNParser.Parse(text);

            //Assert
            Assert.IsTrue(RPNComparer.Compare(rpn.Output, expected));
            Assert.IsTrue(rpn.Output[1].Type == SymbolTypes.Function);
        }

        [Test]
        public void Parse_Tan()
        {
            //Arrange
            var text = "Tan(" + 3.14m.ToString() + ")";
            var expected = 3.14m.ToString() + " tan";

            //Act
            var rpn = RPNParser.Parse(text);

            //Assert
            Assert.IsTrue(RPNComparer.Compare(rpn.Output, expected));
            Assert.IsTrue(rpn.Output[1].Type == SymbolTypes.Function);
        }

        [Test]
        public void Parse_Cot()
        {
            //Arrange
            var text = "Cot(" + 3.14m.ToString() + ")";
            var expected = 3.14m.ToString() + " cot";

            //Act
            var rpn = RPNParser.Parse(text);

            //Assert
            Assert.IsTrue(RPNComparer.Compare(rpn.Output, expected));
            Assert.IsTrue(rpn.Output[1].Type == SymbolTypes.Function);
        }

        [Test]
        public void Parse_Log()
        {
            //Arrange
            var text = "Log(" + 3.14m.ToString() + ")";
            var expected = 3.14m.ToString() + " log";

            //Act
            var rpn = RPNParser.Parse(text);

            //Assert
            Assert.IsTrue(RPNComparer.Compare(rpn.Output, expected));
            Assert.IsTrue(rpn.Output[1].Type == SymbolTypes.Function);
        }
    }
}
