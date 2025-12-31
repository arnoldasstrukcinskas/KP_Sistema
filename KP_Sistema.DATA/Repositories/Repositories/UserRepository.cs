using KP_Sistema.DATA.Entities;
using KP_Sistema.DATA.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace KP_Sistema.DATA.Repositories.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;
        public UserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<User> AddUser(User user)
        {
            //Option #1
            //await _dbContext.Users.AddAsync(user);
            //await _dbContext.SaveChangesAsync();

            //Option #2
            await _dbContext.Database.ExecuteSqlAsync(
                $"""
                INSERT INTO Users (username, email, passwordHash, communityId, roleId) 
                VALUES({user.Username}, {user.Email}, {user.PasswordHash}, {user.CommunityId}, {user.RoleId})
                """
                );

            var createdUser = await GetUserByUsername(user.Username);

            return createdUser;
        }

        public async Task<User> DeleteUser(User user)
        {
            //Option #1
            //_dbContext.Users.Remove(user);
            //await _dbContext.SaveChangesAsync();

            //Option #2
            await _dbContext.Database.ExecuteSqlAsync(
                $" DELETE FROM Users WHERE userid={user.Id} ");

            var deletedUser = await GetUserById(user.Id);

            return deletedUser;
        }

        public async Task<User> EditUser(User user)
        {
            //Option #1
            //_dbContext.Users.Update(user);
            //await _dbContext.SaveChangesAsync();
            
            //Option #2
            await _dbContext.Database.ExecuteSqlAsync(
                $"""
                UPDATE Users SET username={user.Username}, mail={user.Email}, passwordHash={user.PasswordHash}
                """
                );

            return user;
        }

        public async Task<User?> GetUserByUsername(string username)
        {
            //Option #1
            //var user = await _dbContext.Users.FirstOrDefaultAsync(user => user.Username.Equals(username));

            //Option #2
            var user = await _dbContext.Users.FromSqlInterpolated(
                $"""
                SELECT * FROM Users WHERE username={username}
                """).Include(user => user.Community)
                .Include(user => user.Role)
                .FirstOrDefaultAsync();

            return user;
        }

        public async Task<User?> GetUserById(int id)
        {
            //Option #1
            //var user = await _dbContext.Users.FirstOrDefaultAsync(user => user.Id == id);

            //Option@= #2
            var user = await _dbContext.Users.FromSqlInterpolated(
                $"""
                SELECT * FROM users WHERE id={id}
                """).Include(user => user.Community)
                .Include(user => user.Role)
                .FirstOrDefaultAsync();

            return user;
        }

        public async Task<List<User>> GetAllUsers()
        {
            var users = await _dbContext.Users
                .Include(user => user.Community)
                .Include(user => user.Role)
                .ToListAsync();

            return users;
        }
    }
}
