using Mathema.Algorithms.Handlers;
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
        public void Linear_1()
        {
            //Arrange
            var text = "2*x - 4";
            var expression = ExpressionBuilder.BuildFlat(RPNParser.Parse(text).Output).Execute();
            var equation = new Equation(text, expression, null);

            //Act
            var sut = Solver.Solve(equation);

            //Assert
            Assert.IsTrue(sut.Solutions.Count == 1);
            Assert.AreEqual(sut.Solutions[0].Count.Re.ToNumber(), 2);
        }
    }
}
