using Mathema.Algorithms.Parsers;
using System;

namespace Mathema
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("For help type '@h'");
            

            var rpn = RPNParser.Parse("5 + 1 * 3");
            foreach (var d in rpn)
            {
                Console.Write(d.Value + " ");
            }

            Console.ReadLine();
        }
    }
}
