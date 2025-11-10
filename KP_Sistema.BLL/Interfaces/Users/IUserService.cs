using KP_Sistema.BLL.DTO.UserDTO;
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
        Task<UserReturnDTO> EditUser(UserEditDTO userEditDTO);
        Task<UserReturnDTO> DeleteUser(string username);
    }
}
