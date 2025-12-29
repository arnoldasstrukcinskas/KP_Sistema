using KP_Sistema.BLL.Interfaces;
using KP_Sistema.BLL.Interfaces.Users;
using KP_Sistema.CONTRACTS.DTO.CommunityDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace KP_Sistema.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
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
    }
}
