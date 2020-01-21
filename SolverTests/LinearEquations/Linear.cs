using Mathema.Algorithms.Handlers;
using Mathema.Algorithms.Helpers;
using Mathema.Algorithms.Parsers;
using Mathema.Models.Equations;
using Mathema.Solver;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace SolverTests.LinearEquations
{
    [TestFixture]
    public class Linear
    {
        [Test]
        public void Linear_NumbersOnly()
        {
            //Arrange
            var text = "2*x - 4";
            var expression = ExpressionBuilder.BuildFlat(RPNParser.Parse(text).Output).Execute();
            var equation = new Equation(text, expression, null);

            //Act
            var sut = Solver.Solve(equation, "x");

            //Assert
            Assert.IsTrue(sut.Solutions.Count == 1);
            Assert.AreEqual(sut.Solutions[0].Count.Re.ToNumber(), 2);
        }

        [Test]
        public void Linear_WithVariable_1()
        {
            //Arrange
            var text = "2*x - y";
            var expText = "y/2";
            var expected = RPNParser.Parse(expText);
            var expression = ExpressionBuilder.BuildFlat(RPNParser.Parse(text).Output).Execute();
            var equation = new Equation(text, expression, null);

            //Act
            var sut = Solver.Solve(equation, "x");
            var actual = RPNParser.Parse(sut.Solutions[0].ToString());

            //Assert
            Assert.IsTrue(sut.Solutions.Count == 1);
            Assert.IsTrue(RPNComparer.Compare(expected.Output, actual.Output), $"Expected: {expText} but was {sut.Solutions[0].ToString()}");
        }

        [Test]
        public void Linear_WithVariable_2()
        {
            //Arrange
            var text = "y*x - 4";
            var expText = "4/y";
            var expected = RPNParser.Parse(expText);
            var expression = ExpressionBuilder.BuildFlat(RPNParser.Parse(text).Output).Execute();
            var equation = new Equation(text, expression, null);

            //Act
            var sut = Solver.Solve(equation, "x");
            var actual = RPNParser.Parse(sut.Solutions[0].ToString());

            //Assert
            Assert.IsTrue(sut.Solutions.Count == 1);
            Assert.IsTrue(RPNComparer.Compare(expected.Output, actual.Output), $"Expected: {expText} but was {sut.Solutions[0].ToString()}");
        }
    }
}
