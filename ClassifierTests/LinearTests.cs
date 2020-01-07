using Mathema.Algorithms.Handlers;
using Mathema.Algorithms.Parsers;
using Mathema.Classifier;
using Mathema.Enums.Equations;
using Mathema.Models.Equations;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassifierTests
{
    [TestFixture]
    public class LinearTests
    {
        [Test]
        public void Linear_1()
        {
            //Arrange
            var text = "2*x^2 + 3*x - 2*x^2";
            var expression = ExpressionBuilder.BuildFlat(RPNParser.Parse(text).Output).Execute();
            var equation = new Equation(text, expression, null);

            //Act
            var sut = EquationClassifier.Classify(equation);

            //Assert
            Assert.AreEqual(EquationTypes.Linear, sut.Type);
        }
    }
}
