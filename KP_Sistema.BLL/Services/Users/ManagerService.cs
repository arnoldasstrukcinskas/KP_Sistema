using AutoMapper;
using KP_Sistema.BLL.DTO.CommunityDTO;
using KP_Sistema.BLL.DTO.UserDTO;
using KP_Sistema.BLL.DTO.UtilityTaskDTO;
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
                throw new UnauthorizedAccessException("Only managers can add Task to Community!");
            }

            var community = await _communityService.GetCommynityByNameAsync(communityName);

            var utilityTransferTask = await _utilityTaskService.FindUtilityTaskByNameAsync(taskName);

            var utilityTask = _mapper.Map<UtilityTask>(utilityTransferTask);

            community.UtilityTasks.Add(utilityTask);

            var editedCommunity = await _communityService.EditCommunityAsync(community);

            return _mapper.Map<UtilityTaskReturnDTO>(utilityTask);
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
