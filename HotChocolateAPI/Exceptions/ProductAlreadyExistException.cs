using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotChocolateAPI.Exceptions
{
    public class ProductAlreadyExistException : Exception
    {
        public ProductAlreadyExistException(string message):base(message)
        {

        }
    }
}
