using KP_Sistema.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KP_Sistema.DATA.Repositories.Interfaces
{
    public interface ICommunityRepository
    {
        Task<Community> AddCommunityAsync(Community community);
        Task<Community> EditCommunityAsync(Community community);
        Task<Community> DeleteCommunityAsync(Community community);
        Task<Community?> GetCommunityById(int id);
        Task<Community?> GetCommunityByName(string name);
        Task<List<Community>> GetCommunitiesByName(string name);
        Task<List<Community>?> GetAllCommunities();
        Task<Community> AddUserToCommunity(int communityId, int userId);
        Task<Community> DeleteUserFromCommunity(int communityId, int userId);
        Task<Community> AddUtilityTaskToCommunity(int communityId, int taskId);
        Task<Community> DeleteUtilityTaskFromCommunity(int communityId, int taskId);
    }
}
