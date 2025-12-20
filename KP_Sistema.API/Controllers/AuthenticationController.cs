using KP_Sistema.BLL.Interfaces;
using KP_Sistema.CONTRACTS.DTO.UserDTO;
using Microsoft.AspNetCore.Mvc;

namespace KP_Sistema.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {

        private readonly IAuthenticationService _authenticationService;
        private readonly IConfiguration _configuration;
        public AuthenticationController (IAuthenticationService authenticationService, IConfiguration configuration)
        {
            _authenticationService = authenticationService;
            _configuration = configuration;
        }

        /// <summary>
        /// Route for registration
        /// </summary>
        /// <param name="userCreateDto">User data transfer object containint: username, name, email, age, password.</param>
        /// <returns>Basic user information without password.</returns>
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserCreateDTO userCreateDTO)
        {

            var response = await _authenticationService.Register(userCreateDTO);

            if (response == null)
            {
                return BadRequest("Controller: Something wrong with input data");
            }

            return Ok(response);
        }
    }
}
