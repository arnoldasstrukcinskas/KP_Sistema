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
        [Authorize(Roles = "Admin")]
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
        /// Updates an existing user's information based on the provided data.
        /// </summary>
        /// <param name="userTransferDTO">The data transfer object containing updated user information.</param>
        /// <returns>Changed data of user</returns>
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

        /// <summary>
        /// Deletes a user with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>Deleted user data</returns>
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

        /// <summary>
        /// Changes the role of a specified user.
        /// </summary>
        /// <param name="userId">The ID of the user whose role is to be changed.</param>
        /// <param name="roleId">The ID of the new role to assign to the user.</param>
        /// <returns>User data which role was changed</returns>
        [Authorize(Roles = "Admin")]
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

        /// <summary>
        /// Assigns a user to a specified community.
        /// </summary>
        /// <param name="userId">The ID of the user to assign.</param>
        /// <param name="communityId">The ID of the community to assign the user to.</param>
        /// <returns>User data which was added to specified community</returns>
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

        /// <summary>
        /// Retrieves a list of all managers. Accessible only to users with the Admin role.
        /// </summary>
        /// <returns>List of users which are managers</returns>
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

        /// <summary>
        /// Retrieves a list of all administrators.
        /// </summary>
        /// <returns>List of users which are admins</returns>
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
