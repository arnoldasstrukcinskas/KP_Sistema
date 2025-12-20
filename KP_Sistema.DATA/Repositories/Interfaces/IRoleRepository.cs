using KP_Sistema.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KP_Sistema.DATA.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        Task<Role> CreateRole(Role role);
        Task<Role> DeleteRole(int id);
        Task<List<Role>> GetAllRoles();
    }
}
