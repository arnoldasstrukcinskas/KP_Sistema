using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KP_Sistema.BLL.Exceptions.Community
{
    public class CommunityException : Exception
    {
        public CommunityException(string message) 
            : base(message) { }
    }
}
