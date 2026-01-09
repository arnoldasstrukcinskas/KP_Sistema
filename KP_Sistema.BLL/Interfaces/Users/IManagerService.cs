using KP_Sistema.CONTRACTS.DTO.UtilityTaskDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KP_Sistema.BLL.Interfaces.Users
{
    public interface IManagerService
    {
        Task<UtilityTaskReturnDTO> AddTaskToCommunity(string taskName, string communityName);
    }
}
