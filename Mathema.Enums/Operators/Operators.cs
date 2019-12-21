using System;
using System.Collections.Generic;
using System.Text;

namespace Mathema.Enums.Operators
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
}
