using Mathema.Algorithms.Helpers;
using Mathema.Algorithms.Parsers;
using NUnit.Framework;


namespace Mathema.RPNTests.ParsingTests
{
    [TestFixture]
    public class ParsingFunctions
    {
        [Test]
        public void ParseSin()
        {
            //Arrange
            
            var text = "Sin("+3.14m.ToString() +")";
            var expected = 3.14m.ToString() +" sin";

            //Act
            var rpn = RPNParser.Parse(text);

            //Assert
            Assert.IsTrue(RPNComparer.Compare(rpn, expected));
        }
    }
}
