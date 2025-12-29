using KP_Sistema.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KP_Sistema.DATA.Repositories.Interfaces
{
    public interface IUtilityTaskRepository
    {
        Task<UtilityTask> AddUtilityTaskAsync(UtilityTask utilityTask);
        Task<UtilityTask?> GetUtilityTaskById(int id);
        Task<UtilityTask?> GetUtilityTaskByName(string name);
        Task<UtilityTask> EditUtilityTask(UtilityTask utilityTask);
        Task<UtilityTask> DeleteUtilityTask(UtilityTask utilityTask);
        Task<List<UtilityTask>> GetAllUtilityTasks();
    }
}
