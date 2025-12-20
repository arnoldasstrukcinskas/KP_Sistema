using KP_Sistema.BLL.Interfaces;
using KP_Sistema.DATA.Entities;
using KP_Sistema.DATA.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KP_Sistema.BLL.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<string> AddRoleAsync(string name)
        {
            Role role = new Role
            {
                Name = name
            };

            await _roleRepository.CreateRole(role);

            return name;
        }

        public async Task<string> DeleteRoleASync(int id)
        {
           var role = await _roleRepository.DeleteRole(id);

            return role.Name;
        }

        public async Task<List<Role>> GetAllRoles()
        {
            var roles = await _roleRepository.GetAllRoles();

            return roles;
        }
    }
}
