using KP_Sistema.CONTRACTS.DTO.UserDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KP_Sistema.BLL.Interfaces
{
    public interface IAuthenticationService
    {
        Task<UserReturnDTO?> Login(string username, string password);
        Task<UserReturnDTO> Register(UserCreateDTO userCreateDTO);
    }
}
