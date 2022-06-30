using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityProject.Exceptions
{
    public class InputExistException : Exception
    {
        public InputExistException(string? message) : base(message)
        {
        }
    }
}
