using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotChocolateAPI.Exceptions
{
    public class AlreadyExists : Exception
    {
        public AlreadyExists(string msg):base(msg)
        {

        }
    }
}
