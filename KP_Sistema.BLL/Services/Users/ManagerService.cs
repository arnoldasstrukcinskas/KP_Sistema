using AutoMapper;
using KP_Sistema.BLL.DTO.CommunityDTO;
using KP_Sistema.BLL.DTO.UserDTO;
using KP_Sistema.BLL.DTO.UtilityTaskDTO;
using KP_Sistema.BLL.Exceptions.UtilityTasks;
using KP_Sistema.BLL.Interfaces;
using KP_Sistema.BLL.Interfaces.Users;
using KP_Sistema.DATA.Entities;
using KP_Sistema.DATA.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KP_Sistema.BLL.Services.Users
{
    public class ManagerService : UserService, IManagerService
    {
        private readonly IUtilityTaskService _utilityTaskService;
        private readonly ICommunityService _communityService;
        private readonly IMapper _mapper;
        private CurrentUserDTO _currentUser;

        public ManagerService(
            IUserRepository userRepository,
            IUtilityTaskService utilityTaskService, 
            ICommunityService communityService,
            IMapper mapper) : base(userRepository, mapper)
        {
            _utilityTaskService = utilityTaskService;
            _communityService = communityService;
            _mapper = mapper;
        }
        public async Task<UtilityTaskReturnDTO> AddTaskToCommunity(string taskName, string communityName, string currentUser)
        {

           await InitializeCurrentUserAsync(currentUser);

            var user = await GetUserByUsername(currentUser);
            if(!IsManager(currentUser))
            {
                throw new UtilityTaskUnauthorizedAccessException(user.Id);
            }

            var community = await _communityService.GetCommunityByNameAsync<CommunityTransferDTO>(communityName);

            var utilityTransferTask = await _utilityTaskService.GetUtilityTaskByNameAsync<UtilityTaskTransferDTO>(taskName);

            community.UtilityTasks.Add(utilityTransferTask);

            var editedCommunity = await _communityService.EditCommunityAsync(community);

            return _mapper.Map<UtilityTaskReturnDTO>(utilityTransferTask);
        }

        private bool IsManager(string role)
        {
            if (!_currentUser.Role.Equals("Manager"))
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
