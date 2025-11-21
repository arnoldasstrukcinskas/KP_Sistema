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

        public async Task<CommunityReturnDTO> EditCommunityAsync(CommunityTransferDTO communityTransferDTO)
        {
            if(communityTransferDTO == null)
            {
                throw new CommunityNotFoundException(communityTransferDTO.Name);
            }    

            var community = _mapper.Map<Community>(communityTransferDTO);

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
