using System;
using System.Collections.Generic;
using System.Text;

namespace Mathema.Texts
{
    public class HelpText
    {
        public static string Write()
        {
            var allLines = new List<string>();
            allLines.Add("Helpful instructions start with '@' sign");
            allLines.Add("@h - help");
            allLines.Add("@a - all operators and functions");

            return string.Join(Environment.NewLine, allLines);
        }
    }
}
