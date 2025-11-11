using KP_Sistema.BLL.DTO.UtilityTaskDTO;
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
        Task<UtilityTaskReturnDTO> CreateTaskAsync(UtilityTaskCreateDTO utilityTaskCreateDTO);
        Task<UtilityTaskTransferDTO?> FindUtilityTaskByNameAsync(string name);
        Task<UtilityTaskReturnDTO> EditUtilityTaskAsync(UtilityTaskTransferDTO utilityTaskCreateDTO);
        Task<UtilityTaskReturnDTO> DeleteUtilityTaskAsync(string name);
        Task<List<UtilityTaskReturnDTO>?> GetAllUtilityTasksByCommunityAsync(string communityName);
    }
}
