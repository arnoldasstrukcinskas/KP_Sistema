using KP_Sistema.BLL.DTO.CommunityDTO;
using KP_Sistema.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
        /// Adds Discount to postgreSql database.
        /// </summary>
        /// <param discount="Enter discoutn data">Name, percentage, min q of discount to create.</param>
        /// <returns>Added DTO of discount with ID, discount name, percentage and minimum quantity.</returns>
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
    }
}
