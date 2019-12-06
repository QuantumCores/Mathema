using Mathema.Shared.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mathema.Models.Constants
{
    public class Constants
    {
        public static Dictionary<string, Constant> All { get; } = GetAllConstants();

        private static Dictionary<string, Constant> GetAllConstants()
        {
            var result = new Dictionary<string, Constant>();
            result.Add(ConstantTypes.e.ToString().ToLower(), new Constant(ConstantTypes.e.ToString().ToLower(), Shared.Constants.Constants.e, ConstantTypes.e));
            result.Add(ConstantTypes.EM.ToString().ToLower(), new Constant(ConstantTypes.EM.ToString().ToLower(), Shared.Constants.Constants.EM, ConstantTypes.EM));
            result.Add(ConstantTypes.PI.ToString().ToLower(), new Constant(ConstantTypes.PI.ToString().ToLower(), Shared.Constants.Constants.PI, ConstantTypes.EM));
            result.Add(ConstantTypes.Phi.ToString().ToLower(), new Constant(ConstantTypes.Phi.ToString().ToLower(), Shared.Constants.Constants.Phi, ConstantTypes.EM));

            return result;
        }

        public static bool TryGetValue(string v, out Constant constant)
        {
            if (All.TryGetValue(v.ToLower(), out var f))
            {
                constant = new Constant(f.Symbol, f.Value, f.Type );
                return true;
            }

            constant = null;
            return false;
        }

        public static Constant Get(ConstantTypes type)
        {
            return All[type.ToString().ToLower()];
        }

        public static Constant Get(string symbol)
        {
            return All[symbol.ToLower()];
        }
    }
}
