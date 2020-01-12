using Mathema.Algorithms.Handlers;
using Mathema.Algorithms.Parsers;
using Mathema.Interfaces;
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
            Console.Write("You can find solution here: ");
            Console.ForegroundColor = ConsoleColor.Green;
            //Console.Clear();
            Console.Write(@"https://github.com/QuantumCores/Mathema" + Environment.NewLine);
            //Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Your decimal delimeter is '" + del + "'. ");
            Console.ResetColor();
            Console.Write("For more help type '@h'" + Environment.NewLine);
            Console.WriteLine("Type your equation below. Example: 1+2*3^4/(Cos(0)+2)");
            Console.WriteLine();
            Console.WriteLine();

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
            try {
                var rpn = RPNParser.Parse(equation);

                Console.WriteLine();
                Console.WriteLine("Parsed  : " + string.Join("", rpn.Output.Select(o => o.Value)));


                if (rpn.Variables.Any())
                {
                    var expr = ExpressionBuilder.BuildFlat(rpn.Output);
                    Console.WriteLine("Equation  : " + expr.ToString());
                    Console.WriteLine("Result    : " + expr.Execute().ToString());
                }
                else
                {
                    var expr = ExpressionBuilder.Build(rpn.Output);
                    var exprf = ExpressionBuilder.BuildFlat(rpn.Output);
                    Console.WriteLine("Equation r: " + expr.ToString());
                    Console.WriteLine("Result   r: " + expr.Execute().Count.AsString());
                    Console.WriteLine("Equation f: " + exprf.ToString());
                    Console.WriteLine("Result   f: " + exprf.Execute().Count.AsString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
