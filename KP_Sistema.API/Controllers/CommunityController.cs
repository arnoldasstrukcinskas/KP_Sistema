using KP_Sistema.BLL.DTO.CommunityDTO;
using KP_Sistema.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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
        /// <returns>Added community with ID and community name</returns>
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

        [HttpGet]
        public async Task<IActionResult> GetCommunityByName(string name)
        {
            if(name.IsNullOrEmpty())
            {
                return BadRequest("Controller: name is not given.");
            }

            var community = await _communityService.GetCommunityByNameAsync<CommunityReturnDTO>(name);

            return Ok(community);
        }
    }
}
