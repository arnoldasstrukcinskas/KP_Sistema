using AutoMapper;
using KP_Sistema.BLL.DTO.UserDTO;
using KP_Sistema.BLL.Interfaces;
using KP_Sistema.BLL.Interfaces.Users;
using KP_Sistema.DATA.Entities;
using KP_Sistema.DATA.Repositories.Interfaces;
using KP_Sistema.DATA.Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace KP_Sistema.BLL.Services.Users
{
    public class AdministratorService : ManagerService, IAdministratorService
    {
        public readonly ICommunityService _communityService;
        public readonly IMapper _mapper;
        private CurrentUserDTO _currentUser;

        public AdministratorService(
            IUserRepository userRepository,
            IUtilityTaskService utilityTaskService,
            ICommunityService communityService,
            IMapper mapper) : base(userRepository, utilityTaskService, communityService, mapper)
        {
            _communityService = communityService;
            _mapper = mapper;
        }

        public async Task<UserReturnDTO> EditUserRole(UserTransferDTO userEditDto, string currentUserUsername)
        {
            await InitializeCurrentUserAsync(currentUserUsername);
            if (!IsAdmin())
            {
                throw new UnauthorizedAccessException("Only admins can edit Users Role");
            }

            var editedUser = EditUser(userEditDto);
           
            return _mapper.Map<UserReturnDTO>(editedUser);
        }

        public async Task<UserReturnDTO> DeleteUser(string currentUserUsername, string username)
        {
            await InitializeCurrentUserAsync(currentUserUsername);
            if(!IsAdmin())
            {
                throw new UnauthorizedAccessException("Only admins can edit Users Role");
            }
            var deletedUser = await DeleteUser(username);

            return _mapper.Map<UserReturnDTO>(deletedUser);
        }

        private bool IsAdmin()
        {
            if(!_currentUser.Role.Equals("Admin"))
            {
                return false;
            }

        return true;
        }

        private async Task InitializeCurrentUserAsync(string username)
        {
            var user = await GetUserByUsername(username);
            _currentUser = _mapper.Map<CurrentUserDTO>(user);
        }
    }
}
