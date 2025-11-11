using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KP_Sistema.BLL.Exceptions
{
    public class CommunityUnauthorizedAccessException : CommunityException
    {
        public CommunityUnauthorizedAccessException(int id) 
            : base($"Unauthorized actions for user with id: {id}") { }
    }
}
