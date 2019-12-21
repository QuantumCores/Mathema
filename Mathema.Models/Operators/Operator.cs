using Mathema.Enums.Operators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mathema.Models.Operators
{
    public class Operator
    {
        public string Symbol { get; }

        public OperatorTypes Type { get; }

        public int Precedence { get; }

        public AssociativityTypes AssociativityType { get; }

        public OperationTypes OperationType { get; }

        public Operator(string symbol, OperatorTypes type, int precedence, AssociativityTypes associativity, OperationTypes operationType)
        {
            this.Symbol = symbol;
            this.Type = type;
            this.Precedence = precedence;
            this.AssociativityType = associativity;
            this.OperationType = operationType;
        }
    }
}
