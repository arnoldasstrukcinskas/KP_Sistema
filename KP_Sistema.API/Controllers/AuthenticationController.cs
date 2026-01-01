using KP_Sistema.BLL.Interfaces;
using KP_Sistema.CONTRACTS.DTO.AuthenticationDTO;
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
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserCreateDTO userCreateDTO)
        {

            var response = await _authenticationService.Register(userCreateDTO);

            if (response == null)
            {
                return BadRequest("Controller: Something wrong with input data");
            }

            return Ok(response);
        }

        /// <summary>
        /// Route for Login
        /// </summary>
        /// <param name="username">User username</param>
        /// <param name="password">User password</param>
        /// <returns>Returns logged in user info</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            var user = await _authenticationService.Login(loginDTO.username, loginDTO.password);

            if(user == null)
            {
                return BadRequest("Failed to login");
            }

            return Ok(user);
        }


        /// <summary>
        /// Route for changing password
        /// </summary>
        /// <param name="changePasswordDTO">User id, old password and new password</param>
        /// <returns>Returns true if password changed</returns>
        [HttpPost("change")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO changePasswordDTO)
        {
            var isChanged = await _authenticationService.ChangePasword(changePasswordDTO);

            if(!isChanged)
            {
                return BadRequest("Controler: Failed to change password");
            }

            return Ok(isChanged);
        }
    }
}
