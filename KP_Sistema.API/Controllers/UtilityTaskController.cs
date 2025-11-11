using AutoMapper;
using KP_Sistema.BLL.DTO.UtilityTaskDTO;
using KP_Sistema.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KP_Sistema.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UtilityTaskController : ControllerBase
    {
        private readonly IUtilityTaskService _utilityTaskService;
        private readonly IMapper _mapper;

        public UtilityTaskController(IUtilityTaskService utilityTaskService, IMapper mapper)
        {
            _utilityTaskService = utilityTaskService;
            _mapper = mapper;
        }

        /// <summary>
        /// Adds utilityTask to database
        /// </summary>
        /// <param name="utilityTaskCreateDTO">Data create object containing the details of the new UtilityTask (name, description, price, community id).</param>
        /// <returns>Added utility taskt object with name, description, price, community id</returns>
        [HttpPost]
        public async Task<IActionResult> AddUtilityTask([FromBody] UtilityTaskCreateDTO utilityTaskCreateDTO)
        {
            if(utilityTaskCreateDTO == null)
            {
                return BadRequest("Controller: There is no utility task");
            }

            var utilityTask = await _utilityTaskService.CreateTaskAsync(utilityTaskCreateDTO);
            return Ok(utilityTask);
        }
    }
}
