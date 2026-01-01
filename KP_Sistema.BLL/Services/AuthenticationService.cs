using AutoMapper;
using KP_Sistema.BLL.Exceptions.Authentication;
using KP_Sistema.BLL.Interfaces;
using KP_Sistema.BLL.Interfaces.Users;
using KP_Sistema.CONTRACTS.DTO.AuthenticationDTO;
using KP_Sistema.CONTRACTS.DTO.UserDTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace KP_Sistema.BLL.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AuthenticationService(IUserService userService, IMapper mapper, IConfiguration configuration)
        {
            _userService = userService;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<LoginResponseDTO?> Login(string username, string password)
        {
            UserReturnDTO? user = await _userService.GetUserByUsername(username);

            if(user == null)
            {
                throw new UserAuthenticationNotFoundException(username);
            }

            string hash = HashPassword(password);
            string userPassword = await _userService.GetUserPassword(user.Username);

            if(!userPassword.Equals(hash))
            {
                throw new UserAuthenticationException("Authentication forbidden, wrong username or password");
            }

            var jwt = JwtGenerator(user);

            var loginResponse = new LoginResponseDTO
            {
                Token = jwt,
                user = user
            };

            return loginResponse;
        }

        public async Task<UserReturnDTO> Register(UserCreateDTO userCreateDTO)
        {
            UserReturnDTO? user = await _userService.GetUserByUsername(userCreateDTO.Username);

            if (user != null)
            {
                throw new UserAuthenticationRegistrationFailedException("Registration failed, such user exists.");
            }
            userCreateDTO.Password = HashPassword(userCreateDTO.Password);

            var createdUser = await _userService.AddUser(userCreateDTO);

            return createdUser;
        }

        public async Task<bool> ChangePasword(ChangePasswordDTO changePasswordDTO)
        {
            var user = await _userService.GetUserById(changePasswordDTO.id);

            if(changePasswordDTO == null)
            {
                throw new UserAuthenticationException("There is no or not enough data for changing password");
            }

            changePasswordDTO.oldPassword = HashPassword(changePasswordDTO.oldPassword);
            changePasswordDTO.newPassword = HashPassword(changePasswordDTO.newPassword);

            var isChanged = await _userService.ChangeUserPassword(changePasswordDTO);

            return isChanged;
        }

        //Helper methods
        private string HashPassword(string password)
        {
            var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        private string JwtGenerator(UserReturnDTO userReturnDTO)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userReturnDTO.Id.ToString()),
                new Claim(ClaimTypes.Name, userReturnDTO.Username),
                new Claim(ClaimTypes.Email, userReturnDTO.Email),
                new Claim(ClaimTypes.Role, userReturnDTO.Role)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwt = tokenHandler.WriteToken(token);

            return jwt;
        }
    }
}
