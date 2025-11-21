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
        Task<UserReturnDTO> EditUserRole(UserTransferDTO userEditDto, string currentUserUsername);
        Task<UserReturnDTO> DeleteUser(string userName);
    }
}
