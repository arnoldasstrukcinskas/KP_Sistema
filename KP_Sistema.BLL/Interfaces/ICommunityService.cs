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
        Task<CommunityTransferDTO> GetCommunityByIdAsync(int id);
        Task<CommunityTransferDTO?> GetCommynityByNameAsync(string name);
        Task<CommunityTransferDTO> EditCommunityAsync(CommunityTransferDTO communityTransferDTO);
        Task<CommunityReturnDTO> DeleteCommunityAsync(string name);
        Task<List<CommunityReturnDTO>?> GetAllCommunities();
    }
}
