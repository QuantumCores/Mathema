using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mathema.Models.Operators
{
    public enum OperatorTypes
    {
        Add = 1,
        Subtract = 2,
        Divide = 3,
        Multiply = 4,
        Power = 5,
        BinaryAnd = 6,
        BinaryOr = 7,
        LeftParenthesis = 8,
        RightParenthesis = 9,
        Sign = 10,
    }

    public enum AssociativityTypes
    {
        None = 0,
        Left = 1,
        Right = 2
    }

    public enum OperationTypes
    {
        None = 0,
        Unary = 1,
        Binary = 2
    }

    public class Operators
    {
        private static List<Operator> All { get; } = GetAllOperators();

        private static Dictionary<string, Operator> AllByString = All.ToDictionary(o => o.Symbol, o => o);
        private static Dictionary<OperatorTypes, Operator> AllByType = All.ToDictionary(o => o.Type, o => o);

        private static List<Operator> GetAllOperators()
        {
            var result = new List<Operator>();

            result.Add(new Operator("(", OperatorTypes.LeftParenthesis, 1, AssociativityTypes.None, OperationTypes.None));
            result.Add(new Operator(")", OperatorTypes.RightParenthesis, 1, AssociativityTypes.None, OperationTypes.None));
            result.Add(new Operator("+", OperatorTypes.Add, 2, AssociativityTypes.Left, OperationTypes.Binary));
            result.Add(new Operator("-", OperatorTypes.Subtract, 2, AssociativityTypes.Left, OperationTypes.Binary));
            result.Add(new Operator("*", OperatorTypes.Multiply, 3, AssociativityTypes.Left, OperationTypes.Binary));
            result.Add(new Operator("/", OperatorTypes.Divide, 3, AssociativityTypes.Left, OperationTypes.Binary));
            result.Add(new Operator(OperatorTypes.Sign.ToString(), OperatorTypes.Sign, 4, AssociativityTypes.Right, OperationTypes.Unary));
            result.Add(new Operator("^", OperatorTypes.Power, 5, AssociativityTypes.Right, OperationTypes.Unary));

            return result;
        }

        public static Operator Get(string op)
        {
            return AllByString[op];
        }

        public static Operator Get(OperatorTypes type)
        {
            return AllByType[type];
        }
    }
}
