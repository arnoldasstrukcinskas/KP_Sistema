using KP_Sistema.CONTRACTS.DTO.UserDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KP_Sistema.BLL.Interfaces.Users
{
    public interface IUserService
    {
        Task<UserReturnDTO> AddUser(UserCreateDTO userCreateDTO);
        Task<UserReturnDTO> GetUserByUsername(string username);
        Task<UserReturnDTO> EditUser(UserTransferDTO userEditDTO);
        Task<UserReturnDTO> DeleteUser(string username);
        Task<String> GetUserPassword(string username);
        Task<List<UserReturnDTO>> GetAllUsers();
    }
}
