using Mathema.Algorithms.Helpers;
using Mathema.Algorithms.Parsers;
using NUnit.Framework;

namespace Mathema.RPNTests
{
    [TestFixture]
    public class AddTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Add_TwoSingleDigitIntegers()
        {
            //Arrange
            var text = "2 + 3";
            var output = " 2 3 +";

            //Act
            var rpn = RPNParser.Parse(text);

            //Assert
            Assert.IsTrue(RPNComparer.Compare(rpn, output));
        }
    }
}