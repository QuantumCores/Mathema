using System;
using System.Collections.Generic;
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
        Sin = 11,
        Cos = 12,
        Tan = 13,
        Ctan = 14
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
        public static Dictionary<string, Operator> All = GetAllOperators();

        private static Dictionary<string, Operator> GetAllOperators()
        {
            var result = new Dictionary<string, Operator>();

            result.Add("(", new Operator("(", OperatorTypes.LeftParenthesis, 1, AssociativityTypes.None, OperationTypes.None));
            result.Add(")", new Operator(")", OperatorTypes.RightParenthesis, 1, AssociativityTypes.None, OperationTypes.None));
            result.Add("+", new Operator("+", OperatorTypes.Add, 2, AssociativityTypes.Left, OperationTypes.Binary));
            result.Add("-", new Operator("-", OperatorTypes.Subtract, 2, AssociativityTypes.Left, OperationTypes.Binary));
            result.Add("*", new Operator("*", OperatorTypes.Multiply, 3, AssociativityTypes.Left, OperationTypes.Binary));
            result.Add("/", new Operator("/", OperatorTypes.Divide, 3, AssociativityTypes.Left, OperationTypes.Binary));
            result.Add("^", new Operator("^", OperatorTypes.Power, 4, AssociativityTypes.Right, OperationTypes.Unary));

            return result;
        }
    }
}
