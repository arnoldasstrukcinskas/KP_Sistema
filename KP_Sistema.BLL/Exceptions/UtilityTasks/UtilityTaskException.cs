using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KP_Sistema.BLL.Exceptions.UtilityTasks
{
    public class UtilityTaskException : Exception
    {
        public UtilityTaskException(string message)
            : base(message) { }
    }
}
