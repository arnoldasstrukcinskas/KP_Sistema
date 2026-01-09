using KP_Sistema.BLL.Interfaces;
using KP_Sistema.BLL.Interfaces.Users;
using KP_Sistema.CONTRACTS.DTO.CommunityDTO;
using KP_Sistema.CONTRACTS.DTO.UserDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Runtime.CompilerServices;

namespace KP_Sistema.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAdministratorService _administratorService;

        public UserController(IUserService userService, IAdministratorService administratorService)
        {
            _userService = userService;
            _administratorService = administratorService;
        }

        /// <summary>
        /// Gets all users
        /// </summary>
        /// <returns>Return users list</returns>
        //[Authorize(Roles = "Admin")]
        [HttpGet("Users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();

            if (users == null)
            {
                return BadRequest("There are no users!");
            }

            return Ok(users);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("edit")]
        public async Task<IActionResult> EditUser([FromBody] UserTransferDTO userTransferDTO)
        {
            var user = await _userService.EditUser(userTransferDTO);

            if(user == null)
            {
                return BadRequest("Controller: sopmething went wrong");
            }

            return Ok(user);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var response = await _administratorService.DeleteUser(id);
            if(response == null)
            {
                return BadRequest($"Controller: Failed to delete user with id: {id}");
            }

            return Ok(response);
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost("role")]
        public async Task<IActionResult> ChangeUserRole([FromQuery] int userId, int roleId)
        {
            var response = await _administratorService.EditUserRole(userId, roleId);
            if(response == null)
            {
                return BadRequest("Failed to change role");
            }

            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("community")]
        public async Task<IActionResult> SetCommunity([FromQuery] int userId, int communityId)
        {
            var response = await _administratorService.SetCommunity(userId, communityId);

            if(response == null)
            {
                return BadRequest("Failed to set community");
            }

            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("managers")]
        public async Task<IActionResult> GetAllManagers()
        {
            var response = await _administratorService.GetAllManagers();

            if(response == null)
            {
                return BadRequest("Controller: something went wrong, managers not found");
            }

            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("admins")]
        public async Task<IActionResult> GetAllAdmins()
        {
            var response = await _administratorService.GetAllAdmins();

            if (response == null)
            {
                return BadRequest("Controller: something went wrong, admins not found");
            }

            return Ok(response);
        }
    }
}
