using AutoMapper;
using KP_Sistema.BLL.DTO.CommunityDTO;
using KP_Sistema.BLL.DTO.UtilityTaskDTO;
using KP_Sistema.BLL.Interfaces;
using KP_Sistema.DATA.Entities;
using KP_Sistema.DATA.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KP_Sistema.BLL.Services
{
    public class UtilityTaskService : IUtilityTaskService
    {
        private readonly IUtilityTaskRepository _utilityTaskRepository;
        private readonly ICommunityService _communityService;
        private readonly IMapper _mapper;

        public UtilityTaskService(IUtilityTaskRepository utilityTaskRepository, ICommunityService communityService,
            IMapper mapper)
        {
            _utilityTaskRepository = utilityTaskRepository;
            _communityService = communityService;
            _mapper = mapper;
        }

        public async Task<UtilityTaskReturnDTO> CreateTaskAsync(UtilityTaskCreateDTO utilityTaskCreateDTO)
        {
            var utilityTask = _mapper.Map<UtilityTask>(utilityTaskCreateDTO);

            var createedUtilityTask = await _utilityTaskRepository.AddUtilityTaskAsync(utilityTask);

            return _mapper.Map<UtilityTaskReturnDTO>(createedUtilityTask);
        }

        public async Task<UtilityTaskReturnDTO> DeleteUtilityTaskAsync(string name)
        {
            var utilityTask = await _utilityTaskRepository.GetUtilityTaskByName(name);

            var deletedUtilityTask = await _utilityTaskRepository.DeleteUtilityTask(utilityTask);

            return _mapper.Map<UtilityTaskReturnDTO>(deletedUtilityTask);
        }

        public async Task<UtilityTaskReturnDTO> EditUtilityTaskAsync(UtilityTaskTransferDTO utilityTaskTransferDTO)
        {
            var community = await _communityService.GetCommunityByIdAsync<CommunityTransferDTO>(utilityTaskTransferDTO.Id);

            var utilityTask = _mapper.Map<UtilityTask>(utilityTaskTransferDTO);
            utilityTask.CommunityId = community.Id;
            utilityTask.Community = _mapper.Map<Community>(community);

            var editedUtilityTask = await _utilityTaskRepository.EditUtilityTask(utilityTask);

            return _mapper.Map<UtilityTaskReturnDTO>(editedUtilityTask);
        }

        public async Task<UtilityTaskTransferDTO?> FindUtilityTaskByNameAsync(string name)
        {
            var foundUtilityTask = await _utilityTaskRepository.GetUtilityTaskByName(name);

            return _mapper.Map<UtilityTaskTransferDTO>(foundUtilityTask);
        }

        public async Task<List<UtilityTaskReturnDTO>?> GetAllUtilityTasksByCommunityAsync(string communityName)
        {
            var communityTransferDTO = await _communityService.GetCommunityByNameAsync<CommunityTransferDTO>(communityName);

            var community = _mapper.Map<Community>(communityTransferDTO);

            var utilityTasks = await _utilityTaskRepository.GetAllUtilityTasksByCommunity(community);

            var returnUtilityTasks = utilityTasks
                .Select(utilityTask => _mapper.Map<UtilityTaskReturnDTO>(utilityTask))
                .ToList();

            return returnUtilityTasks;
        }
    }
}
