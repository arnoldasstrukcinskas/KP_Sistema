using KP_Sistema.BLL.DTO.CommunityDTO;
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
            if(communityCreateDTO == null)
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
            if(id < 1)
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
        [HttpGet("Community/{name}")]
        public async Task<IActionResult> GetCommunityByName([FromRoute] string name)
        {
            if(name.IsNullOrEmpty())
            {
                return BadRequest("Controller: name is not given.");
            }

            var community = await _communityService.GetCommunityByNameAsync<CommunityTransferDTO>(name);

            return Ok(community);
        }

        /// <summary>
        /// Find community by name.
        /// </summary>
        /// <param name="communityTransferDTO">Write community: id, name, UtilityTasks list, Users list</param>
        /// <returns>Returns found community data: id, name </returns>
        [HttpPut]
        public async Task<IActionResult> EditCommunity([FromBody] CommunityTransferDTO communityTransferDTO)
        {
            if (communityTransferDTO == null)
            {
                return BadRequest("Controller: There is no data for editing community");
            }

            var community = await _communityService.EditCommunityAsync(communityTransferDTO);

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
                throw new CommunityException("Could get communities");
            }

            return Ok(communities);

        }
    }

}
