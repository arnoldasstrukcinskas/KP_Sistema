using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KP_Sistema.BLL.Exceptions.UtilityTasks
{
    public class UtilityTaskNotFoundException : UtilityTaskException
    {
        public UtilityTaskNotFoundException(int id)
            : base($"Utility task with id: {id}, not found") { }
        public UtilityTaskNotFoundException(string name)
            : base($"Utility task with name: {name}, not found") { }
    }
}
