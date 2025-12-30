using AutoMapper;
using KP_Sistema.BLL.Exceptions;
using KP_Sistema.BLL.Exceptions.Community;
using KP_Sistema.BLL.Interfaces;
using KP_Sistema.CONTRACTS.DTO.CommunityDTO;
using KP_Sistema.DATA.Entities;
using KP_Sistema.DATA.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace KP_Sistema.BLL.Services
{
    public class CommunityService : ICommunityService
    {
        private readonly ICommunityRepository _communityRepository;
        private readonly IMapper _mapper;

        public CommunityService(ICommunityRepository communityRepository, IMapper mapper)
        {
            _communityRepository = communityRepository;
            _mapper = mapper;
        }
        public async Task<CommunityReturnDTO> AddCommunityAsync(CommunityCreateDTO communityCreateDTO)
        {
            var community = _mapper.Map<Community>(communityCreateDTO);

            if (community == null || community.Name.Equals(""))
            {
                throw new CommunityException("There is no data for creating community.");
            }

            var foundCommunity = await _communityRepository.GetCommunityByName(communityCreateDTO.Name);
            
            if(foundCommunity != null)
            {
                throw new CommunityException($"Community with name {communityCreateDTO.Name} already exists.");
            }

            var createdCommunity = await _communityRepository.AddCommunityAsync(community);

            return _mapper.Map<CommunityReturnDTO>(createdCommunity);
        }

        public async Task<TDto?> GetCommunityByIdAsync<TDto>(int id)
        {
            //check to avoid null
            var foundCommunity = await _communityRepository.GetCommunityById(id);
            
            if(foundCommunity == null)
            {
                throw new CommunityNotFoundException(id);
            }

            return _mapper.Map<TDto>(foundCommunity);
        }

        public async Task<TDto?> GetCommunityByNameAsync<TDto>(string name)
        {
            var foundCommunity = await _communityRepository.GetCommunityByName(name);

            if (foundCommunity == null)
            {
                throw new CommunityNotFoundException(name);
            }

            return _mapper.Map<TDto>(foundCommunity);
        }

        public async Task<List<TDto>> GetCommunitiesByNameAsync<TDto>(string name)
        {
            var foundCommunities = await _communityRepository.GetCommunitiesByName(name);

            if(foundCommunities == null)
            {
                throw new CommunityNotFoundException(name);
            }

            return _mapper.Map<List<TDto>>(foundCommunities);
        }

        public async Task<CommunityReturnDTO> EditCommunityAsync(int id, CommunityEditDTO communityEditDTO)
        {
            if(communityEditDTO == null)
            {
                throw new CommunityException("There is no or not enough data.");
            }    

            var community = await GetCommunityByIdAsync(id);

            if(community == null)
            {
                throw new CommunityNotFoundException("No such community.");
            }

            community.Name = communityEditDTO.Name;
            community.Address = communityEditDTO.Address;

            var editedCommunity = await _communityRepository.EditCommunityAsync(community);

            return _mapper.Map<CommunityReturnDTO>(editedCommunity);
        }

        public async Task<CommunityReturnDTO> DeleteCommunityAsync(int id)
        {
            var community = await GetCommunityByIdAsync(id);

            if(community == null)
            {
                throw new CommunityNotFoundException(id);
            }

            var deletedCommunity = await _communityRepository.DeleteCommunityAsync(community);

            return _mapper.Map<CommunityReturnDTO>(deletedCommunity);
        }

        public async Task<List<CommunityReturnDTO>?> GetAllCommunities()
        {
            var communities = await _communityRepository.GetAllCommunities();

            if(communities == null)
            {
                throw new CommunityException("Something wrong with getting communities");
            }

            var communitiesList = communities
                .Select(community => _mapper.Map<CommunityReturnDTO>(community))
                .ToList();

            return communitiesList;
        }

        public async Task<CommunityReturnDTO> AddUserToCommunity(int communityId, int userId)
        {
            var community = await GetCommunityByIdAsync(communityId);
            Community editedCommunity;

            if (!community.Users.Any(user => user.Id == userId))
            {
                editedCommunity = await _communityRepository.AddUserToCommunity(communityId, userId);
            }
            else
            {
                throw new CommunityException($"Such user with id:{userId} already added");
            }

            return _mapper.Map<CommunityReturnDTO>(editedCommunity);
        }

        public async Task<CommunityReturnDTO> DeleteUserFromCommunity(int communityId, int userId)
        {
            var community = await GetCommunityByIdAsync(communityId);
            Community editedCommunity;

            if(community.Users.Any(user => user.Id == userId))
            {
                editedCommunity = await _communityRepository.DeleteUserFromCommunity(communityId, userId);
            }
            else
            {
                throw new CommunityException($"There is no such user with id:{userId}");
            }

            return _mapper.Map<CommunityReturnDTO>(editedCommunity);
        }

        public async Task<CommunityReturnDTO> AddUtilityTaskToCommunity(int communityId, int taskId)
        {
            var community = await GetCommunityByIdAsync(communityId);
            Community editedCommunity;

            if(!community.UtilityTasks.Any(task => task.Id == taskId))
            {
                editedCommunity = await _communityRepository.AddUtilityTaskToCommunity(communityId, taskId);
            }
            else
            {
                throw new CommunityException($"Such task with id: {taskId} already added.");
            }

            return _mapper.Map<CommunityReturnDTO>(editedCommunity);
        }

        public async Task<CommunityReturnDTO> DeleteUtilityTaskFromCommunity(int communityId, int taskId)
        {
            var community = await GetCommunityByIdAsync(communityId);
            Community editedCommunity;

            if(community.UtilityTasks.Any(task => task.Id == taskId))
            {
                editedCommunity = await _communityRepository.DeleteUtilityTaskFromCommunity(communityId, taskId);
            }
            else
            {
                throw new CommunityException($"There is no such task with id: {taskId}");
            }

            return _mapper.Map<CommunityReturnDTO>(editedCommunity);
        }

        //Method for internal use
        private async Task<Community> GetCommunityByIdAsync(int id)
        {
            var foundCommunity = await _communityRepository.GetCommunityById(id);

            if (foundCommunity == null)
            {
                throw new CommunityNotFoundException(id);
            }

            return foundCommunity;
        }
    }
}
