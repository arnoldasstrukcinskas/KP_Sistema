using KP_Sistema.CONTRACTS.DTO.CommunityDTO;
using KP_Sistema.BLL.Exceptions.Community;
using KP_Sistema.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Numerics;

namespace KP_Sistema.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CommunityController : ControllerBase
    {
        private readonly ICommunityService _communityService;

        public CommunityController(ICommunityService communityService)
        {
            _communityService = communityService;
        }

        /// <summary>
        /// Adds community to database.
        /// </summary>
        /// <param name="community">Add community name</param>
        /// <returns>Added community data: id, name</returns>
        [HttpPost]
        public async Task<IActionResult> CreateCommunity([FromBody] CommunityCreateDTO communityCreateDTO)
        {
            if (communityCreateDTO == null)
            {
                return BadRequest("Controller: community is empty");
            }

            var community = await _communityService.AddCommunityAsync(communityCreateDTO);

            return Ok(community);
        }

        /// <summary>
        /// Find community by id.
        /// </summary>
        /// <param name="Id">Write community id</param>
        /// <returns>Returns found community data: id, name </returns>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCommunityById(int id)
        {
            if (id < 1)
            {
                return BadRequest("Controller: There is problem with given id");
            }

            var community = await _communityService.GetCommunityByIdAsync<CommunityTransferDTO>(id);

            return Ok(community);
        }

        /// <summary>
        /// Find community by name.
        /// </summary>
        /// <param name="name">Write community name</param>
        /// <returns>Returns found community data: id, name </returns>
        [HttpGet("name")]
        public async Task<IActionResult> GetCommunityByName([FromQuery] string name)
        {
            if (name.IsNullOrEmpty())
            {
                return BadRequest("Controller: name is not given.");
            }

            var community = await _communityService.GetCommunityByNameAsync<CommunityTransferDTO>(name);

            return Ok(community);
        }


        /// <summary>
        /// Search community by name.
        /// </summary>
        /// <param search="name">Write community name</param>
        /// <returns>Returns found communities data: id, name, users and tasks </returns>
        [HttpGet("search")]
        public async Task<IActionResult> GetCommunitiesByName([FromQuery] string name)
        {
            if (name.IsNullOrEmpty())
            {
                return BadRequest("Controller: name is not given.");
            }

            var communities = await _communityService.GetCommunitiesByNameAsync<CommunityTransferDTO>(name);

            return Ok(communities);
        }

        /// <summary>
        /// Find community by name.
        /// </summary>
        /// <param name="communityTransferDTO">Write community: id, name, UtilityTasks list, Users list</param>
        /// <returns>Returns found community data: id, name </returns>
        [HttpPut("id")]
        public async Task<IActionResult> EditCommunity(int id, [FromBody] CommunityEditDTO communityEditDTO)
        {
            if (communityEditDTO == null)
            {
                return BadRequest("Controller: There is no data for editing community");
            }

            var community = await _communityService.EditCommunityAsync(id, communityEditDTO);

            return Ok(community);

        }

        /// <summary>
        /// Deletes community by id.
        /// </summary>
        /// <param name="Id">Write community Id</param>
        /// <returns>Returns found community data: id, name </returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteCommunity(int id)
        {
            if (id < 1)
            {
                return BadRequest("Controller: There is problem with given id");
            }

            var community = await _communityService.DeleteCommunityAsync(id);

            return Ok(community);
        }

        /// <summary>
        /// Get all communities.
        /// </summary>
        /// <param name="Id">Write community Id</param>
        /// <returns>Returns all communities in database</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllCommunities()
        {
            var communities = await _communityService.GetAllCommunities();

            if (communities == null)
            {
                return BadRequest("Could not get communities");
            }

            return Ok(communities);

        }

        /// <summary>
        /// Adds user to specified community.
        /// </summary>
        /// <param name="communityId">Write community Id</param>
        /// <param name="userId">Write User Id</param>
        /// <returns>Returns community where was added User</returns>
        [HttpPost("AddUser/{communityId}/{userId}")]
        public async Task<IActionResult> AddUserToCommunity(int communityId, int userId)
        {
            var community = await _communityService.AddUserToCommunity(communityId, userId);

            if(community == null)
            {
                return BadRequest("Controller: Failed to add user");
            }

            return Ok(community);
        }

        /// <summary>
        /// Deletes user from specified community.
        /// </summary>
        /// <param name="communityId">Write community Id</param>
        /// <param name="userId">Write User Id</param>
        /// <returns>Returns community where was deleted User</returns>
        [HttpPost("RemoveUser/{communityId}/{userId}")]
        public async Task<IActionResult> RemoveUserFromCommunity(int communityId, int userId)
        {
            var community = await _communityService.DeleteUserFromCommunity(communityId, userId);

            if(community == null)
            {
                return BadRequest("Controller: Failed to remove user from community");
            }

            return Ok(community);
        }
    }

}
