using KP_Sistema.DATA.Entities;
using KP_Sistema.DATA.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KP_Sistema.DATA.Repositories.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDbContext _dbContext;

        public RoleRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Role> CreateRole(Role role)
        {
            await _dbContext.Roles.AddAsync(role);
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // ex.InnerException usually contains SQL error message
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }

            return role;
        }

        public async Task<Role> DeleteRole(int id)
        {
            var role = await GetRoleById(id);

            _dbContext.Roles.Remove(role);
            await _dbContext.SaveChangesAsync();

            return role;
        }

        public async Task<List<Role>> GetAllRoles()
        {
            var roles = await _dbContext.Roles.ToListAsync();

            return roles;
        }

        private async Task<Role> GetRoleById(int id)
        {
            var role = await _dbContext.Roles.FirstOrDefaultAsync(role => role.Id == id);

            return role;
        }
    }
}
