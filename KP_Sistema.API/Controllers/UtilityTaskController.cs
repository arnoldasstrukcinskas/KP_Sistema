using AutoMapper;
using KP_Sistema.BLL.DTO.UtilityTaskDTO;
using KP_Sistema.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace KP_Sistema.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UtilityTaskController : ControllerBase
    {
        private readonly IUtilityTaskService _utilityTaskService;

        public UtilityTaskController(IUtilityTaskService utilityTaskService)
        {
            _utilityTaskService = utilityTaskService;
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

            var utilityTask = await _utilityTaskService.AddUtilityTaskAsync(utilityTaskCreateDTO);
            return Ok(utilityTask);
        }

        /// <summary>
        /// Gets utility task by id
        /// </summary>
        /// <param name="Id">Id of utility task.</param>
        /// <returns>Data transfer object of found utility task</returns>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetUtilityTaskById(int id)
        {
            if(id < 1)
            {
                return BadRequest("Controller: There is problem with given id");
            }

            var utilityTask = await _utilityTaskService.GetUtilityTaskByIdAsync<UtilityTaskReturnDTO>(id);

            return Ok(utilityTask);
        }

        /// <summary>
        /// Gets utility task by name
        /// </summary>
        /// <param name="Name">Name of utility task.</param>
        /// <returns>Data transfer object of found utility task</returns>
        [HttpGet("UtilityTask/{name}")]
        public async Task<IActionResult> GetUtilityTaskByName(string name)
        {
            if(name.IsNullOrEmpty())
            {
                return BadRequest("Controller: name is not given.");
            }

            var utilityTask = await _utilityTaskService.GetUtilityTaskByNameAsync<UtilityTaskReturnDTO>(name);

            return Ok(utilityTask);
        }

        /// <summary>
        /// Edits utility task
        /// </summary>
        /// <param name="utilityTask">Data of utility transfer object: id, name, description, price, community id, community name</param>
        /// <returns>Returns edited utility task id, name, and community name it belonged</returns>
        [HttpPut]
        public async Task<IActionResult> EditUtilityTask([FromBody] UtilityTaskTransferDTO utilityTaskTransferDTO)
        {
            if(utilityTaskTransferDTO == null)
            {
                return BadRequest("Controller: Utility task is empty");
            }

            var utilityTask = await _utilityTaskService.EditUtilityTaskAsync(utilityTaskTransferDTO);

            return Ok(utilityTaskTransferDTO);
        }

        /// <summary>
        /// Deletes utility task
        /// </summary>
        /// <param name="Id">Id of utility task</param>
        /// <returns>Returns deleted utility task id, name, and community name it belonged</returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteUtilityTask(int id)
        {
            if(id < 1)
            {
                return BadRequest("Controller:  There is problem with given id");
            }

            var utilityTask = await _utilityTaskService.DeleteUtilityTaskAsync(id);

            return Ok(utilityTask);
        }

        [HttpGet("community/{communityName}")]
        public async Task<IActionResult> GetAllUtilityTasksByCommunityName(string communityName)
        {
            if(communityName.IsNullOrEmpty())
            {
                return BadRequest("\"Controller: name is not given.\"");
            }

            var utilityTasks = await _utilityTaskService.GetAllUtilityTasksByCommunityAsync(communityName);

            return Ok(utilityTasks);
        }
    }
}
