using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KP_Sistema.BLL.Exceptions.UtilityTasks
{
    public class UtilityTaskUnauthorizedAccessException : UtilityTaskException
    {
        public UtilityTaskUnauthorizedAccessException(int id)
            : base($"Unauthorized actions for user with id: {id}") { }
    }
}
