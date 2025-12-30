using KP_Sistema.CONTRACTS.DTO.UtilityTaskDTO;
using KP_Sistema.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KP_Sistema.BLL.Interfaces
{
    public interface IUtilityTaskService
    {
        Task<UtilityTaskReturnDTO> AddUtilityTaskAsync(UtilityTaskCreateDTO utilityTaskCreateDTO);
        Task<TDto?> GetUtilityTaskByIdAsync<TDto>(int id);
        Task<TDto?> GetUtilityTaskByNameAsync<TDto>(string name);
        Task<UtilityTaskReturnDTO> EditUtilityTaskAsync(UtilityTaskEditDTO utilityTaskEditDTO);
        Task<UtilityTaskReturnDTO> DeleteUtilityTaskAsync(int id);
        Task<List<UtilityTaskReturnDTO>> GetAllUtilityTasks();
    }
}
