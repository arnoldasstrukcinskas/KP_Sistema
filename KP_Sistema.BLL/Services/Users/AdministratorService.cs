using AutoMapper;
using KP_Sistema.CONTRACTS.DTO.UserDTO;
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
using KP_Sistema.BLL.Exceptions.Authentication;

namespace KP_Sistema.BLL.Services.Users
{
    public class AdministratorService : ManagerService, IAdministratorService
    {
        public readonly ICommunityService _communityService;
        public readonly IUserRepository _userRepository;
        public readonly IMapper _mapper;

        public AdministratorService(
            IUserRepository userRepository,
            ICommunityService communityService,
            IUtilityTaskService utilityTaskService,
            IMapper mapper) : base(userRepository, utilityTaskService, communityService, mapper)
        {
            _communityService = communityService;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserReturnDTO> EditUserRole(int userId, int roleId)
        {

            if(!await IfUserExistsById(userId))
            {
                throw new Exception("No such user");
            }

            var editedUser = await _userRepository.EditRole(userId, roleId);

            return _mapper.Map<UserReturnDTO>(editedUser);
        }

        public async Task<UserReturnDTO> SetCommunity(int userId, int communityId)
        {
            if(!await IfUserExistsById(userId))
            {
                throw new Exception("No such user");
            }

            var editedUser = await _userRepository.SetCommunity(userId, communityId);

            return _mapper.Map<UserReturnDTO>(editedUser);
        }

        public async Task<UserReturnDTO> DeleteUser(string username)
        {

            if (!await IfUserExistsByName(username))
            {
                throw new Exception("No such user");
            }

            var deletedUser = await DeleteUser(username);

            return _mapper.Map<UserReturnDTO>(deletedUser);
        }

        private async Task<bool> IfUserExistsByName(string username)
        {
            var userByName = await _userRepository.GetUserByUsername(username);

            if(userByName == null)
            {
                throw new Exception("No such user");
                return false;
            }
            return true;
        }

        private async Task<bool> IfUserExistsById(int id)
        {
            var userById = await _userRepository.GetUserById(id);

            if (userById == null)
            {
                return false;
            }
            return true;
        }
    }
}
