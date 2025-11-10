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
        Task<CommunityReturnDTO?> GetCommynityByNameAsync(string name);
        Task<CommunityReturnDTO> EditCommunityAsync(CommunityCreateDTO communityCreateDTO);
        Task<CommunityReturnDTO> DeleteCommunityAsync(string name);
        Task<List<CommunityReturnDTO>?> GetAllCommunities();
    }
}
