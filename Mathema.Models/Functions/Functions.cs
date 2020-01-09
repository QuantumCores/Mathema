using Mathema.Enums.Functions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mathema.Models.Functions
{
    public class Functions
    {
        public static Dictionary<string, Function> All { get; } = GetAllFunctions();

        private static Dictionary<string, Function> GetAllFunctions()
        {
            var result = new Dictionary<string, Function>();
            result.Add(FunctionTypes.Sin.ToString().ToLower(), new Function(FunctionTypes.Sin, (val) => (decimal)Math.Sin((double)val)));
            result.Add(FunctionTypes.Cos.ToString().ToLower(), new Function(FunctionTypes.Cos, (val) => (decimal)Math.Cos((double)val)));
            result.Add(FunctionTypes.Tan.ToString().ToLower(), new Function(FunctionTypes.Tan, (val) => (decimal)Math.Tan((double)val)));
            result.Add(FunctionTypes.Cot.ToString().ToLower(), new Function(FunctionTypes.Cot, (val) => (decimal)(1 / Math.Tan((double)val))));

            return result;
        }

        public static bool TryGetValue(string v, out Function func)
        {
            if (All.TryGetValue(v.ToLower(), out var f))
            {
                func = new Function(f.Type, f.Projection);
                return true;
            }

            func = null;
            return false;
        }

        public static Function Get(FunctionTypes type)
        {
            return All[type.ToString().ToLower()];
        }

        public static Function Get(string function)
        {
            return All[function.ToLower()];
        }
    }
}
