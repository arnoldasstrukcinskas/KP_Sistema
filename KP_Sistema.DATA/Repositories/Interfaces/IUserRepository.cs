using KP_Sistema.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KP_Sistema.DATA.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserByUsername(string username);
        Task<User> AddUser(User user);
        Task<User> EditUser(User user);
        Task<User> DeleteUser(User user);
        Task<User?> GetUserById(int id);
        Task<List<User>> GetAllUsers();
    }
}
