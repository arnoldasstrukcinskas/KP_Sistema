using KP_Sistema.BLL.DTO.CommunityDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KP_Sistema.BLL.Interfaces
{
    public interface ICommunityService
    {
        Task<CommunityReturnDTO> AddCommunityAsync(CommunityCreateDTO communityCreateDTO);
        Task<TDto> GetCommunityByIdAsync<TDto>(int id);        // "Dto" is just description for T generic, can be any descriptione like TEntity etc...
        Task<TDto> GetCommunityByNameAsync<TDto>(string name);
        Task<CommunityReturnDTO> EditCommunityAsync(CommunityTransferDTO communityTransferDTO);
        Task<CommunityReturnDTO> DeleteCommunityAsync(int id);
        Task<List<CommunityReturnDTO>?> GetAllCommunities();
    }
}
