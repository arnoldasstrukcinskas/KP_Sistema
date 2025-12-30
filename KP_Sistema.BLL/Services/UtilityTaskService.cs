using AutoMapper;
using KP_Sistema.CONTRACTS.DTO.CommunityDTO;
using KP_Sistema.BLL.Exceptions.Community;
using KP_Sistema.BLL.Exceptions.UtilityTasks;
using KP_Sistema.BLL.Interfaces;
using KP_Sistema.CONTRACTS.DTO.UtilityTaskDTO;
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

        public async Task<UtilityTaskReturnDTO> AddUtilityTaskAsync(UtilityTaskCreateDTO utilityTaskCreateDTO)
        {

            if(utilityTaskCreateDTO == null)
            {
                throw new UtilityTaskException("Utility task is empty");
            }

            var demoTask = await _utilityTaskRepository.GetUtilityTaskByName(utilityTaskCreateDTO.Name);

            if (demoTask != null)
            {
                throw new UtilityTaskException($"Task with name {utilityTaskCreateDTO.Name} already exists.");
            }

            var utilityTask = _mapper.Map<UtilityTask>(utilityTaskCreateDTO);

            var createedUtilityTask = await _utilityTaskRepository.AddUtilityTaskAsync(utilityTask);

            return _mapper.Map<UtilityTaskReturnDTO>(createedUtilityTask);
        }

        public async Task<TDto?> GetUtilityTaskByIdAsync<TDto>(int id)
        {
            var foundUtilityTask = await _utilityTaskRepository.GetUtilityTaskById(id);

            if(foundUtilityTask == null)
            {
                throw new UtilityTaskNotFoundException(id);
            }

            return _mapper.Map<TDto>(foundUtilityTask);
        }

        public async Task<TDto?> GetUtilityTaskByNameAsync<TDto>(string name)
        {
            var foundUtilityTask = await _utilityTaskRepository.GetUtilityTaskByName(name);

            if(foundUtilityTask == null)
            {
                throw new UtilityTaskNotFoundException(name);
            }

            return _mapper.Map<TDto>(foundUtilityTask);
        }

        public async Task<List<UtilityTaskReturnDTO>> GetUtilityTasksByName(string name)
        {
            var tasks = await _utilityTaskRepository.GetUtilityTasksByName(name);

            if(tasks == null)
            {
                throw new UtilityTaskException("Failed to retrieve tasks");
            }

            return _mapper.Map<List<UtilityTaskReturnDTO>>(tasks);
        }

        public async Task<UtilityTaskReturnDTO> EditUtilityTaskAsync(UtilityTaskEditDTO utilityTaskEditDTO)
        {

            if (utilityTaskEditDTO == null)
            {
                throw new UtilityTaskException("Utility task is empty");
            }

            bool exists = await GetUtilityTaskByIdAsync<UtilityTaskReturnDTO>(utilityTaskEditDTO.Id) != null;

            if (!exists)
            {
                throw new UtilityTaskNotFoundException(utilityTaskEditDTO.Id);
            }

            var utilityTask = _mapper.Map<UtilityTask>(utilityTaskEditDTO);


            var editedUtilityTask = await _utilityTaskRepository.EditUtilityTask(utilityTask);

            return _mapper.Map<UtilityTaskReturnDTO>(editedUtilityTask);
        }

        public async Task<UtilityTaskReturnDTO> DeleteUtilityTaskAsync(int id)
        {
            var utilityTask = await GetUtilityTaskByIdAsync(id);
            if(utilityTask == null)
            {
                throw new UtilityTaskNotFoundException(id);
            }

            var deletedUtilityTask = await _utilityTaskRepository.DeleteUtilityTask(utilityTask);

            return _mapper.Map<UtilityTaskReturnDTO>(deletedUtilityTask);
        }

        public async Task<List<UtilityTaskReturnDTO>> GetAllUtilityTasks()
        {
            var tasks = await _utilityTaskRepository.GetAllUtilityTasks();

            if(tasks == null || !tasks.Any())
            {
                throw new UtilityTaskException("There is no tasks in database");
            }

            return _mapper.Map<List<UtilityTaskReturnDTO>>(tasks);
        }

        //method for internal use
        private async Task<UtilityTask> GetUtilityTaskByIdAsync(int id)
        {
            var foundUtilityTask = await _utilityTaskRepository.GetUtilityTaskById(id);

            if (foundUtilityTask == null)
            {
                throw new UtilityTaskNotFoundException(id);
            }

            return foundUtilityTask;
        }
    }
}
