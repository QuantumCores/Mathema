using Mathema.Algorithms.Handlers;
using Mathema.Algorithms.Parsers;
using System;

namespace Mathema
{
    class Program
    {
        static void Main(string[] args)
        {
            string del = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            Console.WriteLine("For help type '@h'");
            Console.WriteLine("Your decimal delimeter is '" + del + "'");


            var rpn = RPNParser.Parse("5,1 - 1 * 3");
            foreach (var d in rpn)
            {
                Console.Write(d.Value + " ");
            }
            var exp = ExpressionBuilder.Build(rpn);
            var res = exp.Value();
            Console.WriteLine("Result:" + res);

            Console.ReadLine();
        }
    }
}
