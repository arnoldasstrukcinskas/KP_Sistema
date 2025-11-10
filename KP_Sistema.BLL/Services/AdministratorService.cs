using KP_Sistema.BLL.DTO.UserDTO;
using KP_Sistema.BLL.Interfaces;
using KP_Sistema.BLL.Interfaces.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KP_Sistema.BLL.Services
{
    public class AdministratorService : ManagerService, IAdministratorService
    {
        public readonly ICommunityService _communityService;

        public AdministratorService(ICommunityService communityService)
        {
            _communityService = communityService;
        }

        public Task<UserReturnDTO> DeleteUser(UserEditDTO userEditDTO)
        {
            throw new NotImplementedException();
        }

        public Task<UserReturnDTO> EditUserRole(UserEditDTO userEditDto)
        {
            throw new NotImplementedException();
        }
    }
}
