using KP_Sistema.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KP_Sistema.DATA.Repositories.Interfaces
{
    internal interface IUtilityTaskRepository
    {
        Task<UtilityTask> AddTask(UtilityTask utilityTask);
        Task<UtilityTask?> FindUtilityTaskByName(string name);
        Task<UtilityTask> EditUtilityTask(UtilityTask utilityTask);
        Task<UtilityTask> DeleteUtilityTask(UtilityTask utilityTask);
        Task<List<UtilityTask>> GetAllUtilityTasksByCommunity(Community community);
    }
}
