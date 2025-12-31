using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KP_Sistema.BLL.Exceptions.Authentication
{
    public class UserAuthenticationNotFoundException : UserAuthenticationException
    {
        public UserAuthenticationNotFoundException(string name)
            : base($"User with name: {name}, not found") { }
    }
}
