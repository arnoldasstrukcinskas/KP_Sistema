using KP_Sistema.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Net.WebSockets;

namespace KP_Sistema.API.Controllers
{
    [Route("controller")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        /// <summary>
        /// Adds role to database
        /// </summary>
        /// <param name="name">Role Name</param>
        /// <returns>A created role object name</returns>
        [HttpPost]
        public async Task<IActionResult> CreateRole(string name)
        {
            if(name.IsNullOrEmpty())
            {
                return BadRequest("Missing role name.");
            }

            var roleName = await _roleService.AddRoleAsync(name);

            return Ok(roleName);
        }


        /// <summary>
        /// Deletes role from database
        /// </summary>
        /// <param id="id">Role id</param>
        /// <returns>A deleted role object name</returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteRole(int id)
        {
            if(id <= 0)
            {
                return BadRequest("Missing id");
            }

            var response = await _roleService.DeleteRoleASync(id);

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _roleService.GetAllRoles();
            if(roles.IsNullOrEmpty())
            {
                return BadRequest("There is no roles");
            }

            return Ok(roles);
        }
    }
}
