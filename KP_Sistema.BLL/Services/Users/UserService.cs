using AutoMapper;
using KP_Sistema.CONTRACTS.DTO.UserDTO;
using KP_Sistema.BLL.Interfaces.Users;
using KP_Sistema.DATA.Entities;
using KP_Sistema.DATA.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KP_Sistema.BLL.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserReturnDTO> AddUser(UserCreateDTO userCreateDTO)
        {
            User newUser = new User
            {
                Username = userCreateDTO.Username,
                PasswordHash = userCreateDTO.Password,
                Email = userCreateDTO.Mail,
                CommunityId = null,
                RoleId = 1,
            };

            var createdUser = await _userRepository.AddUser(newUser);

            return _mapper.Map<UserReturnDTO>(createdUser);
        }

        public async Task<UserReturnDTO> GetUserByUsername(string username)
        {
            var foundUser = await _userRepository.GetUserByUsername(username);

            return _mapper.Map<UserReturnDTO>(foundUser);
        }

        public async Task<UserReturnDTO> DeleteUser(string userName)
        {
            var user = await _userRepository.GetUserByUsername(userName);

            return _mapper.Map<UserReturnDTO>(user);
        }

        public async Task<UserReturnDTO> EditUser(UserTransferDTO userEditDTO)
        {
            var user = _mapper.Map<User>(EditUser);
            var editedUser = await _userRepository.EditUser(user);

            return _mapper.Map<UserReturnDTO>(editedUser);
        }

        public async Task<String> GetUserPassword(string username)
        {
            var user = await _userRepository.GetUserByUsername(username);

            return user.PasswordHash;
        }

        public async Task<List<UserReturnDTO>> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsers();

            return _mapper.Map<List<UserReturnDTO>>(users);
        }
    }
}
