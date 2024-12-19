using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp
{
    internal class InvalidEmailException : Exception
    {
        public InvalidEmailException() { }

        public InvalidEmailException (string email) : base(String.Format("Invalid emails: {0}  try example@email.com", email)) { }
    }
}
