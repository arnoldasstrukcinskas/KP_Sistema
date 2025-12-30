using AutoMapper;
using KP_Sistema.BLL.Interfaces;
using KP_Sistema.CONTRACTS.DTO.CommunityDTO;
using KP_Sistema.CONTRACTS.DTO.UtilityTaskDTO;
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
        [HttpGet("Task/{id:int}")]
        public async Task<IActionResult> GetUtilityTaskByTaskId(int id)
        {
            if(id < 1)
            {
                return BadRequest($"Controller: There is problem with given id: {id}");
            }

            var utilityTask = await _utilityTaskService.GetUtilityTaskByIdAsync<UtilityTaskReturnDTO>(id);

            return Ok(utilityTask);
        }

        /// <summary>
        /// Gets utility task by name
        /// </summary>
        /// <param name="Name">Name of utility task.</param>
        /// <returns>Data transfer object of found utility task</returns>
        [HttpGet("search")]
        public async Task<IActionResult> GetUtilityTaskByName([FromQuery] string name)
        {
            if(name.IsNullOrEmpty())
            {
                return BadRequest("Controller: name is not given.");
            }

            var utilityTask = await _utilityTaskService.GetUtilityTaskByNameAsync<UtilityTaskReturnDTO>(name);

            return Ok(utilityTask);
        }


        /// <summary>
        /// Gets utility tasks by name
        /// </summary>
        /// <param name="name">Name of utility tasks.</param>
        /// <returns>Returns utility tasks by name</returns>
        [HttpGet("TasksByName")]
        public async Task<IActionResult> GetUtilityTasksByName(string name)
        {
            if(name.IsNullOrEmpty())
            {
                return BadRequest("Controller: name is not given");
            }

            var utilityTasks = await _utilityTaskService.GetUtilityTasksByName(name);

            return Ok(utilityTasks);
        }

        /// <summary>
        /// Edits utility task
        /// </summary>
        /// <param name="id">Write utilityTask id</param>
        /// <param name="utilityTaskEditDTO">Data of utility transfer object: id, name, description, price, community id, community name</param>
        /// <returns>Returns edited utility task id, name, and community name it belonged</returns>
        [HttpPut]
        public async Task<IActionResult> EditUtilityTask(int id, [FromBody] UtilityTaskEditDTO utilityTaskEditDTO)
        {
            if(utilityTaskEditDTO == null)
            {
                return BadRequest("Controller: Utility task is empty");
            }

            var utilityTask = await _utilityTaskService.EditUtilityTaskAsync(id, utilityTaskEditDTO);

            return Ok(utilityTaskEditDTO);
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

        /// <summary>
        /// Returns all tasks from database
        /// </summary>
        /// <returns>Returns list of Utility Tasks in database</returns>
        [HttpGet("Tasks")]
        public async Task<IActionResult> GetAllUtilityTasks()
        {
            var tasks = await _utilityTaskService.GetAllUtilityTasks();
            
            if(tasks == null)
            {
                return BadRequest("Controller: Something wrong with getting tasks");
            }

            return Ok(tasks);
        }
    }
}
