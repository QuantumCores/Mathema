using Mathema.Interfaces;
using Mathema.Models.Equations;
using Mathema.Models.FlatExpressions;
using System;
using System.Linq;

namespace Mathema.Classifier
{
    public class EquationClassifier
    {
        private Equation equation;

        public EquationClassifier(Equation equation)
        {
            this.equation = equation;
        }

        public void Classify()
        {
            this.Classify(this.equation.Left, this.equation.Right);
        }

        public static void Classify(Equation equation)
        {
            var ec = new EquationClassifier(equation);
            ec.Classify();
        }

        private void Classify(IExpression left, IExpression right)
        {
            if (left is FlatMultExpression e)
            {
                var ord = e.Expressions.OrderBy(x => x.Value);
            }
            else if (left is FlatAddExpression e)
            {
                e.Expressions.OrderBy()
            }
        }
    }
}
