using System;
using System.Collections.Generic;
using System.Text;

namespace Mathema.Models.Functions
{
    public enum FunctionTypes
    {
        Sin = 1,
        Cos = 2,
        Tan = 3,
        Ctan = 4
    }

    public class Functions
    {
        private static Dictionary<string, Function> All { get; } = GetAllFunctions();

        private static Dictionary<string, Function> GetAllFunctions()
        {
            var result = new Dictionary<string, Function>();
            result.Add(FunctionTypes.Sin.ToString(), new Function(FunctionTypes.Sin, (val) => (decimal)Math.Sin((double)val)));
            result.Add(FunctionTypes.Cos.ToString(), new Function(FunctionTypes.Cos, (val) => (decimal)Math.Cos((double)val)));

            return result;
        }

        public static bool TryGetValue(string v, out Function func)
        {
            if (All.TryGetValue(v, out var f))
            {
                func = new Function(f.Type, f.Projection);
                return true;
            }

            func = null;
            return false;
        }

        public static Function Get(FunctionTypes type)
        {
            return All[type.ToString()];
        }
    }
}
