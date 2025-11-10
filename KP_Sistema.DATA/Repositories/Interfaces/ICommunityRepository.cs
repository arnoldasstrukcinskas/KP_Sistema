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
        Task<Community?> FindCommunityByName(string name);
        Task<List<Community>?> GetAllCommunities();
    }
}
