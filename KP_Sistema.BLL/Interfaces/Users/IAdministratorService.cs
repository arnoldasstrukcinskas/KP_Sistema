using KP_Sistema.CONTRACTS.DTO.UserDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KP_Sistema.BLL.Interfaces.Users
{
    public interface IAdministratorService
    {
        Task<UserReturnDTO> EditUserRole(int userId, int roleId);
        Task<UserReturnDTO> SetCommunity(int userId, int communityId);
        Task<UserReturnDTO> DeleteUser(int id);
        Task<List<UserReturnDTO>> GetAllAdmins();
        Task<List<UserReturnDTO>> GetAllManagers();
    }
}
