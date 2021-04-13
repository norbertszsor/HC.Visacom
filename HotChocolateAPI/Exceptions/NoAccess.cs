using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotChocolateAPI.Exceptions
{
    public class NoAccess : Exception
    {
        public NoAccess(string msg):base(msg)
        {
            
        }
    }

}
