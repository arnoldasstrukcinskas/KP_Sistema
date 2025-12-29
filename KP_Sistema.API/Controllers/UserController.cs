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
        private readonly ICommunityService _communityService;

        public UserController(IUserService userService, ICommunityService communityService)
        {
            _userService = userService;
            _communityService = communityService;
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

        /// <summary>
        /// Gets users in specific community
        /// </summary>
        /// <param name="Id">Write community id</param>
        /// <returns>Returns Users from specific community</returns>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetUsersFromCommunity(int id)
        {
            var communtiy = await _communityService.GetCommunityByIdAsync<CommunityTransferDTO>(id);

            var users = communtiy.Users;

            if(users.IsNullOrEmpty())
            {
                return BadRequest($"There are no users in {communtiy.Name}!");
            }

            return Ok(users);
        }
    }
}
