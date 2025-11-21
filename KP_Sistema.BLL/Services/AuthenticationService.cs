using AutoMapper;
using KP_Sistema.BLL.Exceptions.Authentication;
using KP_Sistema.BLL.Interfaces;
using KP_Sistema.CONTRACTS.DTO.UserDTO;
using KP_Sistema.DATA.Entities;
using KP_Sistema.DATA.Repositories.Interfaces;
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
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AuthenticationService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserReturnDTO?> Login(string username, string password)
        {
            User? user = await _userRepository.GetUserByUsername(username);

            if(user == null)
            {
                throw new UserAuthenticationNotFoundException(username);
            }

            string hash = HashPassword(password);

            if(!user.PasswordHash.Equals(hash))
            {
                throw new UserAuthenticationException("Authentication forbidden, wrong username or password");
            }

            return _mapper.Map<UserReturnDTO>(user);


        }

        public async Task<UserReturnDTO> Register(UserCreateDTO userCreateDTO)
        {
            User? user = await _userRepository.GetUserByUsername(userCreateDTO.Username);

            if (user != null)
            {
                throw new UserAuthenticationRegistrationFailedException("Registration failed, something went wront.");
            }

            User newUser = new User
            {
                Username = userCreateDTO.Username,
                PasswordHash = HashPassword(userCreateDTO.Password),
                Email = userCreateDTO.Username,
                CommunityId = null,
                Community = null,
                RoleId = 1,
                Role = new Role { Id = 1, Name = "User"}
            };
            var createdUser = await _userRepository.AddUser(user);

            return _mapper.Map<UserReturnDTO>(createdUser);
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
