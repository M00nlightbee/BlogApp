using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp
{
    internal class InvalidPasswordException: Exception
    {
        public InvalidPasswordException() { }

        public InvalidPasswordException(string pwd) : base(String.Format("Invalid password: {0} must include and start with: \n- Letter first, \n- Then number , \n- Then special character", pwd)) { }
    }
}
