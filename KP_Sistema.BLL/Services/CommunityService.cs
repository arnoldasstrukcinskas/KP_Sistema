using AutoMapper;
using KP_Sistema.BLL.DTO.CommunityDTO;
using KP_Sistema.BLL.Interfaces;
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
            
            var createdCommunity = await _communityRepository.AddCommunityAsync(community);

            return _mapper.Map<CommunityReturnDTO>(createdCommunity);
        }

        public async Task<CommunityTransferDTO> GetCommunityByIdAsync(int id)
        {
            var foundCommunity = await _communityRepository.FindCommunityById(id);

            return _mapper.Map<CommunityTransferDTO>(foundCommunity);
        }

        public async Task<CommunityTransferDTO?> GetCommynityByNameAsync(string name)
        {
            var foundCommunity = await _communityRepository.FindCommunityByName(name);

            return _mapper.Map<CommunityTransferDTO>(foundCommunity);
        }

        public async Task<CommunityTransferDTO> EditCommunityAsync(CommunityTransferDTO communityTransferDTO)
        {
            var community = _mapper.Map<Community>(communityTransferDTO);

            var editedCommunity = await _communityRepository.EditCommunityAsync(community);

            return _mapper.Map<CommunityTransferDTO>(editedCommunity);
        }

        public async Task<CommunityReturnDTO> DeleteCommunityAsync(string name)
        {
            var community = await _communityRepository.FindCommunityByName(name);

            var deletedCommunity = await _communityRepository.DeleteCommunityAsync(community);

            return _mapper.Map<CommunityReturnDTO>(deletedCommunity);
        }

        public async Task<List<CommunityReturnDTO>?> GetAllCommunities()
        {
            var communities = await _communityRepository.GetAllCommunities();

            var communitiesList = communities
                .Select(community => _mapper.Map<CommunityReturnDTO>(community))
                .ToList();

            return communitiesList;
        }
    }
}
