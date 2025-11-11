using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KP_Sistema.BLL.Exceptions
{
    public class CommunityNotFoundException : CommunityException
    {
        public CommunityNotFoundException(int id) 
            : base($"Community with id: {id}, not found") { }
        public CommunityNotFoundException(string name) 
            : base($"Community with name: {name}, not found") { }
    }
}
