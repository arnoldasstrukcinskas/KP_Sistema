using AutoMapper;
using KP_Sistema.BLL.Exceptions.Authentication;
using KP_Sistema.BLL.Interfaces;
using KP_Sistema.BLL.Interfaces.Users;
using KP_Sistema.CONTRACTS.DTO.UserDTO;
using KP_Sistema.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace KP_Sistema.BLL.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AuthenticationService(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<UserReturnDTO?> Login(string username, string password)
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

            return _mapper.Map<UserReturnDTO>(user);


        }

        public async Task<UserReturnDTO> Register(UserCreateDTO userCreateDTO)
        {
            UserReturnDTO? user = await _userService.GetUserByUsername(userCreateDTO.Username);

            if (user != null)
            {
                throw new UserAuthenticationRegistrationFailedException("Registration failed, something went wront.");
            }
            userCreateDTO.Password = HashPassword(userCreateDTO.Password);

            var createdUser = await _userService.AddUser(userCreateDTO);

            return createdUser;
        }

        private string HashPassword(string password)
        {
            var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
