using KP_Sistema.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KP_Sistema.BLL.Interfaces
{
    public interface IRoleService
    {
        Task<String> AddRoleAsync(string name);
        Task<String> DeleteRoleASync(int id);
        Task<List<Role>> GetAllRoles();
    }
}
