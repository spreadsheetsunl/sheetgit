using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitExcelAddIn.exceptions
{
    public class NoChangesExistException : Exception
    {
        public NoChangesExistException()
        {
        }

        public NoChangesExistException(string message)
            : base(message)
        {
        }

        public NoChangesExistException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
