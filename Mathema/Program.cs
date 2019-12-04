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
            //var output = Parser.Convert("3 + 4 * (2 - 3)");
            var equation = "";
            do
            {
                equation = Console.ReadLine();
                DoWork(equation);
                Console.WriteLine();
                Console.WriteLine();

            } while (!string.IsNullOrWhiteSpace(equation));


            Console.ReadLine();
        }

        private static void DoWork(string equation)
        {
            var output = RPNParser.Parse(equation);
            foreach (var o in output)
            {
                Console.Write(o.Value + ",");
            }

            var expr = ExpressionBuilder.Build(output);
            Console.WriteLine();
            Console.WriteLine(expr.ToString());
            Console.WriteLine();
            Console.WriteLine(expr.Value());

        }
    }
}
