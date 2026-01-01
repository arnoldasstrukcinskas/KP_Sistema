using KP_Sistema.BLL.Interfaces;
using KP_Sistema.BLL.Interfaces.Users;
using KP_Sistema.CONTRACTS.DTO.CommunityDTO;
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
    }
}
