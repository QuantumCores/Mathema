using Mathema.Algorithms.Handlers;
using Mathema.Algorithms.Parsers;
using Mathema.Texts;
using System;
using System.Linq;

namespace Mathema
{
    class Program
    {
        static void Main(string[] args)
        {
            string del = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            Console.WriteLine("For help type '@h'");
            Console.WriteLine("Your decimal delimeter is '" + del + "'");

            var line = "";
            do
            {
                line = Console.ReadLine();
                var equation = CheckForInstructions(line);
                if (!string.IsNullOrWhiteSpace(equation.Trim()))
                {
                    DoWork(equation);
                    Console.WriteLine();
                    Console.WriteLine();
                }

            } while (!string.IsNullOrWhiteSpace(line));


            Console.ReadLine();
        }

        private static string CheckForInstructions(string textLine)
        {
            var trimed = textLine.Trim();
            if (trimed[0] == '@')
            {
                var instruction = trimed[1];

                switch (instruction)
                {
                    case 'h':
                        Console.WriteLine(HelpText.Write());
                        break;
                    case 'a':
                        Console.WriteLine(AllOperatorsText.Write());
                        break;
                    default:
                        Console.WriteLine("I dont understand this instruction");
                        break;
                }

                return trimed.Substring(2);
            }
            else
            {
                return trimed;
            }
        }

        private static void DoWork(string equation)
        {
            var output = RPNParser.Parse(equation);

            Console.WriteLine();
            Console.WriteLine("Parsed: " + string.Join("", output.Select(o => o.Value)));
            var expr = ExpressionBuilder.Build(output);
            Console.WriteLine();
            Console.WriteLine("Equation: " + expr.ToString());
            Console.WriteLine();
            Console.WriteLine("Result: " + expr.Value());

        }
    }
}
