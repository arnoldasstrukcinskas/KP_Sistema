using AutoMapper;
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
    public class UtilityTaskService : IUtitilityTaskService
    {
        private readonly IUtilityTaskRepository _utilityTaskRepository;
        private readonly ICommunityRepository _communityRepository;
        private readonly IMapper _mapper;

        public UtilityTaskService(IUtilityTaskRepository utilityTaskRepository, ICommunityRepository communityRepository,
            IMapper mapper)
        {
            _utilityTaskRepository = utilityTaskRepository;
            _communityRepository = communityRepository;
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
            var utilityTask = await _utilityTaskRepository.FindUtilityTaskByName(name);

            var deletedUtilityTask = await _utilityTaskRepository.DeleteUtilityTask(utilityTask);

            return _mapper.Map<UtilityTaskReturnDTO>(deletedUtilityTask);
        }

        public async Task<UtilityTaskReturnDTO> EditUtilityTaskAsync(UtilityTaskCreateDTO utilityTaskCreateDTO)
        {
            var utilityTAsk = _mapper.Map<UtilityTask>(utilityTaskCreateDTO);

            var editedUtilityTask = await _utilityTaskRepository.EditUtilityTask(utilityTAsk);

            return _mapper.Map<UtilityTaskReturnDTO>(editedUtilityTask);
        }

        public async Task<UtilityTaskReturnDTO?> FindUtilityTaskByNameAsync(string name)
        {
            var foundUtilityTask = await _utilityTaskRepository.FindUtilityTaskByName(name);

            return _mapper.Map<UtilityTaskReturnDTO>(foundUtilityTask);
        }

        public async Task<List<UtilityTaskReturnDTO>?> GetAllUtilityTasksByCommunityAsync(string communityName)
        {
            var community = await _communityRepository.FindCommunityByName(communityName);

            var utilityTasks = await _utilityTaskRepository.GetAllUtilityTasksByCommunity(community);

            var returnUtilityTasks = utilityTasks
                .Select(utilityTask => _mapper.Map<UtilityTaskReturnDTO>(utilityTask))
                .ToList();

            return returnUtilityTasks;
        }
    }
}
