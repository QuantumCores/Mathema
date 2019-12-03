using System;
using System.Collections.Generic;
using System.Text;

namespace Mathema.Models.Exceptions
{
    public class WrongSyntaxException : Exception
    {
        public WrongSyntaxException()
        {

        }

        public WrongSyntaxException(string message) : base(message)
        {

        }

        public WrongSyntaxException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
