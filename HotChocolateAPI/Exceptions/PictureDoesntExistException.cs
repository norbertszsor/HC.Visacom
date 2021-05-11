using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotChocolateAPI.Exceptions
{
    public class PictureDoesntExistException:Exception
    {
        public PictureDoesntExistException(string msg):base(msg)
        {
            
        }
    }
}
